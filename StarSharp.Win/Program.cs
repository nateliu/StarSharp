using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StarSharp.Win
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Control.CheckForIllegalCrossThreadCalls = false;
            Application.Run(new MainForm());

            //DateTime dt = new DateTime(2013, 5, 6);
            //MessageBox.Show(Result(dt, 8).ToString());
        }

        private static DateTime Result(DateTime dt, int n)
        {
            DateTime temp = dt;
            while (n-- > 0)
            {
                temp = temp.AddDays(1);
                while (temp.DayOfWeek == System.DayOfWeek.Saturday || temp.DayOfWeek == System.DayOfWeek.Sunday)
                    temp = temp.AddDays(1);
            }
            return temp;
        }

    }
}
