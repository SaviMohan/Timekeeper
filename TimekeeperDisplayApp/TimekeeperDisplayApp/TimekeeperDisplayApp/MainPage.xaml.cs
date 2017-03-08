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
            addTapHandlers();
        }

        public void displayData(DataStorage myDataStorage)
        {
            string myString = "";
            foreach (Data item in myDataStorage.getDataList())
            {
                myString = myString + item.ToString() + "\n";
            }      
        }

        private async void updateAndDisplay(DataStorage myDataStorage)
        {
            await updateData(myDataStorage);
            displayData(myDataStorage);
            foreach (Data item in myDataStorage.getDataList())
            {
                int pos = userExists(item, myDataStorage);
                if (pos == -1)
                {
                    myDataStorage.addToUserList(new TimekeeperDisplayApp.User(item, myDataStorage));
                }
                else
                {
                    myDataStorage.getUserList()[pos].addToLog(item, myDataStorage);
                }
            }
            foreach (User user in myDataStorage.getUserList())
            {
                System.Diagnostics.Debug.WriteLine(user.ToString());
            }

        }

        private async Task updateData(DataStorage myDataStorage)
        {
            RestService myService = new RestService();
            myDataStorage.setDataList(await myService.RefreshDataAsync());
        }

        private int userExists(Data myData, DataStorage myDataStorage)
        {
            int pos = 0;
            foreach (User user in myDataStorage.getUserList())
            {
                if (user.getUserID() == myData.userID)
                {
                    return pos;
                }
                pos++;         
            }
            return -1;
        }

        private async void OnButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            await Navigation.PushModalAsync(new Grid1());
        }

        private void addTapHandlers()
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                Navigation.PushModalAsync(new Grid1());
            };
            users.GestureRecognizers.Add(tapGestureRecognizer);
            appsUsage.GestureRecognizers.Add(tapGestureRecognizer);
            trends.GestureRecognizers.Add(tapGestureRecognizer);
            tasks.GestureRecognizers.Add(tapGestureRecognizer);
            options.GestureRecognizers.Add(tapGestureRecognizer);
        }
    }
}
