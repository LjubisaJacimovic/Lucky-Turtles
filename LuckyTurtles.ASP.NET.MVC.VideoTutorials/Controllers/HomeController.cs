using LuckyTurtles.ASP.NET.MVC.VideoTutorials.Models;
using LuckyTurtles.ASP.NET.MVC.VideoTutorials.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuckyTurtles.ASP.NET.MVC.VideoTutorials.Controllers
{
    public class HomeController : Controller
    {
        LuckyTurtlesVideoTutsEntities db = new LuckyTurtlesVideoTutsEntities();
        //
        // GET: /Home/
        public ActionResult Index(string message = null)
        {
            ViewBag.ErrorMessage = null;

            List<Video> videos = db.Videos.Where(x => x.isApproved == true).ToList();

            ViewBag.TagsList = db.Tags.ToList();
            //check if there is error message sent from Sign In
            if (message != null)
            {
                if (message == "Not Approved")
                {
                    ViewBag.ErrorMessage = "Your account has not been approved by the admin yet";
                    return View(videos);
                }
                else
                {
                    ViewBag.ErrorMessage = "Username or password did not match";
                    return View(videos);
                }
            }

            return View(videos);
        }

        public ActionResult Search(SearchModel model)
        {
            List<Video> videos = new List<Video>();
            //get videos by tag if there is tag selected
            if (model.tag != "All Tags")
            {
                videos = db.Tags.Where(x => x.TagName == model.tag).FirstOrDefault().Videos.ToList();
                if (videos.Count() == 0)
                    return PartialView("Partials/VideosIndex", videos);
            }

            //check if there is no input
            if (model.input == null)
                model.input = " ";

            //filter videos by input
            if (videos.Count() == 0)
            {
                videos = db.Videos.Where(x => x.Title.ToUpper().Contains(model.input.ToUpper()) || x.Description.ToUpper().Contains(model.input.ToUpper())).ToList();
                if (videos.Count() == 0)
                    return PartialView("Partials/VideosIndex", videos);
            }
            else
            {
                videos = videos.Where(x => x.Title.ToUpper().Contains(model.input.ToUpper()) || x.Description.ToUpper().Contains(model.input.ToUpper())).ToList();
                if (videos.Count() == 0)
                    return PartialView("Partials/VideosIndex", videos);
            }
            //filter videos by duration
            switch (model.duration)
            {
                case "0-10":
                    if (videos.Count() == 0)
                        videos = db.Videos.Where(x => x.Duration <= 600).ToList();
                    else
                        videos = videos.Where(x => x.Duration <= 600).ToList();
                    break;
                case "0-30":
                    if (videos.Count() == 0)
                        videos = db.Videos.Where(x => x.Duration <= 1800).ToList();
                    else
                        videos = videos.Where(x => x.Duration <= 1800).ToList();
                    break;
                case "30+":
                    if (videos.Count() == 0)
                        videos = db.Videos.Where(x => x.Duration >= 1800).ToList();
                    else
                        videos = videos.Where(x => x.Duration >= 1800).ToList();
                    break;
                default:
                    break;
            }


            //sort newest or oldest
            if (model.sortBy == "Newest")
                videos = videos.OrderByDescending(x => x.DatePublished).ToList();
            else
                videos = videos.OrderBy(x => x.DatePublished).ToList();

            return PartialView("Partials/VideosIndex", videos);
        }

        public ActionResult WatchVideo(int id)
        {
            var video = db.Videos.Find(id);
            var user = (User)Session["User"];

            //adding history of watched videos for signed in user, if user is signed in 
            if (user != null)
            {
                History history = new History();
                var checkIfAdded = db.Histories.Where(x => x.videoId == id && x.userId == user.Id).FirstOrDefault();
                if (checkIfAdded == null)
                {
                    history.userId = user.Id;
                    history.videoId = video.Id;
                    db.Histories.Add(history);
                }
            }

            video.WatchCount += 1;
            db.Entry(video).State = EntityState.Modified;
            db.SaveChanges();
            return View(video);
        }

    }
}