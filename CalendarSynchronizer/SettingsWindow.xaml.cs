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
using System.Windows.Shapes;
using SynchronizerLib;

namespace CalendarSynchronizer
{
    /// <summary>
    /// Interaction logic for SettingsWindows.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private int _syncIntervalMaxDays = 60;
        private int _autoSyncMaxMins = 60;
        private CalendarStore _workingCalendarStore;
        private ICalendarService _currentCalendar;

        public SettingsWindow(CalendarStore workingCalendarStore)
        {
            InitializeComponent();
            _workingCalendarStore = workingCalendarStore;
            SetUpSyncIntervalControls();
            SetUpAutoSyncCheckBox();
            SetUpAutoSyncIntervalControls();
            SetUpCalendarServicesComboBox();
            ChangesAccepted_Label.Content = String.Empty;
        }

        private void SetUpSyncIntervalControls()
        {
            int syncInterval = SynchronizationConfigManager.SynchronizationIntervalInDays;
            if (syncInterval > _syncIntervalMaxDays)
                _syncIntervalMaxDays += syncInterval;
            for (int i = 1; i <= _syncIntervalMaxDays; i++)
                SyncInterval_ComboBox.Items.Add(i);
            SyncInterval_ComboBox.SelectedIndex = SyncInterval_ComboBox.Items.IndexOf(syncInterval);
        }

        private void SetUpAutoSyncCheckBox()
        {
            AutoSync_CheckBox.IsChecked = SynchronizationConfigManager.AutosyncronizationMode;
        }

        private void SetUpAutoSyncIntervalControls()
        {
            int autoSyncInterval = SynchronizationConfigManager.AutosyncIntervalInMinutes;
            if (autoSyncInterval > _autoSyncMaxMins)
                _autoSyncMaxMins += autoSyncInterval;
            for (int i = 1; i <= _autoSyncMaxMins; i++)
                AutoSyncInterval_ComboBox.Items.Add(i);
            AutoSyncInterval_ComboBox.SelectedIndex = AutoSyncInterval_ComboBox.Items.IndexOf(autoSyncInterval);
        }

        private void SetUpCalendarServicesComboBox()
        {
            foreach (var calendarService in _workingCalendarStore.GetCalendars())
                CalendarService_ComboBox.Items.Add(calendarService);
            if (CalendarService_ComboBox.Items.Count > 0)
                CalendarService_ComboBox.SelectedIndex = 0;
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AutoSync_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SynchronizationConfigManager.AutosyncronizationMode = true;
            ChangesAccepted_Label.Content = "Changes accepted";
            AutoSyncInterval_ComboBox.IsEnabled = true;
        }

        private void AutoSync_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SynchronizationConfigManager.AutosyncronizationMode = false;
            ChangesAccepted_Label.Content = "Changes accepted";
            AutoSyncInterval_ComboBox.IsEnabled = false;
        }

        private void CalendarService_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _currentCalendar = CalendarService_ComboBox.SelectedItem as ICalendarService;
            RefreshCalendarControls();
        }

        private void RefreshCalendarControls()
        {
            var config = _currentCalendar.GetConfigManager();
            OutFilter_TextBox.Text = config.OutFilter;
            OutCondition_TextBox.Text = config.OutTransformation.Condition;
            OutTransformation_TextBox.Text = config.OutTransformation.Transformation;
            InCondition_TextBox.Text = config.InTransformation.Condition;
            InTransformation_TextBox.Text = config.InTransformation.Transformation;
            var bannedServices = config.BannedToSyncToServices;
            string services = string.Empty;
            if (bannedServices.Count > 0)
                services += bannedServices[0];
            for (int i = 1; i < bannedServices.Count; i++)
                services += ", " + bannedServices[i];
            Banned_TextBox.Text = services;
            AcceptCalendarChanges_Button.IsEnabled = false;
        }

        private void SyncInterval_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SynchronizationConfigManager.SynchronizationIntervalInDays = (int)SyncInterval_ComboBox.SelectedItem;
            ChangesAccepted_Label.Content = "Changes accepted";
        }

        private void AutoSyncInterval_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SynchronizationConfigManager.AutosyncIntervalInMinutes = (int)AutoSyncInterval_ComboBox.SelectedItem;
            ChangesAccepted_Label.Content = "Changes accepted";
        }

        private void AcceptCalendarChanges_Button_Click(object sender, RoutedEventArgs e)
        {
            var config = _currentCalendar.GetConfigManager();
            config.OutFilter = OutFilter_TextBox.Text;
            config.OutTransformation = new EventTransformation(OutCondition_TextBox.Text, OutTransformation_TextBox.Text);
            config.InTransformation = new EventTransformation(InCondition_TextBox.Text, InTransformation_TextBox.Text);
            config.BannedToSyncToServices = Banned_TextBox.Text.Replace(" ", "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            AcceptCalendarChanges_Button.IsEnabled = false;
        }

        private void OutFilter_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AcceptCalendarChanges_Button.IsEnabled = true;
        }

        private void OutCondition_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AcceptCalendarChanges_Button.IsEnabled = true;
        }

        private void OutTransformation_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AcceptCalendarChanges_Button.IsEnabled = true;
        }

        private void InCondition_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AcceptCalendarChanges_Button.IsEnabled = true;
        }

        private void InTransformation_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AcceptCalendarChanges_Button.IsEnabled = true;
        }

        private void Banned_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AcceptCalendarChanges_Button.IsEnabled = true;
        }
    }
}
