namespace StarSharp.Core.Utility.FormUtils
{
    partial class WaitingForm
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
            System.Windows.Forms.Panel groupBox1;
            this.label2 = new System.Windows.Forms.Label();
            this.CostTimeField = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.WaitingTimer = new System.Windows.Forms.Timer(this.components);
            groupBox1 = new System.Windows.Forms.Panel();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            groupBox1.Controls.Add(this.label2);
            groupBox1.Controls.Add(this.CostTimeField);
            groupBox1.Controls.Add(this.label1);
            groupBox1.Controls.Add(this.pictureBox1);
            groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox1.Location = new System.Drawing.Point(0, 0);
            groupBox1.Margin = new System.Windows.Forms.Padding(0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(284, 262);
            groupBox1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(91, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "    ";
            this.label2.Visible = false;
            // 
            // CostTimeField
            // 
            this.CostTimeField.AutoSize = true;
            this.CostTimeField.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CostTimeField.Location = new System.Drawing.Point(91, 43);
            this.CostTimeField.Name = "CostTimeField";
            this.CostTimeField.Size = new System.Drawing.Size(0, 13);
            this.CostTimeField.TabIndex = 2;
            this.CostTimeField.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(20, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Working...";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(282, 39);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // WaitingTimer
            // 
            this.WaitingTimer.Enabled = true;
            this.WaitingTimer.Interval = 1000;
            // 
            // WaitingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(groupBox1);
            this.Name = "WaitingForm";
            this.Text = "WaitingForm";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label CostTimeField;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer WaitingTimer;
    }
}