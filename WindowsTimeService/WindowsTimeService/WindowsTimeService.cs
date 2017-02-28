using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using UserProcess;
using System.Threading;
using System.Xml;
using System.IO;
using System.Security.Principal;
using System.Xml.XPath;

namespace WindowsTimeService
{
    public partial class WindowsTimeService : ServiceBase
    {

        public WindowsTimeService(string[] args)
        {
            InitializeComponent();

            string eventSourceName = "MySource";
            string logName = "MyNewLog";
            if (args.Count() > 0) {
                eventSourceName = args[0];
            }
            if (args.Count() > 1) {
                logName = args[1];
            }

            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("MySource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "MySource", "MyNewLog");
            }
            eventLog1.Source = "MySource";
            eventLog1.Log = "MyNewLog";
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);

        private static int eventId = 0;

        protected override void OnStart(string[] args)
        {
            // Update the service state to Start Pending.  
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            eventLog1.WriteEntry("In OnStart");
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 10000; // 10 seconds  
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();

            // Update the service state to Running.  
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            ProcessStarter sampleProcess = new ProcessStarter();
            sampleProcess.ProcessName = "sample";
            sampleProcess.ProcessPath = @"C:\Base\sample.exe";
            sampleProcess.Run();
            sampleProcess.createXML();
        }

        protected override void OnStop()
        {
            // Update the service state to Stop Pending.  
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOP_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            eventLog1.WriteEntry("In onStop.");

            // Update the service state to Stopped.  
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {

            IntPtr currentUserToken = ProcessStarter.GetCurrentUserToken();
            WindowsIdentity currentUserId = new WindowsIdentity(currentUserToken);
            WindowsImpersonationContext impersonatedUser = currentUserId.Impersonate();
            String myDocumentsPath = "C:/";
            impersonatedUser.Undo();

            String sampleXmlFilePath = Path.Combine(myDocumentsPath,"sample.xml");
            XmlDocument oXmlDocument = new XmlDocument();
            oXmlDocument.Load(sampleXmlFilePath);

            XPathNavigator oPathNavigator = oXmlDocument.CreateNavigator();
            XPathNodeIterator oNodeIterator = oPathNavigator.Select("/SampleElement/Data");
            oNodeIterator.MoveNext();

            String receivedData = oNodeIterator.Current.Value;
            eventLog1.WriteEntry(receivedData, EventLogEntryType.Information, eventId++);
        }

        protected override void OnContinue()
        {
            eventLog1.WriteEntry("In OnContinue.");
        }

    }
}
