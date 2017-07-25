using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using SynchronizerLib;
using SynchronizerLib.CalendarServices;
using SynchronizerLib.Google;
using SynchronizerLib.Outlook;
using LoggerNamespace;

namespace CalendarSynchronizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SynchronizerMainWindow : Window
    {
        private CalendarStore _calendarStore;
        private ISynchronizer _synchronizer;
        private DispatcherTimer _timer = new DispatcherTimer();

        public SynchronizerMainWindow()
        {
            InitializeComponent();
            LoadCalendarServices();
            RefreshCalendarServicesListBox();
            _timer.Tick += DispatcherTimer_Tick;
            RefresgSettings();
        }

        private void RefresgSettings()
        {
            RefreshDateTimePickers();            
            SetUpTimer();
        }

        private void RefreshDateTimePickers()
        {
            From_DatePicker.SelectedDate = DateTime.Now.Date;
            To_DatePicker.SelectedDate = ((DateTime)From_DatePicker.SelectedDate).AddDays(SynchronizationConfigManager.SynchronizationIntervalInDays);
        }

        private void LoadCalendarServices()
        {
            ICalendarService outlookCalendar = new CalendarServiceLoggingDecorator(new OutlookService(), new NLogLogger());
            ICalendarService googleCalendar = new CalendarServiceLoggingDecorator(new GoogleService(), new NLogLogger());
            _calendarStore = new CalendarStore();
            _calendarStore.AddCalendar(outlookCalendar);
            _calendarStore.AddCalendar(googleCalendar);
            _synchronizer = new SynchronizerLoggingDecorator(new Synchronizer(), new NLogLogger());
        }

        private void RefreshCalendarServicesListBox()
        {
            CalendarServices_ListBox.Items.Clear();
            foreach (var calendarService in _calendarStore.GetCalendars())
                CalendarServices_ListBox.Items.Add(calendarService);
        }

        private void SetUpTimer()
        {            
            _timer.Interval = new TimeSpan(0, SynchronizationConfigManager.AutosyncIntervalInMinutes, 0);
            _timer.IsEnabled = SynchronizationConfigManager.AutosyncronizationMode;
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            Synchronize();
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CalendarServices_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CalendarServices_ListBox.SelectedIndex >= 0)
                RemoveCalendar_Button.IsEnabled = true;
            else
                RemoveCalendar_Button.IsEnabled = false;
        }

        private void RemoveCalendar_Button_Click(object sender, RoutedEventArgs e)
        {
            _calendarStore.RemoveCalendar(CalendarServices_ListBox.SelectedItem as ICalendarService);
            RefreshCalendarServicesListBox();
        }

        private void Synchronize_Button_Click(object sender, RoutedEventArgs e)
        {
            Synchronize();
        }

        private void Synchronize()
        {
            SyncStatus_Label.Content = "Synchronizing...";
            bool synchronizationSucceeded = true;
            var startDate = (DateTime)From_DatePicker.SelectedDate;
            var finishDate = (DateTime)To_DatePicker.SelectedDate;            
            try
            {
                _synchronizer.SynchronizeAll(_calendarStore, startDate, finishDate);
            }
            catch
            {                
                MessageBox.Show("An error has occurred. Calendars are not synchronized. Read more about the error in the log file.");
                synchronizationSucceeded = false;
            }
            if (synchronizationSucceeded)
                SyncStatus_Label.Content = "Calendars are synchronized" + " (" + DateTime.Now.ToString() + ")";
            else
                SyncStatus_Label.Content = "Calendarts are not synchronized";
        }

        private void SettingsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Window settingsWindows = new SettingsWindow(_calendarStore);
            settingsWindows.ShowDialog();
            RefresgSettings();
        }

        private void To_DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckDateTimePickers();
        }

        private void From_DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckDateTimePickers();
        }

        private void CheckDateTimePickers()
        {
            if (From_DatePicker.SelectedDate >= To_DatePicker.SelectedDate)
                To_DatePicker.SelectedDate = ((DateTime)From_DatePicker.SelectedDate).AddHours(1);
        }
    }
}
