using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimekeeperDisplayApp
{
    public class DataStorage
    {
        public List<User> userList;
        public List<Data> dataList;

        public DataStorage()
        {
            userList = new List<User>();
            dataList = new List<Data>();
        }

        public string titleToName(string title)
        {
            if (title.Contains("Mail"))
            {
                return "E-Mail";
            }
            else if (title.Contains("Facebook"))
            {
                return "Facebook";
            }
            else if (title.Contains("Windows Explorer")) 
            {
                return "Windows Explorer";
            }
            else if (title.Contains("cmd.exe"))
            {
                return "Command Prompt";
            }
            else if (title.Contains("Visual Studio"))
            {
                return "Visual Studio";
            }
            else
            {
                return "Other";
            }          
        }

    }
}
