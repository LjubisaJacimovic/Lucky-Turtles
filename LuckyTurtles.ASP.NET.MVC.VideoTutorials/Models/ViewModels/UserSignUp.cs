using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LuckyTurtles.ASP.NET.MVC.VideoTutorials.Models.ViewModels
{
    public class UserSignUp : User
    {

        [Required]
        [Compare("Password", ErrorMessage = "Confirm password and password do not match")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
    }
}