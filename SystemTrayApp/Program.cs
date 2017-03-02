using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Newtonsoft.Json;

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

        private List<string> titles = new List<string>();

        public Sync()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 5000;
            aTimer.Enabled = true;

            System.Timers.Timer bTimer = new System.Timers.Timer();
            bTimer.Elapsed += new ElapsedEventHandler(OnTimedEventTwo);
            bTimer.Interval = 30000;
            bTimer.Enabled = true;      

            System.Diagnostics.Debug.WriteLine("Timers started");
       
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            string title = GetActiveWindowTitle();
            string titleShort = title;
            if (title.Length > 50)
            {
                int difference = title.Length - 50;
                titleShort = title.Substring(0, 25) + " / " + title.Substring(title.Length-25, 25);
            }
            Data newData = new Data(titleShort, 001, 001);
            string json = JsonConvert.SerializeObject(newData);
            System.Diagnostics.Debug.WriteLine(json);
            titles.Add(json);
        }

        private void OnTimedEventTwo(object source, ElapsedEventArgs e)
        {
            updateDB();
        }

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return "Nothing Detected";
        }

        private void updateDB()
        {
            string constring = "Database=timekeeperdata;Data Source=au-cdbr-azure-southeast-a.cloudapp.net;User Id=b36cbbf06103db;Password=3be84fed";

            int len = titles.Count;
            for (int i=0;i<len;i++)
            {
                string query = "INSERT INTO timekeeperdata.test2(test_titles) VALUES('" + titles[0] + "');";
                MySqlConnection conDatabase = new MySqlConnection(constring);
                MySqlCommand cmdDatabase = new MySqlCommand(query, conDatabase);
                MySqlDataReader myReader;

                try
                {
                    conDatabase.Open();
                    myReader = cmdDatabase.ExecuteReader();
                    System.Diagnostics.Debug.WriteLine("SAVED");
                    while (myReader.Read())
                    {

                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }

                titles.RemoveAt(0);        
                cmdDatabase.Connection.Close();
                conDatabase.Close();
            }
            
        }

    }
}