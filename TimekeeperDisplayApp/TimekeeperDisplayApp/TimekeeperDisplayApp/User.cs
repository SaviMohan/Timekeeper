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
            applicationLog.Add(new AppData(myData, myDataStorage.titleToName(myData.windowTitle)));       
        }

        public void addToLog(Data myData, DataStorage myDataStorage)
        {
            applicationLog.Add(new AppData(myData, myDataStorage.titleToName(myData.windowTitle)));
        }

        public override string ToString()
        {
            string returnString = "UserID: " + userID + ".";
            foreach (AppData data in applicationLog)
            {
                returnString = returnString + data.ToString();
            }
            return returnString;
        }
    }
}
