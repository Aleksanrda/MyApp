using System;
using System.Windows.Forms;
using MyApp.Services.Factories.Implementations;

namespace MyApp.WinForm
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var serviceFactory = new ServiceFactory();
            var mainForm = new MainForm(serviceFactory);
            // Main relies upon the DataAccess in order to work
            Application.Run(mainForm);
        }
    }
}
