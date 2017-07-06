﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace synchronizer
{
    public partial class CalendarSyncForm : Form
    {
        public CalendarSyncForm()
        {
            InitializeComponent();
            to_dateTimePicker.Value = (from_dateTimePicker.Value).AddDays(SyncronizationConfigManager.SynchronizationIntervalInDays);        
            autosync_timer.Interval = (int)TimeSpan.FromMinutes(autosync_trackBar.Value).TotalMilliseconds;
            LoadSettingsFromConfig();
        }

        private void LoadSettingsFromConfig()
        {
            autosync_checkBox.Checked = SyncronizationConfigManager.AutosyncronizationMode;
            autosync_trackBar.Value = (int)TimeSpan.FromSeconds(SyncronizationConfigManager.AutosyncIntervalInSeconds).TotalMinutes;
            autosync_trackBar_Scroll(this, new EventArgs());
        }

        private void sync_button_Click(object sender, EventArgs e)
        {
            Sync();
        }

        private void Sync()
        {
            syncStatus_label.Text = "Синхронизация...";
            var startDate = from_dateTimePicker.Value;
            var finishDate = to_dateTimePicker.Value;

            ICalendarService outlookService = new OutlookService();
            ICalendarService googleService = new GoogleService();

            var calendars = new List<ICalendarService> { outlookService, googleService };

            new Syncronizator().ApplyAllUpdates(startDate, finishDate, calendars);
            syncStatus_label.Text = "Данные синхронизированы" + " (" + DateTime.Now + ")";
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void autosync_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (autosync_checkBox.Checked)
            {
                SyncronizationConfigManager.AutosyncronizationMode = true;
                autosync_trackBar.Visible = true;
                astbMin_label.Visible = true;
                astbMax_label.Visible = true;
                every_label.Visible = true;
                autosyncInterval_label.Visible = true;
                mins_label.Visible = true;
                autosync_timer.Start();
            }
            else
            {
                SyncronizationConfigManager.AutosyncronizationMode = false;
                autosync_trackBar.Visible = false;
                astbMin_label.Visible = false;
                astbMax_label.Visible = false;
                every_label.Visible = false;
                autosyncInterval_label.Visible = false;
                mins_label.Visible = false;
                autosync_timer.Stop();
            }
        }

        private void autosync_trackBar_Scroll(object sender, EventArgs e)
        {
            SyncronizationConfigManager.AutosyncIntervalInSeconds = ((int)TimeSpan.FromMinutes(autosync_trackBar.Value).TotalSeconds);
            autosyncInterval_label.Text = autosync_trackBar.Value.ToString();
            autosync_timer.Interval = (int)TimeSpan.FromMinutes(autosync_trackBar.Value).TotalMilliseconds;
        }

        private void autosync_timer_Tick(object sender, EventArgs e)
        {
            Sync();
        }

        private void to_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (to_dateTimePicker.Value < from_dateTimePicker.Value)
                to_dateTimePicker.Value = from_dateTimePicker.Value.AddDays(1);

        }
    }
}