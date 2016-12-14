using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuckyTurtles.ASP.NET.MVC.VideoTutorials.Helpers
{
    public static class ConvertSeconds
    {
        public static int ConvertToSeconds(int hours, int minutes, int seconds)
        {
            int duration = hours * 3600 + minutes * 60 + seconds;
            return duration;
        }
    }
}