using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TimekeeperDisplayApp
{
    public partial class MainPage : ContentPage
    {
        private List<Data> data;

        public MainPage(DataStorage myDataStorage)
        {
            InitializeComponent();
            updateAndDisplay();
        }

        public void displayData()
        {
            string myString = "";
            foreach (Data item in data)
            {
                myString = myString + item.ToString() + "\n";
            }
            testLabel.Text = myString;
        }

        private async void updateAndDisplay()
        {
            await updateData();
            displayData();
        }

        private async Task updateData()
        {
            RestService myService = new RestService();
            data = await myService.RefreshDataAsync();
        }
    }
}
