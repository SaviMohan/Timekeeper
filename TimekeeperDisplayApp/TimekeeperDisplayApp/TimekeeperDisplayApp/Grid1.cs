using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace TimekeeperDisplayApp
{
    public class Grid1 : ContentPage
    {
        int count = 1;

        public Grid1()
        {
            var layout = new StackLayout
            {
                //Orientation = StackOrientation.Vertical,
                Orientation = StackOrientation.Horizontal,
                Padding = 20
            };

            var grid = new Grid
            {
                RowSpacing = 50
            };

            grid.Children.Add(new Label { Text = "This" }, 0, 1); // Left, First element
            grid.Children.Add(new Label { Text = "text is" }, 1, 1); // Right, First element
            grid.Children.Add(new Label { Text = "in a" }, 0, 2); // Left, Second element
            grid.Children.Add(new Label { Text = "grid!" }, 1, 2); // Right, Second element
            grid.Children.Add(new Label { Text = "testing!" }, 1, 3);

            var gridButton = new Button { Text = "So is this Button!\nClick me." };
            gridButton.Clicked += delegate
            {
                gridButton.Text = string.Format("Thanks! {0} clicks.", count++);
            };

            grid.Children.Add(gridButton,0,3);

            var gridButton2 = new Button { Text = "So is this Button!\nClick me." };
            gridButton2.Clicked += delegate
            {
                gridButton2.Text = string.Format("Thanks! {0} clicks.", count++);
            };

            layout.Children.Add(grid);
            layout.Children.Add(gridButton2);
            Content = layout;

        }
    }
}