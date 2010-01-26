using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using MeetupManager.Core.Domain;
using MeetupManager.Core.Repositories;
using MeetupManager.Core.Services;
using Microsoft.Win32;

namespace MeetupManager
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            //no IoC for now so will just load it.

            IList<RsvpItem> rsvpItems;

            //load data from meetup
            IMeetupService service = new MeetupService(new MeetupRepository(txbAPIKey.Text));
            rsvpItems = service.GetRsvpsForEvent(long.Parse(txbEventId.Text));


            //export it
            IRsvpManager manager = new RsvpManager();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("FirstName,LastName,RSVPName,RSVPAnswer,RSVPGuests");
            foreach (var item in manager.GetAttendees(rsvpItems))
            {
                sb.AppendLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\"", item.FirstName, item.LastName, item.RSVPName, item.RSVPAnswer, item.RSVPGuests));
            }

            SaveFileDialog fd = new SaveFileDialog();
            if (fd.ShowDialog() ?? false)
            {
                File.WriteAllText(fd.FileName, sb.ToString());
            }
        }
    }
}
