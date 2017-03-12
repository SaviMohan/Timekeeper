using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TimekeeperDisplayApp
{
    public partial class UsersPage : ContentPage
    {
        public UsersPage(DataStorage myDataStorage, ContentView pageHolder)
        {
            InitializeComponent();
            update(myDataStorage, pageHolder);          
            addColumnTapHandlers(myDataStorage, pageHolder);   
        }

        public async void update(DataStorage myDataStorage, ContentView pageHolder)
        {
            await updateProductivity(myDataStorage);
            await updateUsername(myDataStorage);
            myDataStorage.sortUsersBy("name");
            initialiseUserDataTable(myDataStorage, pageHolder);
        }

        public void initialiseUserDataTable(DataStorage myDataStorage, ContentView pageHolder)
        {
            int length = dataGrid.Children.Count();
            for (int i = 0; i < length; i++)
            {
                dataGrid.Children.RemoveAt(0);
            }

            int rowCount = 0;
            foreach (User user in myDataStorage.getUserList())
            {
                List<Label> myLabels = new List<Label>();
                user.getStats();
                string name = user.getName();
                if (name == "")
                {
                    name = "-";
                }
                myLabels.Add(new Label { Text = name, FontSize = 20, TextColor = Color.Black });
                myLabels.Add(new Label { Text = user.getUserID().ToString(), FontSize = 20, TextColor = Color.Black });
                myLabels.Add(new Label { Text = user.getProductiveTime().ToString(), FontSize = 20, TextColor = Color.Black });
                myLabels.Add(new Label { Text = user.getUnproductiveTime().ToString(), FontSize = 20, TextColor = Color.Black });
                myLabels.Add(new Label { Text = user.getMostUsed().getName(), FontSize = 20, TextColor = Color.Black });

                var rowDefinition = new RowDefinition();    //ensures the height of each row is constant
                rowDefinition.Height = new GridLength(50.0);
                dataGrid.RowDefinitions.Add(rowDefinition);

                for (int i = 0; i < myLabels.Count; i++)
                {                
                    dataGrid.Children.Add(myLabels[i], i, rowCount);
                    addTapHandler(myLabels[i], myDataStorage, pageHolder, user);               
                }            
                rowCount++;
            }
        }
        public async Task<int> updateProductivity(DataStorage myDataStorage)
        {
            RestService myService = new RestService();
            List<AppToSend> myList = await myService.RefreshAppAsync();
            foreach (AppToSend app in myList)
            {
                foreach (User user in myDataStorage.getUserList())
                {
                    List<AppData> applications = user.getApplicationLog();
                    foreach (AppData myAppData in applications)
                    {
                        if (myAppData.getName().Equals(app.name))
                        {
                            myAppData.setClassification(app.classification);
                        }
                    }
                    user.getStats();
                }
            }
            return 0;
         }

        public async Task<int> updateUsername(DataStorage myDataStorage)
        {
            RestService myService = new RestService();
            List<UserToSend> myList = await myService.RefreshUsernameAsync();
            System.Diagnostics.Debug.WriteLine(myList.Count);
            foreach (UserToSend user in myList)
            {
                foreach (User existingUser in myDataStorage.getUserList())
                {
                    if (existingUser.getUserID().Equals(user.userId))
                    {
                        existingUser.setName(user.name);
                    }
                    existingUser.getStats();
                } 
            }
            return 0;
        }
        #region TapHandlers
        private void addTapHandler(Label myLabel, DataStorage myDataStorage, ContentView pageHolder, User user)
        {
            var tapRow = new TapGestureRecognizer();
            tapRow.Tapped += (s, e) =>
            {
                ContentPage newPage = new TimekeeperDisplayApp.UsersPageTwo(myDataStorage, user, pageHolder);
                pageHolder.Content = newPage.Content;
            };
            myLabel.GestureRecognizers.Add(tapRow);
        }

        private void addColumnTapHandlers(DataStorage myDataStorage, ContentView pageHolder)
        {
            var tapUser = new TapGestureRecognizer();
            tapUser.Tapped += (s, e) =>
            {
                if (myDataStorage.getUserLastSortedBy().Equals("name"))
                {
                    myDataStorage.reverse("userlist");
                }
                else
                {
                    myDataStorage.sortUsersBy("name");
                }
                initialiseUserDataTable(myDataStorage, pageHolder);
            };
            userLabel.GestureRecognizers.Add(tapUser);

            var tapID = new TapGestureRecognizer();
            tapID.Tapped += (s, e) =>
            {
                if (myDataStorage.getUserLastSortedBy().Equals("userid"))
                {
                    myDataStorage.reverse("userlist");
                }
                else
                {
                    myDataStorage.sortUsersBy("userid");
                }
                initialiseUserDataTable(myDataStorage, pageHolder);
            };
            IDLabel.GestureRecognizers.Add(tapID);

            var tapProductiveTime = new TapGestureRecognizer();
            tapProductiveTime.Tapped += (s, e) =>
            {
                if (myDataStorage.getUserLastSortedBy().Equals("productivetime"))
                {
                    myDataStorage.reverse("userlist");
                }
                else
                {
                    myDataStorage.sortUsersBy("productivetime");
                }
                initialiseUserDataTable(myDataStorage, pageHolder);
            };
            productiveTimeTodayLabel.GestureRecognizers.Add(tapProductiveTime);

            var tapUnproductiveTime = new TapGestureRecognizer();
            tapUnproductiveTime.Tapped += (s, e) =>
            {
                if (myDataStorage.getUserLastSortedBy().Equals("unproductivetime"))
                {
                    myDataStorage.reverse("userlist");
                }
                else
                {
                    myDataStorage.sortUsersBy("unproductivetime");
                }
                initialiseUserDataTable(myDataStorage, pageHolder);
            };
            unproductiveTimeTodayLabel.GestureRecognizers.Add(tapUnproductiveTime);

            var tapMostUsed = new TapGestureRecognizer();
            tapMostUsed.Tapped += (s, e) =>
            {
                if (myDataStorage.getUserLastSortedBy().Equals("mostused"))
                {
                    myDataStorage.reverse("userlist");
                }
                else
                {
                    myDataStorage.sortUsersBy("mostused");
                }
                initialiseUserDataTable(myDataStorage, pageHolder);
            };
            mostUsedApplicationLabel.GestureRecognizers.Add(tapMostUsed);
        }
        #endregion
    }
}
