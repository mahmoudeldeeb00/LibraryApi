using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Project.Helpers
{
    public static class TimeServer
    {
        public static string Now_s ()=>DateTime.Now.ToString("yyyy/MM/dd hh:mm tt");
    }
}