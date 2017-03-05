using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimekeeperDisplayApp
{
    public class Data
    {
        public string windowTitle;
        public DateTime time;
        public int companyID;
        public int userID;
        public int dataID;

        public Data(string title, int company, int user)
        {
        }

        public override string ToString()
        {
            return "DataID: " + dataID + ". User " + userID + " of " + companyID + " was using " + windowTitle + " at " + time + ".";
        }
    }
}
