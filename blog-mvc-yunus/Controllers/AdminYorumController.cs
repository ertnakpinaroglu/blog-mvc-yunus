using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using blog_mvc_yunus.Models;
using PagedList;
using PagedList.Mvc;

namespace blog_mvc_yunus.Controllers
{
    public class AdminYorumController : Controller
    {
        db_blog db = new db_blog();
        // GET: AdminYorum
        public ActionResult YorumListele(int sayfa=1)
        {
            var gelenYorumlar = db.Yorumlars.OrderByDescending(m => m.YorumTarihi).ToPagedList(sayfa,10); ;
            if (gelenYorumlar == null)
            {
                return HttpNotFound();
            }
            return View(gelenYorumlar);
        }

        public ActionResult YorumSil(int id)
        {
            var gelenYorum = db.Yorumlars.Where(m => m.YorumId == id).SingleOrDefault();
            if (gelenYorum == null)
            {
                return HttpNotFound();
            }
            return View(gelenYorum);
        }
        [HttpPost,ActionName("YorumSil")]
        public ActionResult YorumSilOk(int id)
        {
            var gelenYorum = db.Yorumlars.Where(m => m.YorumId == id).SingleOrDefault();
            if (gelenYorum==null)
            {
                return HttpNotFound();
            }
            db.Yorumlars.Remove(gelenYorum);
            db.SaveChanges();
            return RedirectToAction("YorumListele", "AdminYorum");
        

        }
    }
}