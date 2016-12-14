using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuckyTurtles.ASP.NET.MVC.VideoTutorials.Models.ViewModels
{
    public class UserEdit
    {
        public int Id { get; set; }
        public string  Username { get; set; }
        public bool isApproved { get; set; }
        public bool isAdmin { get; set; }
    }
}