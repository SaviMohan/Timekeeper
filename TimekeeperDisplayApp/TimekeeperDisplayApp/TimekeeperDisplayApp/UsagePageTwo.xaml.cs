using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TimekeeperDisplayApp
{
    public partial class UsagePageTwo : ContentPage
    {
        public UsagePageTwo(DataStorage myDataStorage, AppData app)
        {
            InitializeComponent();
            appName.Text = app.getName() + ":";
        }
    }
}
