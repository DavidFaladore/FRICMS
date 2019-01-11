using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(user userModel)
        {
            userModel.LoginErrorMessage = "";
            return View(userModel);
        }

        [HttpPost]
        public ActionResult Autherize(user userModel) {
            using (LoginDatabaseEntites db = new LoginDatabaseEntites()) {
                var userDetails = db.users.Where(x => x.Username == userModel.Username && x.Password == userModel.Password).FirstOrDefault();

                if (userDetails == null){
                    userModel.LoginErrorMessage = "Wrong username or password";
                    return View("Index", userModel);
                }
                else
                {
                    Session["userID"] = userDetails.UserID;
                    return RedirectToAction("Index", "Cms");
                }
            }

            return View();
        }

        public ActionResult LogOut() {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}