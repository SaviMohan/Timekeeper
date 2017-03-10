using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TimekeeperDisplayApp
{
    public partial class UsagePage : ContentPage
    {
        public UsagePage(DataStorage myDataStorage, ContentView pageHolder)
        {
            InitializeComponent();
            initialiseUsageDataTable(myDataStorage, pageHolder);
        }

        public void initialiseUsageDataTable(DataStorage myDataStorage, ContentView pageHolder)
        {

            
            myDataStorage.sumApps();
            int rowCount = 0;
            foreach (AppData app in myDataStorage.getAppList())
            {
                List<Label> myLabels = new List<Label>();
                myLabels.Add(new Label { Text = app.getName() , FontSize = 20, TextColor = Color.Black });
                myLabels.Add(new Label { Text = app.getClassification().ToString(), FontSize = 20, TextColor = Color.Black });
                myLabels.Add(new Label { Text = app.getTimeSpan().ToString(), FontSize = 20, TextColor = Color.Black });

                
                var rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(50.0);
                usageDataGrid.RowDefinitions.Add(rowDefinition);


                for (int i = 0; i < myLabels.Count; i++)
                {
                    usageDataGrid.Children.Add(myLabels[i], i, rowCount);
                    addTapHandler(myLabels[i], myDataStorage, pageHolder);
                }
                rowCount++;
            }
        }

        private void addTapHandler(Label myLabel, DataStorage myDataStorage, ContentView pageHolder)
        {
            var tapRow = new TapGestureRecognizer();
            tapRow.Tapped += (s, e) =>
            {
                //ContentPage newPage = new TimekeeperDisplayApp.UsersPageTwo(myDataStorage, user);
                //pageHolder.Content = newPage.Content;
            };
            myLabel.GestureRecognizers.Add(tapRow);
        }
    }
}
