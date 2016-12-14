using LuckyTurtles.ASP.NET.MVC.VideoTutorials.Filters;
using LuckyTurtles.ASP.NET.MVC.VideoTutorials.Models;
using LuckyTurtles.ASP.NET.MVC.VideoTutorials.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuckyTurtles.ASP.NET.MVC.VideoTutorials.Helpers;


namespace LuckyTurtles.ASP.NET.MVC.VideoTutorials.Controllers
{
    [LoginAuthorize]
    public class VideoController : Controller
    {
        LuckyTurtlesVideoTutsEntities db = new LuckyTurtlesVideoTutsEntities();
        // GET: /Video/
        [LoginAuthorize]
        public ActionResult AddVideo()
        {
            List<Tag> tagList = db.Tags.ToList();
            ViewBag.Tag = tagList;
            return View();
        }

        [HttpPost]
        public ActionResult AddVideo(VideoAdd video)
        {

            if (ModelState.IsValid)
            {
                Video videoToAdd = new Video();

                //get logged in user
                User user = (User)Session["User"];

                string videoSource;
                string[] source = video.SourceLink.Split('=');
                videoSource = source[1];

                //Convert duration to seconds
                int duration = ConvertSeconds.ConvertToSeconds(video.Hours, video.Minutes, video.Seconds);
                //get tags from db for relation table
                ICollection<Tag> DbTags = db.Tags.ToList();
                List<Tag> tagsToAdd = new List<Tag>();

                //find the tags that needs to be added to video 
                foreach (var addedTag in video.Tag)
                {
                    foreach (var dbTag in DbTags)
                    {
                        if (addedTag == dbTag.TagName)
                            tagsToAdd.Add(dbTag);

                    };
                }

                //bind view model with video model
                videoToAdd.Title = video.Title;
                videoToAdd.Description = video.Description;
                videoToAdd.Duration = duration;
                videoToAdd.SourceLink = videoSource;
                videoToAdd.DatePublished = DateTime.Now;
                videoToAdd.isApproved = false;
                videoToAdd.UserId = user.Id;
                videoToAdd.Rejected = null;
                videoToAdd.Tags = tagsToAdd;
                videoToAdd.WatchCount = 0;

                db.Videos.Add(videoToAdd);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");

            }

            return View(video);
        }
        [HttpGet]
        public JsonResult GetTags()
        {
            return Json(db.Tags.Select(x => x.TagName).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}