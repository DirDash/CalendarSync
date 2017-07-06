namespace synchronizer
{
    partial class CalendarSyncForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.from_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.sync_button = new System.Windows.Forms.Button();
            this.exit_button = new System.Windows.Forms.Button();
            this.to_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.autosync_checkBox = new System.Windows.Forms.CheckBox();
            this.autosync_trackBar = new System.Windows.Forms.TrackBar();
            this.astbMin_label = new System.Windows.Forms.Label();
            this.astbMax_label = new System.Windows.Forms.Label();
            this.autosync_timer = new System.Windows.Forms.Timer(this.components);
            this.syncStatus_label = new System.Windows.Forms.Label();
            this.every_label = new System.Windows.Forms.Label();
            this.mins_label = new System.Windows.Forms.Label();
            this.autosyncInterval_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.autosync_trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // from_dateTimePicker
            // 
            this.from_dateTimePicker.Enabled = false;
            this.from_dateTimePicker.Location = new System.Drawing.Point(78, 35);
            this.from_dateTimePicker.Name = "from_dateTimePicker";
            this.from_dateTimePicker.Size = new System.Drawing.Size(138, 20);
            this.from_dateTimePicker.TabIndex = 0;
            // 
            // sync_button
            // 
            this.sync_button.Location = new System.Drawing.Point(56, 191);
            this.sync_button.Name = "sync_button";
            this.sync_button.Size = new System.Drawing.Size(154, 40);
            this.sync_button.TabIndex = 1;
            this.sync_button.Text = "синхронизировать";
            this.sync_button.UseVisualStyleBackColor = true;
            this.sync_button.Click += new System.EventHandler(this.sync_button_Click);
            // 
            // exit_button
            // 
            this.exit_button.Location = new System.Drawing.Point(96, 236);
            this.exit_button.Margin = new System.Windows.Forms.Padding(2);
            this.exit_button.Name = "exit_button";
            this.exit_button.Size = new System.Drawing.Size(77, 32);
            this.exit_button.TabIndex = 2;
            this.exit_button.Text = "exit";
            this.exit_button.UseVisualStyleBackColor = true;
            this.exit_button.Click += new System.EventHandler(this.exit_button_Click);
            // 
            // to_dateTimePicker
            // 
            this.to_dateTimePicker.Location = new System.Drawing.Point(78, 60);
            this.to_dateTimePicker.Name = "to_dateTimePicker";
            this.to_dateTimePicker.Size = new System.Drawing.Size(138, 20);
            this.to_dateTimePicker.TabIndex = 3;
            this.to_dateTimePicker.ValueChanged += new System.EventHandler(this.to_dateTimePicker_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(93, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Синхронизировать";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "с";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "до";
            // 
            // autosync_checkBox
            // 
            this.autosync_checkBox.AutoSize = true;
            this.autosync_checkBox.Location = new System.Drawing.Point(56, 96);
            this.autosync_checkBox.Name = "autosync_checkBox";
            this.autosync_checkBox.Size = new System.Drawing.Size(127, 17);
            this.autosync_checkBox.TabIndex = 7;
            this.autosync_checkBox.Text = "Автосинхронизация";
            this.autosync_checkBox.UseVisualStyleBackColor = true;
            this.autosync_checkBox.CheckedChanged += new System.EventHandler(this.autosync_checkBox_CheckedChanged);
            // 
            // autosync_trackBar
            // 
            this.autosync_trackBar.Location = new System.Drawing.Point(48, 140);
            this.autosync_trackBar.Maximum = 60;
            this.autosync_trackBar.Minimum = 1;
            this.autosync_trackBar.Name = "autosync_trackBar";
            this.autosync_trackBar.Size = new System.Drawing.Size(168, 45);
            this.autosync_trackBar.TabIndex = 8;
            this.autosync_trackBar.Value = 5;
            this.autosync_trackBar.Visible = false;
            this.autosync_trackBar.Scroll += new System.EventHandler(this.autosync_trackBar_Scroll);
            // 
            // astbMin_label
            // 
            this.astbMin_label.AutoSize = true;
            this.astbMin_label.Location = new System.Drawing.Point(10, 140);
            this.astbMin_label.Name = "astbMin_label";
            this.astbMin_label.Size = new System.Drawing.Size(39, 13);
            this.astbMin_label.TabIndex = 9;
            this.astbMin_label.Text = "1 мин.";
            this.astbMin_label.Visible = false;
            // 
            // astbMax_label
            // 
            this.astbMax_label.AutoSize = true;
            this.astbMax_label.Location = new System.Drawing.Point(222, 140);
            this.astbMax_label.Name = "astbMax_label";
            this.astbMax_label.Size = new System.Drawing.Size(33, 13);
            this.astbMax_label.TabIndex = 10;
            this.astbMax_label.Text = "1 час";
            this.astbMax_label.Visible = false;
            // 
            // autosync_timer
            // 
            this.autosync_timer.Tick += new System.EventHandler(this.autosync_timer_Tick);
            // 
            // syncStatus_label
            // 
            this.syncStatus_label.AutoSize = true;
            this.syncStatus_label.Location = new System.Drawing.Point(10, 270);
            this.syncStatus_label.Name = "syncStatus_label";
            this.syncStatus_label.Size = new System.Drawing.Size(163, 13);
            this.syncStatus_label.TabIndex = 11;
            this.syncStatus_label.Text = "Данные не синхронизированы";
            // 
            // every_label
            // 
            this.every_label.AutoSize = true;
            this.every_label.Location = new System.Drawing.Point(53, 116);
            this.every_label.Name = "every_label";
            this.every_label.Size = new System.Drawing.Size(48, 13);
            this.every_label.TabIndex = 12;
            this.every_label.Text = "Каждые";
            this.every_label.Visible = false;
            // 
            // mins_label
            // 
            this.mins_label.AutoSize = true;
            this.mins_label.Location = new System.Drawing.Point(132, 116);
            this.mins_label.Name = "mins_label";
            this.mins_label.Size = new System.Drawing.Size(30, 13);
            this.mins_label.TabIndex = 13;
            this.mins_label.Text = "мин.";
            this.mins_label.Visible = false;
            // 
            // autosyncInterval_label
            // 
            this.autosyncInterval_label.AutoSize = true;
            this.autosyncInterval_label.Location = new System.Drawing.Point(107, 116);
            this.autosyncInterval_label.Name = "autosyncInterval_label";
            this.autosyncInterval_label.Size = new System.Drawing.Size(19, 13);
            this.autosyncInterval_label.TabIndex = 14;
            this.autosyncInterval_label.Text = "05";
            this.autosyncInterval_label.Visible = false;
            // 
            // CalendarSyncForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 290);
            this.Controls.Add(this.autosyncInterval_label);
            this.Controls.Add(this.mins_label);
            this.Controls.Add(this.every_label);
            this.Controls.Add(this.syncStatus_label);
            this.Controls.Add(this.astbMax_label);
            this.Controls.Add(this.astbMin_label);
            this.Controls.Add(this.autosync_trackBar);
            this.Controls.Add(this.autosync_checkBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.to_dateTimePicker);
            this.Controls.Add(this.exit_button);
            this.Controls.Add(this.sync_button);
            this.Controls.Add(this.from_dateTimePicker);
            this.Name = "CalendarSyncForm";
            this.Text = "Cинхронизатор календарей";
            ((System.ComponentModel.ISupportInitialize)(this.autosync_trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker from_dateTimePicker;
        private System.Windows.Forms.Button sync_button;
        private System.Windows.Forms.Button exit_button;
        private System.Windows.Forms.DateTimePicker to_dateTimePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox autosync_checkBox;
        private System.Windows.Forms.TrackBar autosync_trackBar;
        private System.Windows.Forms.Label astbMin_label;
        private System.Windows.Forms.Label astbMax_label;
        private System.Windows.Forms.Timer autosync_timer;
        private System.Windows.Forms.Label syncStatus_label;
        private System.Windows.Forms.Label every_label;
        private System.Windows.Forms.Label mins_label;
        private System.Windows.Forms.Label autosyncInterval_label;
    }
}

