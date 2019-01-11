using CMS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlTypes;
using System.IO;

namespace CMS.Controllers
{
    public class CmsController : Controller
    {
        private static SqlConnection con;
        private void connection()
        {
            var constr = ConfigurationManager.ConnectionStrings["LoginDatabaseEntites"].ToString();
            con = new SqlConnection(constr);

        }

        // GET: Cms
        public ActionResult Index()
        {
            LoginDatabaseEntites db = new LoginDatabaseEntites();
            var blogi = db.blogs;

            if (blogi.Count() > 0)
            {
                ViewBag.Empty = false;
                ViewBag.AppDataApplicationType = new SelectList(db.categories, "CatId", "naslov_kategorije");
                return View(blogi.ToList());
            }
            ViewBag.Empty = true;
            return View();
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            LoginDatabaseEntites db = new LoginDatabaseEntites();
            if (Request.HttpMethod == "POST")
            {
                var naslov = Request.Form["title"].ToString();
                var vsebina = Request.Form["content"].ToString();
                var category = Convert.ToInt32(Request.Form["kategorija"]);
                var path = Path.Combine("/assets/images", "wha.jpg");
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    var fileName = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
                    path = Path.Combine("/assets/images", fileName);
                    path.ToList();
                    file.SaveAs(Server.MapPath(path));
                }
                    DateTime date2 = (DateTime) new SqlDateTime(DateTime.Now);
                var temp = new DateTime().Date.ToString("yyyy-MM-dd HH:mm:ss");
                blogs novi = new blogs() {
                    title=naslov,
                    content=vsebina,
                    kategorija=category,
                    Imagepath=path
             
                };
               
                db.blogs.Add(novi);
                db.SaveChanges();
                return RedirectToAction("Index", "cms");
            }
            ViewBag.AppDataApplicationType = new SelectList(db.categories, "CatId", "naslov_kategorije");
            return View();
        }

        public ActionResult Edit(int id)
        {
            LoginDatabaseEntites db = new LoginDatabaseEntites();
            if (Request.HttpMethod == "POST")
            {
                var naslov = Request.Form["title"].ToString();
                var vsebina = Request.Form["content"].ToString();
                var idbloga = Convert.ToInt32(Request.Form["blogid"].ToString());
                var category = Convert.ToInt32(Request.Form["kategorija"]);
                var path = "";
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    var fileName = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
                    path = Path.Combine("/assets/images", fileName);
                    path.ToList();
                    file.SaveAs(Server.MapPath(path));
                }
                var posodobi = db.blogs.FirstOrDefault(x => x.blogid == id);
                posodobi.content = vsebina;
                posodobi.title = naslov;
                posodobi.kategorija = category;
                if (path.Length > 1) posodobi.Imagepath = path;
                db.SaveChanges();
                return RedirectToAction("Index", "Cms");
            }
            else
            {
                var post = db.blogs.SingleOrDefault(x => x.blogid == id);
                if (post != null)
                {
                    ViewBag.Found = true;
                    List<blogs> blg = new List<blogs>();
                    blg.Add(post);
                    ViewBag.AppDataApplicationType = new SelectList(db.categories, "CatId", "naslov_kategorije");
                    return View(blg);
                    
                }
                else
                {
                    ViewBag.Found = false;
                }
            }
            return View();

        }

        public ActionResult Delete(int id)
        {
            LoginDatabaseEntites db = new LoginDatabaseEntites();
            var posodobi = db.blogs.Find(id);
            db.blogs.Remove(posodobi);
            db.SaveChanges();

            return RedirectToAction("Index", "Cms");
        }
    }
}