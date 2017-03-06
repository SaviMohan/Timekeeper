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
            string name;

            name = "hi";
            return name;
        }

    }
}
