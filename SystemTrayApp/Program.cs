using System;
using System.Timers;
using System.Windows.Forms;

namespace SystemTrayApp
{
	/// <summary>
	/// 
	/// </summary>
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

            // Show the system tray icon.					
            using (ProcessIcon pi = new ProcessIcon())
			{
				pi.Display();

                // Make sure the application runs!
                Application.Run(new Sync());
            }
        }
    }

    class Sync : ApplicationContext
    {
        public Sync()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 5000;
            aTimer.Enabled = true;

            System.Diagnostics.Debug.WriteLine("Timer started");
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Hello World!");
            MessageBox.Show("The calculations are complete", "My Application", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
        }
    }
}