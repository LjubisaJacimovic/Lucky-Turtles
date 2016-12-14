using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuckyTurtles.ASP.NET.MVC.VideoTutorials.Models.ViewModels
{
    public class VideoEdit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SourceLink { get; set; }
        public System.DateTime DatePublished { get; set; }
        public Nullable<int> Duration { get; set; }
        public int UserId { get; set; }
        public bool isApproved { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual User Users { get; set; }
    }
}