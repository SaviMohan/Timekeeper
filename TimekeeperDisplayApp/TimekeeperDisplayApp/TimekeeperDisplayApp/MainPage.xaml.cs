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

        public MainPage(DataStorage myDataStorage)
        {
            InitializeComponent();
            updateAndDisplay(myDataStorage);
        }

        public void displayData(DataStorage myDataStorage)
        {
            string myString = "";
            foreach (Data item in myDataStorage.dataList)
            {
                myString = myString + item.ToString() + "\n";
            }
            testLabel.Text = myString;
        }

        private async void updateAndDisplay(DataStorage myDataStorage)
        {
            await updateData(myDataStorage);
            displayData(myDataStorage);
            foreach (Data item in myDataStorage.dataList)
            {
                int pos = userExists(item, myDataStorage);
                if (pos == -1)
                {
                    myDataStorage.userList.Add(new TimekeeperDisplayApp.User(item, myDataStorage));
                }
                else
                {
                    myDataStorage.userList[pos].addToLog(item, myDataStorage);
                }
            }
            foreach (User user in myDataStorage.userList)
            {
                System.Diagnostics.Debug.WriteLine(user.ToString());
            }

        }

        private async Task updateData(DataStorage myDataStorage)
        {
            RestService myService = new RestService();
            myDataStorage.dataList = await myService.RefreshDataAsync();
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
