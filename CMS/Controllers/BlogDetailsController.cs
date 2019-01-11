using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CMS.Models;

namespace CMS.Controllers
{
    public class BlogDetailsController : ApiController
    {
        private LoginDatabaseEntites db;

        public BlogDetailsController()
        {
            db = new LoginDatabaseEntites();

        }
        // GET: api/BlogDetails
        public IHttpActionResult Get()
        {
            var post = db.blogs.ToList();
            return Ok(new { results = post });
        }

        // GET: api/BlogDetails/5
        public IHttpActionResult Get(int id)
        {
            var post = db.blogs.FirstOrDefault(x => x.blogid == id);
            return Ok(new { results = post });
        }

        // POST: api/BlogDetails
        public void Post([FromBody]blogs value)
        {
            var naslov = value.title;
            var vsebina = value.content;
            var category = value.kategorija;
            var imagepath = (value.Imagepath != null) ? value.Imagepath : "https://workhardanywhere.com/wp-content/uploads/2014/11/WHA_marvin_king_ashore.jpg";
            blogs novi = new blogs()
            {
                title = naslov,
                content = vsebina,
                kategorija = category,
                Imagepath = imagepath
            };
            db.blogs.Add(novi);
            db.SaveChanges();
        }

        // PUT: api/BlogDetails/5
        public void Put(int id, [FromBody]blogs value)
        {
            var posodobi = db.blogs.FirstOrDefault(x => x.blogid == id);
            var naslov = value.title;
            var vsebina = value.content;
            var category = value.kategorija;
            posodobi.content = vsebina;
            posodobi.title = naslov;
            posodobi.kategorija = category;
            db.SaveChanges();
        }

        // DELETE: api/BlogDetails/5
        public void Delete(int id)
        {
            LoginDatabaseEntites db = new LoginDatabaseEntites();
            var posodobi = db.blogs.Find(id);
            db.blogs.Remove(posodobi);
            db.SaveChanges();
        }
    }
}
