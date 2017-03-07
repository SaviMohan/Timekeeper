using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Windows.Automation;

namespace SystemTrayApp
{
	static class Program
	{
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
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
        [DllImport("user32.dll")]
        static extern int GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

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
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (Properties.Settings.Default.companyID < 100 || Properties.Settings.Default.userID < 100)
            {
                return;
            }
            string title = GetActiveWindowTitle();
            title = title.Replace("\"", "").Replace("'", "");
            string titleShort = title;
            if (title.Length > 92)
            {
                int difference = title.Length - 46;
                titleShort = title.Substring(0, 46) + " / " + title.Substring(title.Length-46, 46);
            }
            Data newData = new Data(titleShort, Properties.Settings.Default.companyID, Properties.Settings.Default.userID);
            string json = JsonConvert.SerializeObject(newData);
            System.Diagnostics.Debug.WriteLine(json);
            titles.Add(json);
        }

        private void OnTimedEventTwo(object source, ElapsedEventArgs e)
        {
            updateDB();
        }
        
        private static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            IntPtr processID = Marshal.AllocHGlobal(100);
            GetWindowThreadProcessId(handle, processID);
            Process mainProcess = Process.GetProcessById(Marshal.ReadInt32(processID));

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                string title = Buff.ToString();
                if (mainProcess.MainModule.ModuleName == "Explorer.EXE")
                {
                    return title + " - Windows Explorer";
                }
                else if (mainProcess.MainModule.ModuleName == "iexplore.exe")
                {
                    return getURL(mainProcess, handle, title + " - iexplore.exe");
                }
                else if (mainProcess.MainModule.ModuleName == "chrome.exe")
                {
                    return getURL(mainProcess, handle, title + " - chrome.exe");
                }
                else if (mainProcess.MainModule.ModuleName == "firefox.exe")
                {
                    return getURL(mainProcess, handle, title + " - firefox.exe");
                } 
                else
                {
                    return title;
                }                     
            }
            return "Nothing Detected";
        }

        private void updateDB()
        {
            string constring = "Database=timekeeperdata;Data Source=au-cdbr-azure-southeast-a.cloudapp.net;User Id=b36cbbf06103db;Password=3be84fed";

            int len = titles.Count;
            for (int i=0;i<len;i++)
            {
                string query = "INSERT INTO timekeeperdata.test3(data) VALUES('" + titles[0] + "');";
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

        private static string getURL(Process myProcess, IntPtr handle, string windowTitle)
        {
            if (windowTitle.Contains("chrome.exe"))
            {
                return extractURL(myProcess, handle, windowTitle, "Google Chrome");
            }
            else if (windowTitle.Contains("iexplore.exe"))
            {
                return extractURL(myProcess, handle, windowTitle, "Internet Explorer");
            }
            else if (windowTitle.Contains("firefox.exe"))
            {
                return extractURL(myProcess, handle, windowTitle, "Mozilla Firefox");
            }
            return windowTitle;
        }

        private static string extractURL(Process myProcess, IntPtr handle, string windowTitle, string browser)
        {
            AutomationElement element = AutomationElement.FromHandle(handle);
            if (element == null)
                return "Unidentified Website - " + browser;
            Condition conditions = new AndCondition(
                new PropertyCondition(AutomationElement.ProcessIdProperty, myProcess.Id),
                new PropertyCondition(AutomationElement.IsControlElementProperty, true),
                new PropertyCondition(AutomationElement.IsContentElementProperty, true),
                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit));

            AutomationElement elementx = element.FindFirst(TreeScope.Descendants, conditions);
            return ((ValuePattern)elementx.GetCurrentPattern(ValuePattern.Pattern)).Current.Value as string + " - " + browser;
        }
    }
}