using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimekeeperDisplayApp
{
    class AppData
    {
        public string name;
        public string classification;
        public DateTime timeSpentOn;
        public List<DateTime> timesList = new List<DateTime>();

        public AppData(Data myData, string appName)
        {
            timeSpentOn = new DateTime();
            timesList.Add(myData.time);
            classification = "undefined";
            name = appName;
        }
    }
}
