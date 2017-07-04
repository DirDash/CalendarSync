namespace synchronizer
{
    partial class Form1
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.autosync_checkBox = new System.Windows.Forms.CheckBox();
            this.autosync_trackBar = new System.Windows.Forms.TrackBar();
            this.astbMin_label = new System.Windows.Forms.Label();
            this.astbMax_label = new System.Windows.Forms.Label();
            this.autosync_timer = new System.Windows.Forms.Timer(this.components);
            this.syncStatuc_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.autosync_trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(78, 35);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(138, 20);
            this.dateTimePicker1.TabIndex = 0;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(56, 173);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(154, 40);
            this.button1.TabIndex = 1;
            this.button1.Text = "синхронизировать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(96, 218);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(77, 32);
            this.button2.TabIndex = 2;
            this.button2.Text = "exit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(78, 60);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(138, 20);
            this.dateTimePicker2.TabIndex = 3;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
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
            this.autosync_trackBar.Location = new System.Drawing.Point(50, 122);
            this.autosync_trackBar.Maximum = 60;
            this.autosync_trackBar.Minimum = 1;
            this.autosync_trackBar.Name = "autosync_trackBar";
            this.autosync_trackBar.Size = new System.Drawing.Size(174, 45);
            this.autosync_trackBar.TabIndex = 8;
            this.autosync_trackBar.Value = 5;
            this.autosync_trackBar.Visible = false;
            this.autosync_trackBar.Scroll += new System.EventHandler(this.autosync_trackBar_Scroll);
            // 
            // astbMin_label
            // 
            this.astbMin_label.AutoSize = true;
            this.astbMin_label.Location = new System.Drawing.Point(12, 131);
            this.astbMin_label.Name = "astbMin_label";
            this.astbMin_label.Size = new System.Drawing.Size(39, 13);
            this.astbMin_label.TabIndex = 9;
            this.astbMin_label.Text = "1 мин.";
            this.astbMin_label.Visible = false;
            // 
            // astbMax_label
            // 
            this.astbMax_label.AutoSize = true;
            this.astbMax_label.Location = new System.Drawing.Point(220, 131);
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
            // syncStatuc_label
            // 
            this.syncStatuc_label.AutoSize = true;
            this.syncStatuc_label.Location = new System.Drawing.Point(12, 255);
            this.syncStatuc_label.Name = "syncStatuc_label";
            this.syncStatuc_label.Size = new System.Drawing.Size(163, 13);
            this.syncStatuc_label.TabIndex = 11;
            this.syncStatuc_label.Text = "Данные не синхронизированы";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 277);
            this.Controls.Add(this.syncStatuc_label);
            this.Controls.Add(this.astbMax_label);
            this.Controls.Add(this.astbMin_label);
            this.Controls.Add(this.autosync_trackBar);
            this.Controls.Add(this.autosync_checkBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dateTimePicker1);
            this.Name = "Form1";
            this.Text = "синхронизатор календарей";
            ((System.ComponentModel.ISupportInitialize)(this.autosync_trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox autosync_checkBox;
        private System.Windows.Forms.TrackBar autosync_trackBar;
        private System.Windows.Forms.Label astbMin_label;
        private System.Windows.Forms.Label astbMax_label;
        private System.Windows.Forms.Timer autosync_timer;
        private System.Windows.Forms.Label syncStatuc_label;
    }
}

