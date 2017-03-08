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
        private bool usersSelected;
        private bool usageSelected;
        private bool trendsSelected;
        private bool tasksSelected;
        private bool optionsSelected;

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
            usersSelected = true;
            usageSelected = false;
            trendsSelected = false;
            tasksSelected = false;
            optionsSelected = false;

            var tapUser = new TapGestureRecognizer();
            tapUser.Tapped += (s, e) =>
            {
                if (!usersSelected)
                {
                    usersSelected = true;
                    usageSelected = false;
                    trendsSelected = false;
                    tasksSelected = false;
                    optionsSelected = false;
                }
                Navigation.PushModalAsync(new Grid1());
            };
            users.GestureRecognizers.Add(tapUser);

            var tapUsage = new TapGestureRecognizer();
            tapUsage.Tapped += (s, e) =>
            {
                Navigation.PushModalAsync(new Grid1());
                if (!usageSelected)
                {
                    usersSelected = false;
                    usageSelected = true;
                    trendsSelected = false;
                    tasksSelected = false;
                    optionsSelected = false;
                }
            };
            appsUsage.GestureRecognizers.Add(tapUsage);

            var tapTrends = new TapGestureRecognizer();
            tapTrends.Tapped += (s, e) =>
            {
                if (!trendsSelected)
                {
                    usersSelected = false;
                    usageSelected = false;
                    trendsSelected = true;
                    tasksSelected = false;
                    optionsSelected = false;
                }
                Navigation.PushModalAsync(new Grid1());
            };
            trends.GestureRecognizers.Add(tapTrends);

            var tapTasks = new TapGestureRecognizer();
            tapTasks.Tapped += (s, e) =>
            {
                if (!tasksSelected)
                {
                    usersSelected = false;
                    usageSelected = false;
                    trendsSelected = false;
                    tasksSelected = true;
                    optionsSelected = false;
                }
                Navigation.PushModalAsync(new Grid1());
            };
            tasks.GestureRecognizers.Add(tapTasks);

            var tapOptions = new TapGestureRecognizer();
            tapOptions.Tapped += (s, e) =>
            {
                if (!optionsSelected)
                {
                    usersSelected = false;
                    usageSelected = false;
                    trendsSelected = false;
                    tasksSelected = false;
                    optionsSelected = true;
                }
                Navigation.PushModalAsync(new Grid1());
            };
            options.GestureRecognizers.Add(tapOptions);
        }
    }
}
