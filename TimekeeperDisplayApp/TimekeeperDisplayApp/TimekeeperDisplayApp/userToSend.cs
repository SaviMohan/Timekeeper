using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimekeeperDisplayApp
{
    class UserToSend
    {
        public string name;
        public string classification;

        public UserToSend()
        {

        }

        public UserToSend(AppData myApp)
        {
            name = myApp.getName();
            classification = myApp.getClassification();
        }
    }
}
