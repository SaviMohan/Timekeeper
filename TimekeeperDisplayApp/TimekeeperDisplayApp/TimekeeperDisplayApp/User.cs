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

        public User(string inName, int uID, int cID) 
        {
            name = inName;
            userID = uID;
            companyID = cID;
        }

        public User(Data myData)
        {

        }

        
    }
}
