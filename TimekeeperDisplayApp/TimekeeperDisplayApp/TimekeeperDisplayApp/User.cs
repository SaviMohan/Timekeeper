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
        public List<Data> applicationLog;
        private DataStorage dataStorage;

        public User(string inName, int uID, int cID, DataStorage myDataStorage) 
        {
            name = inName;
            userID = uID;
            companyID = cID;
            dataStorage = myDataStorage;
            applicationLog = new List<Data>();
        }

        public User(Data myData, DataStorage myDataStorage)
        {
            name = "";
            userID = myData.userID;
            companyID = myData.companyID;
            dataStorage = myDataStorage;
            applicationLog = new List<Data>();
            applicationLog.Add(myData);       
        }

        public void addToLog(Data myData)
        {
            applicationLog.Add(myData);
        }

        public override string ToString()
        {
            string returnString = "UserID: " + userID + ".";
            foreach (Data data in applicationLog)
            {
                returnString = returnString + data.windowTitle;
            }
            return returnString;
        }
    }
}
