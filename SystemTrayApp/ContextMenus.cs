using System;
using System.Diagnostics;
using System.Windows.Forms;
using SystemTrayApp.Properties;
using System.Drawing;

namespace SystemTrayApp
{
	class ContextMenus
	{
		bool isAboutLoaded = false;
		bool isOptionsLoaded = false;

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>ContextMenuStrip</returns>
        public ContextMenuStrip Create()
		{
			// Add the default menu options.
			ContextMenuStrip menu = new ContextMenuStrip();
			ToolStripMenuItem item;
			ToolStripSeparator sep;

			// About.
			item = new ToolStripMenuItem();
			item.Text = "About";
			item.Click += new EventHandler(About_Click);
			item.Image = Resources.About;
			menu.Items.Add(item);

            // Options.
            item = new ToolStripMenuItem();
            item.Text = "Options";
            item.Click += new EventHandler(Options_Click);
            item.Image = Resources.About;
            menu.Items.Add(item);

            // Separator.
            sep = new ToolStripSeparator();
            menu.Items.Add(sep);

            // Exit.
            item = new ToolStripMenuItem();
			item.Text = "Exit";
			item.Click += new System.EventHandler(Exit_Click);
			item.Image = Resources.Exit;
			menu.Items.Add(item);

			return menu;
		}

		void About_Click(object sender, EventArgs e)
		{
			if (!isAboutLoaded)
			{
				isAboutLoaded = true;
				new AboutBox().ShowDialog();
				isAboutLoaded = false;
			}
		}

		void Options_Click(object sender, EventArgs e)
        {
            if (!isOptionsLoaded)
            {
                isOptionsLoaded = true;
                string promptValue = new OptionBox().ShowDialog("Test", "123");
                isOptionsLoaded = false;
            }
        }

        void Exit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
	}
}