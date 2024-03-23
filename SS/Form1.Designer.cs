namespace SS
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
            this.StartScreenshotTimer = new System.Windows.Forms.Timer(this.components);
            this.EnableBtn = new System.Windows.Forms.Button();
            this.LblFPSIssue = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // StartScreenshotTimer
            // 
            this.StartScreenshotTimer.Interval = 1000;
            this.StartScreenshotTimer.Tick += new System.EventHandler(this.StartScreenshotTimer_Tick);
            // 
            // EnableBtn
            // 
            this.EnableBtn.Location = new System.Drawing.Point(49, 119);
            this.EnableBtn.Name = "EnableBtn";
            this.EnableBtn.Size = new System.Drawing.Size(267, 23);
            this.EnableBtn.TabIndex = 0;
            this.EnableBtn.Text = "Start Screenshots On Timer";
            this.EnableBtn.UseVisualStyleBackColor = true;
            this.EnableBtn.Click += new System.EventHandler(this.EnableBtn_Click);
            // 
            // LblFPSIssue
            // 
            this.LblFPSIssue.AutoSize = true;
            this.LblFPSIssue.Location = new System.Drawing.Point(12, 9);
            this.LblFPSIssue.Name = "LblFPSIssue";
            this.LblFPSIssue.Size = new System.Drawing.Size(210, 13);
            this.LblFPSIssue.TabIndex = 1;
            this.LblFPSIssue.Text = "Causing FPS Issues? Adjust Interval : 1000";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(15, 25);
            this.trackBar1.Maximum = 5000;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(337, 45);
            this.trackBar1.TabIndex = 2;
            this.trackBar1.Value = 1000;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 154);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.LblFPSIssue);
            this.Controls.Add(this.EnableBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer StartScreenshotTimer;
        private System.Windows.Forms.Button EnableBtn;
        private System.Windows.Forms.Label LblFPSIssue;
        private System.Windows.Forms.TrackBar trackBar1;
    }
}

