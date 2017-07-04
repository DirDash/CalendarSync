﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            var startDate = dateTimePicker1.Value;
            var finishDate = dateTimePicker2.Value;

            ICalendarService outlookService = new OutlookService();
            ICalendarService googleService = new GoogleService();

            var calendars = new List<ICalendarService> { outlookService, googleService };

            new Syncronizator().ApplyAllUpdates(startDate, finishDate, calendars);
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
    }
}
