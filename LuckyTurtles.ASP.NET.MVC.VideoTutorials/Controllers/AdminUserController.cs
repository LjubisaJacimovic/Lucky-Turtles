using LuckyTurtles.ASP.NET.MVC.VideoTutorials.Filters;
using LuckyTurtles.ASP.NET.MVC.VideoTutorials.Models;
using LuckyTurtles.ASP.NET.MVC.VideoTutorials.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LuckyTurtles.ASP.NET.MVC.VideoTutorials.Controllers
{

    [AdminAuthorize]
    public class AdminUserController : Controller
    {
        LuckyTurtlesVideoTutsEntities db = new LuckyTurtlesVideoTutsEntities();
        // GET: /Admin/
        public ActionResult ManageUsers()
        {
            //get all users to list 
            var users = db.Users.ToList();
            return View(users);
        }

        public ActionResult ApprovedUsers()
        {
            var users = db.Users.Where(x => x.Approved == true).ToList();
            return View("ManageUsers", users);
        }

        public ActionResult PendingUsers()
        {
            var users = db.Users.Where(x => x.Approved == false).ToList();
            return View("ManageUsers", users);
        }

        public ActionResult AdminUsers()
        {
            var users = db.Users.Where(x => x.isAdmin == true).ToList();
            return View("ManageUsers", users);
        }
        //Get
        public ActionResult EditUsers(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //find the user in DB 
            User users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }

            //bind the DB user with view model 
            UserEdit editUser = new UserEdit();
            editUser.Username = users.Username;
            editUser.isAdmin = users.isAdmin;
            editUser.isApproved = users.Approved;
            editUser.Id = users.Id;

            return View(editUser);
        }

        [HttpPost]
        public ActionResult EditUsers(UserEdit user)
        {
            //get user that needs to be changes from DB
            var findUser = db.Users.Find(user.Id);

            //set modified properties
            findUser.isAdmin = user.isAdmin;
            findUser.Approved = user.isApproved;
            if (user.isAdmin == true)
                findUser.Approved = true;

            //initialize the update 
            db.Entry(findUser).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ManageUsers", "AdminUser");

        }

        public ActionResult DeleteUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //get user from DB 
            User users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }

            //bind the DB user with view model 
            UserDelete deleteUser = new UserDelete();
            deleteUser.Id = users.Id;
            deleteUser.Username = users.Username;
            deleteUser.FirstName = users.FirstName;
            deleteUser.LastName = users.LastName;

            return View(deleteUser);
        }

        [HttpPost]
        public ActionResult DeleteUser(UserDelete deleteUser)
        {
            var loggedUser = (User)Session["User"];
            if (loggedUser.Id == deleteUser.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //get user from db 
            var user = db.Users.Find(deleteUser.Id);
            if (user != null)
            {
                //Delete the user from database
                db.Users.Remove(user);
                db.SaveChanges();

                return RedirectToAction("ManageUsers");
            }
            else
                //if there is no such user return bad request
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        }
    }
}