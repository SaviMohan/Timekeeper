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
        public UsersPage(DataStorage myDataStorage)
        {
            InitializeComponent();
            initialiseUserDataTable(myDataStorage);
        }

        public void initialiseUserDataTable(DataStorage myDataStorage)
        {
            int rowCount = 0;
            foreach (User user in myDataStorage.getUserList())
            {
                dataGrid.Children.Add(new Label { Text = user.getName()+"test" }, 0, rowCount);
                dataGrid.Children.Add(new Label { Text = (user.getUserID()).ToString() }, 1, rowCount);
                dataGrid.Children.Add(new Label { Text = (user.getCompanyID()).ToString() }, 2, rowCount);
                dataGrid.Children.Add(new Label { Text = "This" }, 3, rowCount);
                dataGrid.Children.Add(new Label { Text = "This" }, 4, rowCount);
                rowCount++;
            }
        }
    }
}
