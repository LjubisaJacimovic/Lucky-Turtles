using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LuckyTurtles.ASP.NET.MVC.VideoTutorials.Models;
using LuckyTurtles.ASP.NET.MVC.VideoTutorials.Filters;
using LuckyTurtles.ASP.NET.MVC.VideoTutorials.Models.ViewModels;

namespace LuckyTurtles.ASP.NET.MVC.VideoTutorials.Controllers
{
    [AdminAuthorize]
    public class AdminVideoController : Controller
    {
        private LuckyTurtlesVideoTutsEntities db = new LuckyTurtlesVideoTutsEntities();

        // GET: /AdminVideo/
        public ActionResult ManageVideos()
        {
            ViewBag.PageTitle = "All Videos";
            //get all videos from db and send to view
            var videos = db.Videos.Include(v => v.User).Include(t => t.Tags);    
            return View(videos.ToList());
        }

        public ActionResult ApproveVideo(int id)
        {
            ViewBag.ErrorMessage = null;
            var video = db.Videos.Find(id);
            if (video.Rejected != null)
            {
                return RedirectToAction("ManageVideos");
            }
            video.isApproved = true;

            db.Entry(video).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ManageVideos");
        }

        public ActionResult DisapproveVideo(int id)
        {
            var video = db.Videos.Find(id);
            video.isApproved = false;

            db.Entry(video).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ManageVideos");
        }

        [HttpGet]
        public ActionResult RejectVideo(int id)
        {
            Video video = db.Videos.Find(id);
            return View(video);
        }

        [HttpPost]
        public ActionResult RejectVideo(string message, int Id)
        {
            //Get the rejection msg and save it to db
            RejectMessage msg = new RejectMessage();
            msg.Message = message;
            db.RejectMessages.Add(msg);
            db.SaveChanges();

            //Reference the rejection message to the video
            Video video = db.Videos.Find(Id);
            video.isApproved = false;
            video.Rejected = msg.Id;
            db.Entry(video).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ManageVideos");
        }

        public ActionResult ApprovedVideos()
        {
            ViewBag.PageTitle = "Approved Videos";
            List<Video> videos = db.Videos.Where(x => x.isApproved == true).ToList();
            return View("ManageVideos", new List<Video>(videos));
        }

        public ActionResult PendingVideos()
        {
            ViewBag.PageTitle = "Pending Videos";
            List<Video> videos = db.Videos.Where(x => x.isApproved == false && x.Rejected == null).ToList();
            return View("ManageVideos", new List<Video>(videos));
        }

        public ActionResult RejectedVideos()
        {
            ViewBag.PageTitle = "Rejected Videos";
            List<Video> videos = db.Videos.Where(x => x.Rejected != null).ToList();
            return View("ManageVideos", new List<Video>(videos));
        }

        public ActionResult RejectedDetails(int id)
        {
            Video video = db.Videos.Find(id);
            return View(video);
        }
    }
}
