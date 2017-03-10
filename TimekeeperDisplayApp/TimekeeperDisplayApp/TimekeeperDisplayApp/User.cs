using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimekeeperDisplayApp
{
    public class User
    {
        private string name;
        private int userID;
        private int companyID;
        private List<AppData> applicationLog;
        private TimeSpan productiveTime;
        private TimeSpan unproductiveTime;
        private AppData mostUsed;

        public User(string inName, int uID, int cID, DataStorage myDataStorage) 
        {
            name = inName;
            userID = uID;
            companyID = cID;
            applicationLog = new List<AppData>();
            getStats();
        }

        public User(Data myData, DataStorage myDataStorage)
        {
            name = "";
            userID = myData.userID;
            companyID = myData.companyID;
            applicationLog = new List<AppData>();
            addApplication(myData, myDataStorage);
        }

        public void addToLog(Data myData, DataStorage myDataStorage)
        {
            addApplication(myData, myDataStorage);  
        }

        public override string ToString()
        {
            string returnString = "UserID: " + userID + "\n";
            foreach (AppData data in applicationLog)
            {
                returnString = returnString + data.ToString();
            }
            returnString = returnString + "\n";
            return returnString;
        }

        public void addApplication(Data myData, DataStorage myDataStorage)
        {
            string appName = myDataStorage.titleToName(myData.windowTitle);
            int pos = 0;
            foreach (AppData application in applicationLog)
            {
                if (appName == application.getName())
                {
                    application.addToTimesList(myData.time);
                    return;
                }
                pos++;
            }   
            applicationLog.Add(new AppData(myData, appName));
        }

        public void getStats()
        {
            mostUsed = applicationLog[0];
            productiveTime = new TimeSpan(0, 0, 0);
            unproductiveTime = new TimeSpan(0, 0, 0);
            foreach (AppData app in applicationLog)
            {
                if (app.getTimeSpan() > mostUsed.getTimeSpan())
                {
                    mostUsed = app;
                }
                if (app.getClassification() == "Productive")
                {
                    productiveTime = productiveTime + app.getTimeSpan();
                }
                else if (app.getClassification() == "Unproductive")
                {
                    unproductiveTime = unproductiveTime + app.getTimeSpan();
                }
            }
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

        public int getUserID()
        {
            return userID;
        }

        public void setUserID(int input)
        {
            userID = input;
        }

        public int getCompanyID()
        {
            return companyID;
        }

        public void setCompanyID(int input)
        {
            companyID = input;
        }

        public List<AppData> getApplicationLog()
        {
            return applicationLog;
        }

        public void setApplicationLog(List<AppData> input)
        {
            applicationLog = input;
        }

        public TimeSpan getProductiveTime()
        {
            return productiveTime;
        }

        public TimeSpan getUnproductiveTime()
        {
            return unproductiveTime;
        }

        public AppData getMostUsed()
        {
            return mostUsed;
        }
        #endregion
    }
}
