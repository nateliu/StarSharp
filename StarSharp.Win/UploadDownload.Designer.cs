namespace StarSharp.Win
{
    partial class UploadDownload
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
            this.lbxRemoteLocation = new System.Windows.Forms.Label();
            this.lbxLocalLocation = new System.Windows.Forms.Label();
            this.tbxRemoteLocation = new System.Windows.Forms.TextBox();
            this.tbxLocalLocation = new System.Windows.Forms.TextBox();
            this.btnRemoteLocation = new System.Windows.Forms.Button();
            this.btnLocalLocation = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbxRemoteLocation
            // 
            this.lbxRemoteLocation.AutoSize = true;
            this.lbxRemoteLocation.Location = new System.Drawing.Point(45, 28);
            this.lbxRemoteLocation.Name = "lbxRemoteLocation";
            this.lbxRemoteLocation.Size = new System.Drawing.Size(88, 13);
            this.lbxRemoteLocation.TabIndex = 0;
            this.lbxRemoteLocation.Text = "Remote Location";
            // 
            // lbxLocalLocation
            // 
            this.lbxLocalLocation.AutoSize = true;
            this.lbxLocalLocation.Location = new System.Drawing.Point(45, 70);
            this.lbxLocalLocation.Name = "lbxLocalLocation";
            this.lbxLocalLocation.Size = new System.Drawing.Size(77, 13);
            this.lbxLocalLocation.TabIndex = 1;
            this.lbxLocalLocation.Text = "Local Location";
            // 
            // tbxRemoteLocation
            // 
            this.tbxRemoteLocation.Location = new System.Drawing.Point(138, 28);
            this.tbxRemoteLocation.Name = "tbxRemoteLocation";
            this.tbxRemoteLocation.Size = new System.Drawing.Size(281, 20);
            this.tbxRemoteLocation.TabIndex = 2;
            // 
            // tbxLocalLocation
            // 
            this.tbxLocalLocation.Location = new System.Drawing.Point(138, 63);
            this.tbxLocalLocation.Name = "tbxLocalLocation";
            this.tbxLocalLocation.Size = new System.Drawing.Size(281, 20);
            this.tbxLocalLocation.TabIndex = 3;
            // 
            // btnRemoteLocation
            // 
            this.btnRemoteLocation.Location = new System.Drawing.Point(425, 25);
            this.btnRemoteLocation.Name = "btnRemoteLocation";
            this.btnRemoteLocation.Size = new System.Drawing.Size(36, 23);
            this.btnRemoteLocation.TabIndex = 4;
            this.btnRemoteLocation.Text = "...";
            this.btnRemoteLocation.UseVisualStyleBackColor = true;
            this.btnRemoteLocation.Click += new System.EventHandler(this.btnRemoteLocation_Click);
            // 
            // btnLocalLocation
            // 
            this.btnLocalLocation.Location = new System.Drawing.Point(425, 61);
            this.btnLocalLocation.Name = "btnLocalLocation";
            this.btnLocalLocation.Size = new System.Drawing.Size(36, 23);
            this.btnLocalLocation.TabIndex = 5;
            this.btnLocalLocation.Text = "...";
            this.btnLocalLocation.UseVisualStyleBackColor = true;
            this.btnLocalLocation.Click += new System.EventHandler(this.btnLocalLocation_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(305, 109);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 6;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(386, 109);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDownload.TabIndex = 7;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // UploadDownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 153);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnLocalLocation);
            this.Controls.Add(this.btnRemoteLocation);
            this.Controls.Add(this.tbxLocalLocation);
            this.Controls.Add(this.tbxRemoteLocation);
            this.Controls.Add(this.lbxLocalLocation);
            this.Controls.Add(this.lbxRemoteLocation);
            this.Name = "UploadDownload";
            this.Text = "UploadDownload";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbxRemoteLocation;
        private System.Windows.Forms.Label lbxLocalLocation;
        private System.Windows.Forms.TextBox tbxRemoteLocation;
        private System.Windows.Forms.TextBox tbxLocalLocation;
        private System.Windows.Forms.Button btnRemoteLocation;
        private System.Windows.Forms.Button btnLocalLocation;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnDownload;
    }
}