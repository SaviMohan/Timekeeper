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
            updateAndDisplay(myDataStorage);
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

        private async void updateAndDisplay(DataStorage myDataStorage)
        {
            await updateData();
            displayData();
            foreach (Data item in data)
            {
                int pos = userExists(item, myDataStorage);
                if (pos == -1)
                {
                    myDataStorage.userList.Add(new TimekeeperDisplayApp.User(item, myDataStorage));
                }
                else
                {
                    myDataStorage.userList[pos].addToLog(item);
                }
            }
            foreach (User user in myDataStorage.userList)
            {
                System.Diagnostics.Debug.WriteLine(user.ToString());
            }

        }

        private async Task updateData()
        {
            RestService myService = new RestService();
            data = await myService.RefreshDataAsync();
        }

        private int userExists(Data myData, DataStorage myDataStorage)
        {
            int pos = 0;
            foreach (User user in myDataStorage.userList)
            {
                if (user.userID == myData.userID)
                {
                    return pos;
                }
                pos++;         
            }
            return -1;
        }

    }
}
