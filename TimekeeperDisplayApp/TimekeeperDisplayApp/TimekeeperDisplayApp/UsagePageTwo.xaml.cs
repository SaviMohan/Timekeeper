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
        ContentView pageHolder;
        DataStorage dataStorage;
        AppData myApp;

        public UsagePageTwo(DataStorage myDataStorage, AppData app, ContentView myPageHolder)
        {
            InitializeComponent();
            appName.Text = app.getName() + ":";
            pageHolder = myPageHolder;
            dataStorage = myDataStorage;
            myApp = app;

            classPicker.Items.Add("Productive");
            classPicker.Items.Add("Unproductive");
        }

        void OnPickerChanged(object sender, EventArgs args)
        {
            if (classPicker.SelectedIndex == 0)
            {
                dataStorage.setAllApps(myApp.getName(), "Productive");
            }
            else if (classPicker.SelectedIndex == 1)
            {
                dataStorage.setAllApps(myApp.getName(), "Unproductive");
            }
            else
            {
                
            }
        }

        void OnButtonClicked(object sender, EventArgs args)
        {
            ContentPage newPage = new TimekeeperDisplayApp.UsagePage(dataStorage, pageHolder);
            pageHolder.Content = newPage.Content;
        }
    }
}
