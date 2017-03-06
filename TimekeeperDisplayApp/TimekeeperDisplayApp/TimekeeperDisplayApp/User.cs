using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimekeeperDisplayApp
{
    public class User
    {
        public string name;
        public int userID;
        public int companyID;
        public List<AppData> applicationLog;

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
                if (appName == application.name)
                {
                    application.timesList.Add(myData.time);
                    return;
                }
                pos++;
            }   
            applicationLog.Add(new AppData(myData, appName));
        }
    }
}
