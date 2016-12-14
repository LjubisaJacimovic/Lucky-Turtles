﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuckyTurtles.ASP.NET.MVC.VideoTutorials.Models.ViewModels
{
    public class ProfileSettings
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }
}