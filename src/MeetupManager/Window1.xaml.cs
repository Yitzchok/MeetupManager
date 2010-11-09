using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using MeetupManager.Core.Domain;
using MeetupManager.Core.Repositories;
using MeetupManager.Core.Services;
using MeetupManager.Properties;
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
            txbAPIKey.Text = Settings.Default.ApiKey;
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbAPIKey.Text) || string.IsNullOrEmpty(txbEventId.Text))
            {
                MessageBox.Show("Please enter all the information on the form.", "Export", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //load data from meetup should be async
            IMeetupService service = new MeetupService(new MeetupRepository(txbAPIKey.Text));
            IList<RsvpItem> rsvpItems = service.GetRsvpsForEvent(long.Parse(txbEventId.Text));

            //export it
            var saveFileDialog = new SaveFileDialog
                         {
                             InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                             DefaultExt = "csv",
                             AddExtension = true,
                             Filter = @"CSV Files (*.csv)|*.csv"
                         };

            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveFileDialog.FileName, new RsvpManager().GetAttendees(rsvpItems).ToCsv(true));
                    MessageBox.Show("Export done.", "Export", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show("Export Failed.", "Export", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ApplicationClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Settings.Default.ApiKey = txbAPIKey.Text;

            Settings.Default.Save();
        }
    }
}