using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TimekeeperDisplayApp
{
    public partial class UsersPageTwo : ContentPage
    {
        User user;
        ContentView pageHolder;
        DataStorage dataStorage;

        public UsersPageTwo(DataStorage myDataStorage, User userIn, ContentView pageHolderIn)
        {
            InitializeComponent();
            dataStorage = myDataStorage;
            pageHolder = pageHolderIn;
            user = userIn;
            userID.Text = "User " + user.getUserID() + ": " + user.getName();
            if (user.getName() != "")
            {
                nameEntry.Text = user.getName();
            }
            else
            {
                nameEntry.Placeholder = "John Smith";
            }
            addTapHandlers();
        }

        void onCompleted(object sender, EventArgs args)
        {
            user.setName(nameEntry.Text);
            nameEntry.Text = user.getName();
            userID.Text = "User " + user.getUserID() + ": " + user.getName();
            dataStorage.seAllUsers(user.getName(), user.getUserID());
        }

        private void addTapHandlers()
        {
            var tapBack = new TapGestureRecognizer();
            tapBack.Tapped += (s, e) =>
            {
                ContentPage newPage = new TimekeeperDisplayApp.UsersPage(dataStorage, pageHolder);
                pageHolder.Content = newPage.Content;
            };
            back.GestureRecognizers.Add(tapBack);
        }
    }
}
