namespace StarSharp.Win
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MessageNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.MessageContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.DMMenu = new System.Windows.Forms.MenuStrip();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.treeTitleLabel = new System.Windows.Forms.Label();
            this.DMNavigation = new System.Windows.Forms.TreeView();
            this.mainContentSplit = new System.Windows.Forms.SplitContainer();
            this.DMToolbar = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.menuImageList = new System.Windows.Forms.ImageList(this.components);
            this.mainMenuImageList = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainContentSplit)).BeginInit();
            this.mainContentSplit.Panel1.SuspendLayout();
            this.mainContentSplit.SuspendLayout();
            this.DMToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // MessageNotifyIcon
            // 
            this.MessageNotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.MessageNotifyIcon.BalloonTipText = "DataManager Message";
            this.MessageNotifyIcon.ContextMenuStrip = this.MessageContextMenu;
            this.MessageNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("MessageNotifyIcon.Icon")));
            this.MessageNotifyIcon.Text = "DataManager Message";
            // 
            // MessageContextMenu
            // 
            this.MessageContextMenu.Name = "MessageContextMenu";
            this.MessageContextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2MinSize = 11;
            this.splitContainer1.Size = new System.Drawing.Size(643, 532);
            this.splitContainer1.SplitterDistance = 60;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer3.Panel1.Controls.Add(this.panel1);
            this.splitContainer3.Panel1.Controls.Add(this.pictureBox1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.DMMenu);
            this.splitContainer3.Panel2MinSize = 20;
            this.splitContainer3.Size = new System.Drawing.Size(643, 60);
            this.splitContainer3.SplitterDistance = 31;
            this.splitContainer3.SplitterWidth = 1;
            this.splitContainer3.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Red;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(643, 5);
            this.panel1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(13, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(305, 37);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // DMMenu
            // 
            this.DMMenu.BackColor = System.Drawing.SystemColors.MenuBar;
            this.DMMenu.Location = new System.Drawing.Point(0, 0);
            this.DMMenu.Name = "DMMenu";
            this.DMMenu.Size = new System.Drawing.Size(643, 24);
            this.DMMenu.TabIndex = 0;
            this.DMMenu.Text = "menuStrip1";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer4);
            this.splitContainer2.Panel1MinSize = 0;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.mainContentSplit);
            this.splitContainer2.Size = new System.Drawing.Size(643, 471);
            this.splitContainer2.SplitterDistance = 113;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer4.IsSplitterFixed = true;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.BackColor = System.Drawing.Color.Gray;
            this.splitContainer4.Panel1.Controls.Add(this.treeTitleLabel);
            this.splitContainer4.Panel1MinSize = 0;
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.DMNavigation);
            this.splitContainer4.Panel2MinSize = 20;
            this.splitContainer4.Size = new System.Drawing.Size(113, 471);
            this.splitContainer4.SplitterDistance = 25;
            this.splitContainer4.SplitterWidth = 1;
            this.splitContainer4.TabIndex = 0;
            // 
            // treeTitleLabel
            // 
            this.treeTitleLabel.AutoSize = true;
            this.treeTitleLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.treeTitleLabel.ForeColor = System.Drawing.Color.White;
            this.treeTitleLabel.Location = new System.Drawing.Point(12, 3);
            this.treeTitleLabel.Name = "treeTitleLabel";
            this.treeTitleLabel.Size = new System.Drawing.Size(39, 13);
            this.treeTitleLabel.TabIndex = 0;
            this.treeTitleLabel.Text = "Start";
            // 
            // DMNavigation
            // 
            this.DMNavigation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DMNavigation.HideSelection = false;
            this.DMNavigation.ItemHeight = 16;
            this.DMNavigation.Location = new System.Drawing.Point(0, 0);
            this.DMNavigation.Margin = new System.Windows.Forms.Padding(5);
            this.DMNavigation.Name = "DMNavigation";
            this.DMNavigation.ShowNodeToolTips = true;
            this.DMNavigation.Size = new System.Drawing.Size(113, 445);
            this.DMNavigation.TabIndex = 0;
            // 
            // mainContentSplit
            // 
            this.mainContentSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContentSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.mainContentSplit.IsSplitterFixed = true;
            this.mainContentSplit.Location = new System.Drawing.Point(0, 0);
            this.mainContentSplit.Name = "mainContentSplit";
            this.mainContentSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // mainContentSplit.Panel1
            // 
            this.mainContentSplit.Panel1.Controls.Add(this.DMToolbar);
            this.mainContentSplit.Panel1MinSize = 20;
            this.mainContentSplit.Panel2MinSize = 20;
            this.mainContentSplit.Size = new System.Drawing.Size(529, 471);
            this.mainContentSplit.SplitterDistance = 25;
            this.mainContentSplit.SplitterWidth = 1;
            this.mainContentSplit.TabIndex = 0;
            // 
            // DMToolbar
            // 
            this.DMToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.DMToolbar.Location = new System.Drawing.Point(0, 0);
            this.DMToolbar.Name = "DMToolbar";
            this.DMToolbar.Size = new System.Drawing.Size(529, 25);
            this.DMToolbar.TabIndex = 0;
            this.DMToolbar.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Visible = false;
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.Visible = false;
            // 
            // menuImageList
            // 
            this.menuImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.menuImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.menuImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // mainMenuImageList
            // 
            this.mainMenuImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.mainMenuImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.mainMenuImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 532);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.mainContentSplit.Panel1.ResumeLayout(false);
            this.mainContentSplit.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainContentSplit)).EndInit();
            this.mainContentSplit.ResumeLayout(false);
            this.DMToolbar.ResumeLayout(false);
            this.DMToolbar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon MessageNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip MessageContextMenu;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip DMMenu;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Label treeTitleLabel;
        private System.Windows.Forms.SplitContainer mainContentSplit;
        private System.Windows.Forms.ToolStrip DMToolbar;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ImageList menuImageList;
        private System.Windows.Forms.ImageList mainMenuImageList;
        private System.Windows.Forms.TreeView DMNavigation;
    }
}

