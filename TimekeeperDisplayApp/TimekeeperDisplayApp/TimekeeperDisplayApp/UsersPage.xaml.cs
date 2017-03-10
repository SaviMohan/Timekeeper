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
                for (int i = 0; i < myLabels.Count; i++)
                {
                    dataGrid.Children.Add(myLabels[i], i, rowCount);
                    addTapHandler(myLabels[i], myDataStorage, pageHolder, user);
                    //dataGrid.RowDefinitions.
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
        }
    }
}
