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
            //swap for dictionary and for loop iteration

            if (title.Contains("Outlook"))
            {
                return "E-Mail";
            }
            if (title.Contains("Gmail"))
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
            else if (title.Contains("9GAG"))
            {
                return "9GAG";
            }
            else if (title.Contains("YouTube"))
            {
                return "YouTube";
            }
            else if (title.Contains("Microsoft Word"))
            {
                return "Microsoft Word";
            }
            else if (title.Contains("Microsoft Excel"))
            {
                return "Microsoft Excel";
            }
            else if (title.Contains("Microsoft PowerPoint"))
            {
                return "Microsoft PowerPoint";
            }
            else if (title.Contains("Microsoft OneNote"))
            {
                return "Microsoft OneNote";
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
