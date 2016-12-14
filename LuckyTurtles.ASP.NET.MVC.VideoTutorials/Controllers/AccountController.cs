using LuckyTurtles.ASP.NET.MVC.VideoTutorials.Models;
using LuckyTurtles.ASP.NET.MVC.VideoTutorials.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuckyTurtles.ASP.NET.MVC.VideoTutorials.Controllers
{
    public class AccountController : Controller
    {
        LuckyTurtlesVideoTutsEntities db = new LuckyTurtlesVideoTutsEntities();
        // GET: /Account/

        [HttpPost]
        public ActionResult signIn(UserSignIn signInUser)//prima parametar User
        {

            if (ModelState.IsValid)
            {
                //if username and password match with user in database
                var checkUser = db.Users.Where(x => x.Username == signInUser.Username && x.Password == signInUser.Password).FirstOrDefault();
                if (checkUser != null)
                {
                    //if there is match, check approved by admin        
                    if (checkUser.Approved == true)
                    {
                        //if approved Sign In
                        Session["User"] = checkUser;
                        return RedirectToAction("Index", "Home");
                    }

                    //if not approved return message
                    return RedirectToAction("Index", "Home", new {message = "Not Approved"});
                }

                return RedirectToAction("Index", "Home", new { message = "Not Registered" });
            }

            //if not registered return message
            return RedirectToAction("Index", "Home");
        }

        //Get
        public ActionResult signUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult signUp(UserSignUp user) //prima parametar User
        {
            if (ModelState.IsValid)
            {   
                //check if there already exists user with this username
                ViewBag.UsernameExists = null;
                var checkValidUsername = db.Users.Where(x => x.Username == user.Username).FirstOrDefault();
                if (checkValidUsername != null)
                {
                    //if yes return viewbag
                    ViewBag.UsernameExists = "Username is taken, please try different username";
                    return View(user);
                }

                //check if there already exists user with this email
                ViewBag.EmailExists = null;
                var checkEmailExists = db.Users.Where(x => x.Email == user.Email).FirstOrDefault();
                if (checkEmailExists != null)
                {
                    //if yes return viewbag
                    ViewBag.EmailExists = "There is already account using this mail, please try signing in";
                    return View(user);
                }

                // if all the validation passed add the user in database
                User userOk = new User();
                userOk.FirstName = user.FirstName;
                userOk.LastName = user.LastName;
                userOk.Username = user.Username;
                userOk.Email = user.Email;
                userOk.Password = user.Password;
                userOk.isAdmin = false;
                userOk.Approved = false;

                db.Users.Add(userOk);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        public ActionResult signOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");// redirect to Home/Index
        }
	}
}