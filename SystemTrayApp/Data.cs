using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemTrayApp
{

    class Data
    {
        public string windowTitle;
        public DateTime time;
        public int companyID;
        public int userID;
        private static int baseDataID = 0;
        public int dataID;

        public Data(string title, int company, int user)
        {
            windowTitle = title;
            time = DateTime.Today;
            companyID = company;
            userID = user;
            dataID = ++baseDataID;
        }

        public override string ToString()
        {
            return "DataID: " + dataID + ". User " + userID + " of " + companyID + " was using " + windowTitle + " at " + time + ".";
        }
    }
}
