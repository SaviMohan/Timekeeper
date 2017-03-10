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
            initialiseUserDataTable(myDataStorage, pageHolder);          
        }

        public void initialiseUserDataTable(DataStorage myDataStorage, ContentView pageHolder)
        {
            
            int rowCount = 0;
            foreach (User user in myDataStorage.getUserList())
            {
                List<Label> myLabels = new List<Label>();
                user.getStats();
                myLabels.Add(new Label { Text = user.getName() + "test", FontSize = 20, TextColor = Color.Black });
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

        private void addTapHandler(Label myLabel, DataStorage myDataStorage, ContentView pageHolder, User user)
        {
            var tapRow = new TapGestureRecognizer();
            tapRow.Tapped += (s, e) =>
            {
                ContentPage newPage = new TimekeeperDisplayApp.UsersPageTwo(myDataStorage, user);
                pageHolder.Content = newPage.Content;
            };
            myLabel.GestureRecognizers.Add(tapRow);

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
            };
            IDLabel.GestureRecognizers.Add(tapID);

            var tapProductiveTime = new TapGestureRecognizer();
            tapID.Tapped += (s, e) =>
            {
                myDataStorage.sortUsersBy("productivetime");
                if (myDataStorage.getUserLastSortedBy().Equals("productivetime"))
                {
                    myDataStorage.reverse("userlist");
                }
                else
                {
                    myDataStorage.sortUsersBy("productivetime");
                }
            };
            productiveTimeTodayLabel.GestureRecognizers.Add(tapProductiveTime);
        }
    }
}
