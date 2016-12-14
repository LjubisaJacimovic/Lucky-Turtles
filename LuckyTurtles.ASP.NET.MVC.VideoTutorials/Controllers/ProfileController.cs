using LuckyTurtles.ASP.NET.MVC.VideoTutorials.Filters;
using LuckyTurtles.ASP.NET.MVC.VideoTutorials.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LuckyTurtles.ASP.NET.MVC.VideoTutorials.Controllers
{
    [LoginAuthorize]
    public class ProfileController : Controller
    {
        LuckyTurtlesVideoTutsEntities db = new LuckyTurtlesVideoTutsEntities();
        //
        // GET: /Profile/
        public ActionResult MyProfile()
        {
            User user = (User)Session["User"];
            List<Video> uploadedVideos = db.Videos.Where(x => x.UserId == user.Id).ToList();
            ViewBag.VideoType = "Uploaded Videos";
            return View(uploadedVideos);
        }

        //GET: history of watched videos by the user
        public ActionResult HistoryVideos()
        {
            User user = (User)Session["User"];
            List<History> history = db.Histories.Where(x => x.userId == user.Id).ToList();
            List<Video> historyVideos = new List<Video>();
            foreach (var item in history)
            {
                if(item.Video.Rejected == null){
                    historyVideos.Add(item.Video);
                }
            }
            ViewBag.VideoType = "History";
            return View("MyProfile", new List<Video>(historyVideos));
        }
        public ActionResult RejectedVideos()
        {
            User user = (User)Session["User"];
            List<Video> videos = db.Videos.Where(x => x.UserId == user.Id && x.Rejected != null).ToList();
            return View("MyProfile", new List<Video>(videos));
        }


        public ActionResult DeleteVideo(int id)
        {
            //confirm that the video belongs to the user
            User loggedUser = (User)Session["User"];
            Video check = db.Users.Where(x => x.Id == loggedUser.Id).FirstOrDefault().Videos.Where(x => x.Id == id).FirstOrDefault();
            if (check == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //delete the video with all the references
            Video video = db.Videos.Find(id);
            List<History> history = db.Histories.Where(x => x.videoId == id).ToList();
            foreach (var item in history)
            {
                db.Histories.Remove(item);
            }

            if (video.Rejected != null)
            {
                RejectMessage rejectMsg = db.RejectMessages.FirstOrDefault(x => x.Id == video.Rejected);
                db.RejectMessages.Remove(rejectMsg);
            }

            db.Videos.Remove(video);
            db.SaveChanges();
            return RedirectToAction("MyProfile");
        }
    }
}