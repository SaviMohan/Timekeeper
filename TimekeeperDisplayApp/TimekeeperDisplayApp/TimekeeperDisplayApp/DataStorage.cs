using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimekeeperDisplayApp
{
    public class DataStorage
    {
        private List<User> userList;
        private List<Data> dataList;

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
        #region GetAndSet
        public List<User> getUserList()
        {
            return userList;
        }
        
        public void addToUserList(User input)
        {
            userList.Add(input);
        }

        public void setUserList(List<User> input)
        {
            userList = input;
        }

        public List<Data> getDataList()
        {
            return dataList;
        }

        public void addToDataList(Data input)
        {
            dataList.Add(input);
        }

        public void setDataList(List<Data> input)
        {
            dataList = input;
        }
        #endregion
    }
}
