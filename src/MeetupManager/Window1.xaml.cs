using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using MeetupManager.Core.Domain;
using MeetupManager.Core.Repositories;
using MeetupManager.Core.Services;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

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
            //load data from meetup
            IMeetupService service = new MeetupService(new MeetupRepository(txbAPIKey.Text));
            IList<RsvpItem> rsvpItems = service.GetRsvpsForEvent(long.Parse(txbEventId.Text));


            //export it
            var saveFileDialog = new SaveFileDialog
                         {
                             InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                         };

            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, new RsvpManager().GetAttendees(rsvpItems).ToCsv(true));
            }
        }
    }
}