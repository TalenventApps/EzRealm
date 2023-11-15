using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzRealm
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static bool hasAgreed;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Show the Terms of Service agreement message box
            Application.Run(new Terms());
            if (hasAgreed == true)
            {
                // User agreed, allow access to the application
                // You can open the main form or perform any other necessary actions here
                Application.Run(new Oxygen());//new Base());
            }
            else
            {
                // User did not agree, close the application
                Application.Exit();
            }
            
        }
    }
}
