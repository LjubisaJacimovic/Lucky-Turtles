using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuckyTurtles.ASP.NET.MVC.VideoTutorials.Models.ViewModels
{
    public class SearchModel
    {
        public string tag { get; set; }
        public string input { get; set; }
        public string  duration { get; set; }
        public string sortBy { get; set; }
    }
}