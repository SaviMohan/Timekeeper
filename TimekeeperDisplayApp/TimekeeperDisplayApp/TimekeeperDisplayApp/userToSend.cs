using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimekeeperDisplayApp
{
    public class UserToSend
    {
        public string name;
        public int userId;

        public UserToSend()
        {

        }

        public UserToSend(String inputname, int inputUserId)
        {
            name = inputname;
            userId = inputUserId;
        }
    }
}
