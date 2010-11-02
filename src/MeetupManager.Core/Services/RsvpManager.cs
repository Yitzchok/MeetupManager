using System;
using System.Collections.Generic;
using System.Linq;
using MeetupManager.Core.Domain;

namespace MeetupManager.Core.Services
{
    public class RsvpManager : IRsvpManager
    {
        public IEnumerable<Attendee> GetAttendees(IList<RsvpItem> items)
        {
            var rsvpItems = from item in items
                            where item.Response != RsvpReponseType.No
                            select item;

            foreach (var item in rsvpItems)
            {
                yield return GetAttendee(item, 0);
                for (int i = 1; i < item.Guests + 1; i++)
                {
                    Attendee attendee = null;
                    try
                    {
                        attendee = GetAttendee(item, i);
                    }
                    catch {/*ignore*/}

                    if (attendee != null)
                        yield return attendee;
                }

            }
        }

        public Attendee GetAttendee(RsvpItem item, int guestNumber)
        {
            Attendee attendee = new Attendee();
            string[] nameSplit = item.Name.Split(' ');

            //we hope that the first answer is the guests names
            string firstAnswer = item.Answers != null && item.Answers.Count > 0 ? item.Answers[0].Replace(Environment.NewLine, ",") : "";

            if (!string.IsNullOrEmpty(firstAnswer))
            {
                string proposedName = "";

                if ((item.Guests > 0 && guestNumber > 0) && (!firstAnswer.Contains(",") || !firstAnswer.Contains(" and ")))
                {
                    //will try spaces

                    string[] spaceSplit = firstAnswer.Split(' ');

                    if (spaceSplit.Length == (item.Guests * 2) + 2)
                    {
                        int index = guestNumber;
                        if (guestNumber > 0)
                        {
                            index = guestNumber * 2;
                            proposedName = spaceSplit[index] + " " + spaceSplit[index + 1];
                        }
                        else
                        {
                            proposedName = spaceSplit[index] + " " + spaceSplit[index + 1];
                        }
                    }
                }

                if (string.IsNullOrEmpty(proposedName))
                {
                    proposedName = firstAnswer
                        .Split(new[] { ",", " and " }, 5, StringSplitOptions.RemoveEmptyEntries)[guestNumber]
                        .Trim();
                }

                string[] strings = proposedName.Split(' ');

                if (guestNumber == 0)
                {
                    if (strings.Length < 2 || strings[0].Contains("myself"))
                    {
                        strings = nameSplit;
                    }
                }
                ExtractName(strings, attendee);
                attendee.RSVPAnswer = firstAnswer;
            }
            else
            {
                ExtractName(nameSplit, attendee);
            }

            attendee.RSVPName = item.Name;
            attendee.RSVPGuests = item.Guests;

            return attendee;
        }

        public void ExtractName(string[] nameSplit, Attendee attendee)
        {
            string firstName;
            string lastName = "";
            if (nameSplit.Length == 1)
            {
                firstName = nameSplit[0];
            }
            else if (nameSplit.Length == 3)
            {
                firstName = nameSplit[0] + " " + nameSplit[1];
                lastName = nameSplit[2];
            }
            else
            {
                firstName = nameSplit[0];
                lastName = nameSplit[1];
            }

            attendee.FirstName = firstName.Trim();
            attendee.LastName = lastName.Trim();
        }

    }
}