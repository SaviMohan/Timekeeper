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

        public User(string inName, int uID, int cID, DataStorage myDataStorage) 
        {
            name = inName;
            userID = uID;
            companyID = cID;
            applicationLog = new List<AppData>();
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
        #endregion
    }
}
