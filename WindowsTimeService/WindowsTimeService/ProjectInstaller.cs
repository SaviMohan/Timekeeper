using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace WindowsTimeService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        protected override void OnBeforeInstall(IDictionary savedState) {
            string parameter = "MySource1\" \"MyLogFile1";
            Context.Parameters["assemblypath"] = "\"" + Context.Parameters["assemblypath"] + "\" \"" + parameter + "\""; base.OnBeforeInstall(savedState);
        }

        protected override void OnAfterInstall(IDictionary savedState)
        {
            base.OnAfterInstall(savedState);
            //Get the service by its name 
            ServiceController theController = new ServiceController("WindowsTimeService");
            ConnectionOptions coOptions = new ConnectionOptions();
            coOptions.Impersonation = ImpersonationLevel.Impersonate;
            ManagementScope mgmtScope = new System.Management.ManagementScope(@"root\CIMV2", coOptions);
            mgmtScope.Connect();
            ManagementObject wmiService;
            wmiService = new ManagementObject("Win32_Service.Name='" + theController.ServiceName + "'");
            ManagementBaseObject InParam = wmiService.GetMethodParameters("Change");

            // Setting the interaction with the desktop
            InParam["DesktopInteract"] = true;

            ManagementBaseObject OutParam = wmiService.InvokeMethod("Change", InParam, null);

            theController.Start();  
    }

        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }
}
