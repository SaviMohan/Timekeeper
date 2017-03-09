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
            ContentPage initialPage = new TimekeeperDisplayApp.UsersPage();
            pageHolder.Content = initialPage.Content;

            var tapUser = new TapGestureRecognizer();
            tapUser.Tapped += (s, e) =>
            {
                ContentPage usersPage = new TimekeeperDisplayApp.UsersPage();
                pageHolder.Content = usersPage.Content;
            };
            users.GestureRecognizers.Add(tapUser);

            var tapUsage = new TapGestureRecognizer();
            tapUsage.Tapped += (s, e) =>
            {
                ContentPage usagePage = new TimekeeperDisplayApp.UsagePage();
                pageHolder.Content = usagePage.Content;
            };
            appsUsage.GestureRecognizers.Add(tapUsage);

            var tapTrends = new TapGestureRecognizer();
            tapTrends.Tapped += (s, e) =>
            {
                ContentPage trendsPage = new TimekeeperDisplayApp.TrendsPage();
                pageHolder.Content = trendsPage.Content;
            };
            trends.GestureRecognizers.Add(tapTrends);

            var tapTasks = new TapGestureRecognizer();
            tapTasks.Tapped += (s, e) =>
            {
                ContentPage tasksPage = new TimekeeperDisplayApp.TasksPage();
                pageHolder.Content = tasksPage.Content;
            };
            tasks.GestureRecognizers.Add(tapTasks);

            var tapOptions = new TapGestureRecognizer();
            tapOptions.Tapped += (s, e) =>
            {
                ContentPage optionsPage = new TimekeeperDisplayApp.OptionsPage();
                pageHolder.Content = optionsPage.Content;
            };
            options.GestureRecognizers.Add(tapOptions);
        }
    }
}
