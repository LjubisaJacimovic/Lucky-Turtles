//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LuckyTurtles.ASP.NET.MVC.VideoTutorials.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.Histories = new HashSet<History>();
            this.Videos = new HashSet<Video>();
        }
    
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool isAdmin { get; set; }
        public bool Approved { get; set; }
    
        public virtual ICollection<History> Histories { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
    }
}
