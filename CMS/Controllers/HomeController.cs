using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Models;

namespace CMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            LoginDatabaseEntites db = new LoginDatabaseEntites();
            var blogi = db.blogs;
     
            if (blogi.Count() > 0)
            {
                ViewBag.Empty = false;
                var reversani = blogi.ToList();
                reversani.Reverse();
                return View(reversani);
            }
            ViewBag.Empty = true;
            return View();
        }

        [Route("Read/{id}")] // Set the ID parameter
        public ActionResult Read(int id)
        {
            // Read one single blog
            LoginDatabaseEntites db = new LoginDatabaseEntites();
            var blogs = db.blogs;
            blogs post = null;

            if (blogs != null && blogs.Count() > 0)
            {
                post = blogs.Where(x => x.blogid == id).FirstOrDefault();
            }

            if (post == null)
            {
                ViewBag.PostFound = false;
                return View();
            }
            else
            {
                var categId = post.kategorija;
                string kategorija = db.categories.Where(x => x.CatId == categId).Select(x => x.naslov_kategorije).Single();
                ViewBag.Categories = kategorija;
                ViewBag.PostFound = true;
                return View(post);
            }
        }

        public ActionResult GetData(int pageIndex, int pageSize)
        {
            System.Threading.Thread.Sleep(1000);
            LoginDatabaseEntites db = new LoginDatabaseEntites();
            var query = (from c in db.blogs
                         orderby c.blogid descending
                         select c)
                         .Skip(pageIndex * pageSize)
                         .Take(pageSize);
            return Json(query.ToList(), JsonRequestBehavior.AllowGet);
        }

    }
}