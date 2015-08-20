using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StarSharp.Core.Utility.FormUtils
{
    public partial class WaitingForm : Form
    {
        int MaxWaiting = 5;
        int times = 1;

        public WaitingForm()
        {
            InitializeComponent();
            WaitingTimer.Start();
        }

        private void WaitingTimer_Tick(object sender, EventArgs e)
        {
            try { CostTimeField.Text = times.ToString(); }
            catch { }
            Application.DoEvents();

            if (times++ >= MaxWaiting)
            {
                try { this.WaitingTimer.Stop(); }
                catch { }
                try
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new WinFormHelper.InvodeMethodhandler(Hide_Form), new object[] { this, "Hide", null });
                    }
                    else
                    {
                        this.Hide();
                    }
                }
                catch { }
            }
        }

        void Hide_Form(Form f, string methodName, params object[] parms)
        {
            WinFormHelper.InvokeMethod(f, methodName, parms);
        }

        public void HideForm()
        {
            label1_Click(null, null);
        }

        private void WaitingForm_Deactivate(object sender, EventArgs e)
        {
            HideForm();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new WinFormHelper.InvodeMethodhandler(Hide_Form), new object[] { this, "Hide", null });
                }
                else
                {
                    this.Hide();
                }
            }
            catch { }
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new WinFormHelper.InvodeMethodhandler(Hide_Form), new object[] { this, "Hide", null });
                }
                else
                {
                    this.Hide();
                }
            }
            catch { }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            label1_Click(sender, e);
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            label2.Visible = true;
        }
    }
}
