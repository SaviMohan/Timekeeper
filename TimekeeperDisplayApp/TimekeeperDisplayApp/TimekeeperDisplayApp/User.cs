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
        }

        public User(Data myData, DataStorage myDataStorage)
        {
            name = "";
            userID = myData.userID;
            companyID = myData.companyID;
            dataStorage = myDataStorage;
            applicationLog.Add(myData);       
        }
    }
}
