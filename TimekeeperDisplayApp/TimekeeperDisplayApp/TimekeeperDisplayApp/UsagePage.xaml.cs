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
            myDataStorage.sumApps();
            myDataStorage.sortAppListBy("name");
            initialiseUsageDataTable(myDataStorage, pageHolder);
            addColumnTapHandlers(myDataStorage, pageHolder);
        }

        public void initialiseUsageDataTable(DataStorage myDataStorage, ContentView pageHolder)
        {
            int length = usageDataGrid.Children.Count();
            for (int i = 0; i < length; i++)
            {
                usageDataGrid.Children.RemoveAt(0);
            }

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
                    addTapHandler(myLabels[i], myDataStorage, pageHolder, app);
                }
                rowCount++;
            }
        }

        #region TapHandlers
        private void addTapHandler(Label myLabel, DataStorage myDataStorage, ContentView pageHolder, AppData app)
        {
            var tapRow = new TapGestureRecognizer();
            tapRow.Tapped += (s, e) =>
            {
                ContentPage newPage = new TimekeeperDisplayApp.UsagePageTwo(myDataStorage, app, pageHolder);
                pageHolder.Content = newPage.Content;
            };
            myLabel.GestureRecognizers.Add(tapRow);
        }

        private void addColumnTapHandlers(DataStorage myDataStorage, ContentView pageHolder)
        {
            var tapName = new TapGestureRecognizer();
            tapName.Tapped += (s, e) =>
            {
                if (myDataStorage.getAppLastSortedBy().Equals("name"))
                {
                    myDataStorage.reverse("applist");
                }
                else
                {
                    myDataStorage.sortAppListBy("name");
                }
                initialiseUsageDataTable(myDataStorage, pageHolder);
            };
            nameLabel.GestureRecognizers.Add(tapName);

            var tapClass = new TapGestureRecognizer();
            tapClass.Tapped += (s, e) =>
            {
                if (myDataStorage.getAppLastSortedBy().Equals("classification"))
                {
                    myDataStorage.reverse("applist");
                }
                else
                {
                    myDataStorage.sortAppListBy("classification");
                }
                initialiseUsageDataTable(myDataStorage, pageHolder);
            };
            classLabel.GestureRecognizers.Add(tapClass);

            var tapTime = new TapGestureRecognizer();
            tapTime.Tapped += (s, e) =>
            {              
                if (myDataStorage.getAppLastSortedBy().Equals("timespan"))
                {
                    myDataStorage.reverse("applist");
                }
                else
                {
                    myDataStorage.sortAppListBy("timespan");
                }
                initialiseUsageDataTable(myDataStorage, pageHolder);
            };
            timeLabel.GestureRecognizers.Add(tapTime);
        }
        #endregion
    }
}
