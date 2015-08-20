using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StarSharp.Core.Utility.FormUtils
{
    public class WaitingQueue
    {
        static Dictionary<string, WaitingQueue> _list = new Dictionary<string, WaitingQueue>();
        public static WaitingQueue Create(string category)
        {
            if (string.IsNullOrEmpty(category))
            {
                category = "DataManager.NET";
            }
            if (!_list.ContainsKey(category))
            {
                _list[category] = new WaitingQueue();
            }
            return _list[category];
        }

        private WaitingQueue() { }

        WaitingForm theForm;
        Thread theThread;
        ThreadWaiting arg;
        public void Show()
        {
            try
            {
                arg = new ThreadWaiting();
                arg.theForm = theForm;
                ThreadStart ts = new ThreadStart(arg.Run);
                theThread = new Thread(ts);
                theThread.Name = "Waiting";
                theThread.Start();
                theForm = arg.theForm;
            }
            catch { }
        }

        class ThreadWaiting
        {
            public WaitingForm theForm;
            public void Run()
            {
                if (theForm == null || theForm.IsDisposed)
                {
                    theForm = new WaitingForm();
                    theForm.WindowState = FormWindowState.Normal;
                    theForm.StartPosition = FormStartPosition.CenterScreen;
                    theForm.Text = string.Empty;
                    theForm.ControlBox = false;
                    theForm.FormBorderStyle = FormBorderStyle.None;
                }
                try
                {
                    Application.Run(theForm);
                }
                catch { }
            }
        }

        public void Close()
        {
            if (arg == null)
                return;
            try
            {
                if (arg != null && arg.theForm != null)
                {
                    arg.theForm.HideForm();
                }
            }
            catch { }
            arg = null;
        }

        public WaitingForm Get()
        {
            return arg.theForm;
        }
    }

}
