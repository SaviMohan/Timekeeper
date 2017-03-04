using System;
using MySql.Data.MySqlClient;
using Xamarin.Forms;
using System.Collections.Generic;

namespace TimekeeperClientApp
{
    public class App : Application
    {
        private List<Data> data = new List<Data>();

        public App()
        {
            // The root page of your application
            var content = new ContentPage
            {
                Title = "TimekeeperClientApp",
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        new Label {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = "Welcome to Xamarin Forms!"
                        }
                    }
                }
            };

            MainPage = new NavigationPage(content);
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
