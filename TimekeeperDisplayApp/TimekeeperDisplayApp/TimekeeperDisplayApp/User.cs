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

        public User() 
        {

        }

        public User(Data myData)
        {

        }

        
    }
}
