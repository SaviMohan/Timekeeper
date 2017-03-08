using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimekeeperDisplayApp
{
    public class AppData
    {
        private string name;
        private string classification;
        private DateTime timeSpentOn;
        private List<DateTime> timesList = new List<DateTime>();

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
        #region GetAndSet
        public string getName()
        {
            return name;
        }

        public void setName(string input)
        {
            name = input;
        }

        public string getClassification()
        {
            return classification;
        }

        public void setClassification(string input)
        {
            classification = input;
        }

        public DateTime getDateTime()
        {
            return timeSpentOn;
        }

        public void setDateTime(DateTime input)
        {
            timeSpentOn = input;
        }

        public void addToTimesList(DateTime input)
        {
            timesList.Add(input);
        }
        #endregion
    }
}
