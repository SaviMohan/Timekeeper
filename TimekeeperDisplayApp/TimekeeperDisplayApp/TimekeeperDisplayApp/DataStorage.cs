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
        private List<AppData> appList;
        private Dictionary<string, string> appsList;

        public DataStorage()
        {
            userList = new List<User>();
            dataList = new List<Data>();
            appList = new List<AppData>();
            appsList = new Dictionary<string, string>();
            addAppsToList();
        }

        public string titleToName(string title)
        {           
            if (title.Contains(" - Google Chrome"))
            {
                title = title.Replace(" - Google Chrome", "");
                return getDomain(title);
            }
            else if (title.Contains(" - Internet Explorer"))
            {
                title = title.Replace(" - Internet Explorer", "");
                return getDomain(title);
            }
            else if (title.Contains(" - Mozilla Firefox"))
            {
                title = title.Replace(" - Mozilla Firefox", "");
                return getDomain(title);
            }
            else
            {
                foreach (KeyValuePair<string, string> entry in appsList)
                {
                    if (title.Contains(entry.Key))
                    {
                        return entry.Value;
                    }
                }
                return title;
            }          
        }

        public string getDomain(string title)
        {
            title = title.Replace(" / ", "");
            if (!title.Contains("https://"))
            {
                title = "https://" + title;
            }
            Uri myUri = new Uri(title);
            return myUri.Host;
        }

        public void sumApps()
        {
            foreach (User user in userList)
            {
                foreach (AppData app in user.getApplicationLog())
                {
                    int pos = appListContains(app);
                    if (pos == -1)
                    {
                        appList.Add(new AppData(app.getData(), app.getName()));
                    }
                    else
                    {
                        foreach (DateTime data in app.getTimesList())
                        {
                            appList[pos].addToTimesList(data);
                        }
                    }
                }          
            }
        }

        public int appListContains(AppData appData)
        {
            int pos = 0;
            foreach (AppData app in appList)
            {
                if (appData.getName() == app.getName())
                {
                    return pos;
                }
                pos++;
            }
            return -1;
        }

        #region AddApps
        private void addAppsToList()
        {
            addApp("cmd.exe", "Command Prompt");
            addApp("Windows Explorer");
            addApp("Visual Studio");
            addApp("Word");
            addApp("Excel");
            addApp("PowerPoint");
            addApp("OneNote");
        }

        public void addApp(string title)
        {
            appsList.Add(title, title);
        }
 
        public void addApp(string title, string output)
        {
            appsList.Add(title, output);
        }
        #endregion

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

        public List<AppData> getAppList()
        {
            return appList;
        }

        public void setAppList(List<AppData> input)
        {
            appList = input;
        }
        #endregion
    }
}
