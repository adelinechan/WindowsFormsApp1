namespace WindowsFormsApp1
{
    partial class Maintainance
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Maintainance));
            this.Camera = new System.Windows.Forms.Label();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.vlcControl = new Vlc.DotNet.Forms.VlcControl();
            this.btnStream = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.vlcControl)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Camera
            // 
            this.Camera.AccessibleName = "Camera";
            this.Camera.AutoSize = true;
            this.Camera.Location = new System.Drawing.Point(20, 26);
            this.Camera.Name = "Camera";
            this.Camera.Size = new System.Drawing.Size(72, 21);
            this.Camera.TabIndex = 2;
            this.Camera.Text = "Camera";
            this.Camera.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(16, 211);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(93, 32);
            this.btnPause.TabIndex = 4;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(16, 261);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(93, 32);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "S&top";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // vlcControl
            // 
            this.vlcControl.BackColor = System.Drawing.Color.Black;
            this.vlcControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vlcControl.Location = new System.Drawing.Point(0, 0);
            this.vlcControl.Name = "vlcControl";
            this.vlcControl.Padding = new System.Windows.Forms.Padding(1);
            this.vlcControl.Size = new System.Drawing.Size(1014, 508);
            this.vlcControl.Spu = -1;
            this.vlcControl.TabIndex = 6;
            this.vlcControl.Text = "vlcControl1";
            this.vlcControl.VlcLibDirectory = ((System.IO.DirectoryInfo)(resources.GetObject("vlcControl.VlcLibDirectory")));
            this.vlcControl.VlcMediaplayerOptions = null;
            this.vlcControl.Click += new System.EventHandler(this.vlcControl1_Click);
            // 
            // btnStream
            // 
            this.btnStream.Location = new System.Drawing.Point(16, 161);
            this.btnStream.Name = "btnStream";
            this.btnStream.Size = new System.Drawing.Size(93, 32);
            this.btnStream.TabIndex = 7;
            this.btnStream.Text = "&Stream";
            this.btnStream.UseVisualStyleBackColor = true;
            this.btnStream.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPause);
            this.panel1.Controls.Add(this.btnStream);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(121, 508);
            this.panel1.TabIndex = 8;
            // 
            // Maintainance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.ClientSize = new System.Drawing.Size(1014, 508);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.vlcControl);
            this.Controls.Add(this.Camera);
            this.Font = new System.Drawing.Font("Century Schoolbook", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Maintainance";
            this.Load += new System.EventHandler(this.Maintainance_Load);
            ((System.ComponentModel.ISupportInitialize)(this.vlcControl)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Camera;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStop;
        private Vlc.DotNet.Forms.VlcControl vlcControl;
        private System.Windows.Forms.Button btnStream;
        private System.Windows.Forms.Panel panel1;
    }
}
