using Infrastructure;
using Logic.Configuration;
using System.Configuration;

namespace Desktop
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            Application.Run(new App(new Manager(), new HashingConfig(ConfigurationManager.AppSettings["pepper"]!)));
        }
    }
}