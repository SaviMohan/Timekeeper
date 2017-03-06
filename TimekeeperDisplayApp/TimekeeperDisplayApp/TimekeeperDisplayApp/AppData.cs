using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimekeeperDisplayApp
{
    public class AppData
    {
        public string name;
        public string classification;
        public DateTime timeSpentOn;
        public List<DateTime> timesList = new List<DateTime>();

        public AppData(Data myData, string appName)
        {           
            timesList.Add(myData.time);
            timeSpentOn = sumTimes();
            classification = "undefined";
            name = appName;
        }

        public override string ToString()
        {
            string appData = name + ": \n";
            foreach (DateTime time in timesList)
            {
                appData = appData + "\t" + time.ToString() + "\n";
            }
            return appData;
        }

        public DateTime sumTimes()
        {
            //if we already have a time value just add the new time on
            //else 
            DateTime totalTime = new DateTime();

            return totalTime;
        }
    }
}
