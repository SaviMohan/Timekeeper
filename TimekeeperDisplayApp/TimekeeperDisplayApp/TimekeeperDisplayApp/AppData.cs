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
        private TimeSpan timeSpentOn;
        private List<DateTime> timesList = new List<DateTime>();
        private Data data;

        public AppData(Data myData, string appName)
        {
            data = myData;
            timesList.Add(myData.time);
            classification = "Productive";
            name = appName;
            timeSpentOn = new TimeSpan(0, 0, 0);
        }

        public AppData(Data myData, string appName, TimeSpan myTimeSpan, string myClassification)
        {
            data = myData;
            timesList.Add(myData.time);
            classification = myClassification;
            name = appName;
            timeSpentOn = myTimeSpan;
        }

        public override string ToString()
        {
            string appData = name + ": (" + timeSpentOn + " total) \n";
            foreach (DateTime time in timesList)
            {
                appData = appData + "\t" + time.ToString() + "\n";
            }
            return appData;
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

        public TimeSpan getTimeSpan()
        {
            return timeSpentOn;
        }

        public void setTimeSpan(TimeSpan input)
        {
            timeSpentOn = input;
        }

        public void addToTimesList(DateTime input)
        {
            timesList.Add(input);
            timeSpentOn = timeSpentOn.Add(new TimeSpan(0, 0, 5));
        }

        public List<DateTime> getTimesList()
        {
            return timesList;
        }

        public Data getData()
        {
            return data;
        }
        #endregion
    }
}
