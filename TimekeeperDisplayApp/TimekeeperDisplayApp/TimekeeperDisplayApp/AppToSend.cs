using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimekeeperDisplayApp
{
    public class AppToSend
    {
        public string name;
        public string classification;
        
        public AppToSend(AppData myApp)
        {
            name = myApp.getName();
            classification = myApp.getClassification();
        }
    }
}
