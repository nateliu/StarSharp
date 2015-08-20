namespace StarSharp.Win
{
    partial class BasicForm
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
            this.userContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // userContextMenuStrip
            // 
            this.userContextMenuStrip.Name = "userContextMenuStrip";
            this.userContextMenuStrip.Size = new System.Drawing.Size(153, 26);
            // 
            // BasicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "BasicForm";
            this.Text = "BasicForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip userContextMenuStrip;
    }
}