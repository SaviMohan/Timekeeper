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
            addTapHandlers(myDataStorage);
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

        private void addTapHandlers(DataStorage myDataStorage)
        {
            ContentPage initialPage = new TimekeeperDisplayApp.UsersPage(myDataStorage, pageHolder);
            pageHolder.Content = initialPage.Content;
            usersView.BackgroundColor = Color.FromHex("#0080ff");
            users.BackgroundColor = Color.FromHex("#0080ff");
            var tapUser = new TapGestureRecognizer();
            tapUser.Tapped += (s, e) =>
            {
                ContentPage usersPage = new TimekeeperDisplayApp.UsersPage(myDataStorage, pageHolder);
                pageHolder.Content = usersPage.Content;
                resetToGrey();
                usersView.BackgroundColor = Color.FromHex("#0080ff");
                users.BackgroundColor = Color.FromHex("#0080ff");
                myDataStorage.sortUsersBy("name");
            };
            users.GestureRecognizers.Add(tapUser);

            var tapUsage = new TapGestureRecognizer();
            tapUsage.Tapped += (s, e) =>
            {
                ContentPage usagePage = new TimekeeperDisplayApp.UsagePage(myDataStorage, pageHolder);
                pageHolder.Content = usagePage.Content;
                resetToGrey();
                appsView.BackgroundColor = Color.FromHex("#0080ff");
                appsUsage.BackgroundColor = Color.FromHex("#0080ff");
                myDataStorage.sortUsersBy("userid");
            };
            appsUsage.GestureRecognizers.Add(tapUsage);

            var tapTrends = new TapGestureRecognizer();
            tapTrends.Tapped += (s, e) =>
            {
                ContentPage trendsPage = new TimekeeperDisplayApp.TrendsPage();
                pageHolder.Content = trendsPage.Content;
                resetToGrey();
                trendsView.BackgroundColor = Color.FromHex("#0080ff");
                trends.BackgroundColor = Color.FromHex("#0080ff");
                myDataStorage.sortUsersBy("companyid");
            };
            trends.GestureRecognizers.Add(tapTrends);

            var tapTasks = new TapGestureRecognizer();
            tapTasks.Tapped += (s, e) =>
            {
                ContentPage tasksPage = new TimekeeperDisplayApp.TasksPage();
                pageHolder.Content = tasksPage.Content;
                resetToGrey();
                tasksView.BackgroundColor = Color.FromHex("#0080ff");
                tasks.BackgroundColor = Color.FromHex("#0080ff");
                myDataStorage.sortUsersBy("productivetime");
            };
            tasks.GestureRecognizers.Add(tapTasks);

            var tapOptions = new TapGestureRecognizer();
            tapOptions.Tapped += (s, e) =>
            {
                ContentPage optionsPage = new TimekeeperDisplayApp.OptionsPage();
                pageHolder.Content = optionsPage.Content;
                resetToGrey();
                optionsView.BackgroundColor = Color.FromHex("#0080ff");
                options.BackgroundColor = Color.FromHex("#0080ff");
                myDataStorage.sortUsersBy("mostused");
            };
            options.GestureRecognizers.Add(tapOptions);
        }
        private void resetToGrey()
        {
            usersView.BackgroundColor = Color.FromHex("#808080");
            users.BackgroundColor = Color.FromHex("#808080");
            appsView.BackgroundColor = Color.FromHex("#808080");
            appsUsage.BackgroundColor = Color.FromHex("#808080");
            trendsView.BackgroundColor = Color.FromHex("#808080");
            trends.BackgroundColor = Color.FromHex("#808080");
            tasksView.BackgroundColor = Color.FromHex("#808080");
            tasks.BackgroundColor = Color.FromHex("#808080");
            optionsView.BackgroundColor = Color.FromHex("#808080");
            options.BackgroundColor = Color.FromHex("#808080");
        }
    }
}
