using System;
using System.Diagnostics;
using System.Windows.Forms;
using SystemTrayApp.Properties;

namespace SystemTrayApp
{
	class ProcessIcon : IDisposable
	{
		NotifyIcon ni;

		public ProcessIcon()
		{
			ni = new NotifyIcon();
		}

		public void Display()
		{
			// Put the icon in the system tray and allow it react to mouse clicks.			
			ni.MouseClick += new MouseEventHandler(ni_MouseClick);
			ni.Icon = Resources.Logo;
			ni.Text = "Timekeeper Client";
			ni.Visible = true;

			// Attach a context menu.
			ni.ContextMenuStrip = new ContextMenus().Create();
		}

		public void Dispose()
		{
			// When the application closes, this will remove the icon from the system tray immediately.
			ni.Dispose();
		}

		void ni_MouseClick(object sender, MouseEventArgs e)
		{
			// Handle mouse button clicks.
			if (e.Button == MouseButtons.Left)
			{

			}
		}
	}
}