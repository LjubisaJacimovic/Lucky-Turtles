using LuckyTurtles.ASP.NET.MVC.VideoTutorials.Filters;
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
    [LoginAuthorize]
    public class ProfileSettingsController : Controller
    {
        LuckyTurtlesVideoTutsEntities db = new LuckyTurtlesVideoTutsEntities();
        //
        // GET: /ProfileSettings/
        public ActionResult Settings(string message = null)
        {
            ViewBag.Message = null;
            ViewBag.MsgColor = "red";
            var loggedInUser = (User)Session["User"];
            ProfileSettings settingsUser = new ProfileSettings();

            settingsUser.Id = loggedInUser.Id;
            settingsUser.FirstName = loggedInUser.FirstName;
            settingsUser.LastName = loggedInUser.LastName;
            settingsUser.Username = loggedInUser.Username;
            settingsUser.Email = loggedInUser.Email;

            if (message != null)
            {
                if (message == "Current Password Incorrect")
                {
                    ViewBag.Message = "Current Password Incorrect";
                    return View(settingsUser);
                }
                if (message == "Paswords do not macht")
                {
                    ViewBag.Message = "New Password and Confirm Password do not match";
                    return View(settingsUser);
                }
                else
                {
                    ViewBag.MsgColor = "green";
                    ViewBag.Message = "Change Confirmed";
                    return View(settingsUser);
                }
            }
            return View(settingsUser);
        }

        [HttpPost]
        public ActionResult ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            User user = (User)Session["User"];
            if (user.Password != oldPassword)
            {
                return RedirectToAction("Settings", new { message = "Current Password Incorrect" });
            }
            if (newPassword != confirmPassword)
            {
                return RedirectToAction("Settings", new { message = "Paswords do not macht" });
            }

            User dbUser = db.Users.Find(user.Id);
            dbUser.Password = newPassword;
            db.Entry(dbUser).State = EntityState.Modified;
            db.SaveChanges();
            Session["User"] = dbUser;
            return RedirectToAction("Settings", new { message = "Change Confirmed" });
        }

        public ActionResult ChangeEmail(string oldPassword, string newMail)
        {
            User user = (User)Session["User"];
            if (user.Password != oldPassword)
            {
                return RedirectToAction("Settings", new { message = "Current Password Incorrect" });
            }
            User dbUser = db.Users.Find(user.Id);
            dbUser.Email = newMail;
            db.Entry(dbUser).State = EntityState.Modified;
            db.SaveChanges();
            Session["User"] = dbUser;
            return RedirectToAction("Settings", new { message = "Change Confirmed" });
        }
	}
}