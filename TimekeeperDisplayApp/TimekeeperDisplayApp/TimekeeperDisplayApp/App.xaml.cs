using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace TimekeeperDisplayApp
{
    public partial class App : Application
    {
        private List<Data> data = new List<Data>();

        public App()
        {
            InitializeComponent();

            MainPage = new TimekeeperDisplayApp.MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            updateData();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private async void updateData()
        {
            RestService myService = new RestService();
            data = await myService.RefreshDataAsync();
            foreach (Data item in data)
            {
                System.Diagnostics.Debug.WriteLine(item.ToString());
            }
        }
    }
}
