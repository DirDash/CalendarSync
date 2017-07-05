using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Configuration;

namespace synchronizer
{
    public partial class Form1 : Form
    {
        private readonly string _google = "Google";
        private readonly string _outlook = "Outlook";
        public Form1()
        {
            InitializeComponent();
            dateTimePicker2.Value = (dateTimePicker1.Value).AddMonths(1);
            autosync_timer.Interval = autosync_trackBar.Value * 60000;

        }

        private void LoadSettingsFromConfig()
        {
            //todo
            bool autosync;
            switch (ConfigurationSettings.AppSettings["autosync"])
            {
                case "false":
                    autosync_checkBox.Checked = true;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sync();
        }

        private void Sync()
        {
            syncStatuc_label.Text = "Синхронизация...";
            var startDate = dateTimePicker1.Value;
            var finishDate = dateTimePicker2.Value;

            ICalendarService outlookService = new OutlookService();
            ICalendarService googleService = new GoogleService();

            var calendars = new List<ICalendarService> { outlookService, googleService };

            new Syncronizator().ApplyAllUpdates(startDate, finishDate, calendars);
            syncStatuc_label.Text = "Данные синхронизированы" + " (" + DateTime.Now + ")";
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void autosync_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (autosync_checkBox.Checked)
            {
                autosync_trackBar.Visible = true;
                astbMin_label.Visible = true;
                astbMax_label.Visible = true;
                Sync();
                autosync_timer.Enabled = true;
            }
            else
            {
                autosync_trackBar.Visible = false;
                astbMin_label.Visible = false;
                astbMax_label.Visible = false;
                autosync_timer.Enabled = false;
            }
        }

        private void autosync_trackBar_Scroll(object sender, EventArgs e)
        {
            autosync_timer.Interval = autosync_trackBar.Value * 60000;
        }

        private void autosync_timer_Tick(object sender, EventArgs e)
        {
            Sync();
        }
    }
}
