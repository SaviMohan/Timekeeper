using System;
using System.Reflection;
using System.Windows.Forms;

namespace SystemTrayApp
{
    partial class OptionBox : Form
    {
        public OptionBox()
        {
            InitializeComponent();
            this.Text = String.Format("{0} Options", AssemblyTitle);
            this.label1.Text = "User ID: (Current Value is: " + Properties.Settings.Default.userID + ")";
            this.label2.Text = "Current ID: (Current Value is: " + Properties.Settings.Default.companyID + ")";
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            int userBoxNum = 0;
            int companyBoxNum = 0;
            if (Int32.TryParse(textBox1.Text, out userBoxNum) && Int32.TryParse(textBox2.Text, out companyBoxNum)) 
            {
                if ((companyBoxNum > 99 && companyBoxNum < 1000) && (userBoxNum > 99 && userBoxNum < 1000))
                {
                    Properties.Settings.Default.companyID = companyBoxNum;
                    Properties.Settings.Default.userID = userBoxNum;
                    Properties.Settings.Default.Save();
                    this.Close();
                    return;
                }               
            }
            MessageBox.Show("Please enter your Company ID and User ID. Both should be a 3 digit number.");
        }
    }
}