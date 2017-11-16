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
    public class AdminUyeController : Controller
    {
        db_blog db = new db_blog();
        // GET: AdminUye
        public ActionResult UyeListele(int sayfa=1)
        {

            return View(db.Uyelers.OrderByDescending(m=>m.Tarih).ToPagedList(sayfa,10));
        }

        public ActionResult UyeDetay(int id)
        {
            var gelenUye = db.Uyelers.Where(x => x.UyeId == id).SingleOrDefault();
            if (gelenUye == null)
            {
                return HttpNotFound();
            }
            return View(gelenUye);
        }

        [HttpGet]
        public ActionResult UyeSil(int id)
        {
            var gelenUye = db.Uyelers.Where(m => m.UyeId == id).SingleOrDefault();
            if (gelenUye == null)
            {
                return HttpNotFound();
            }
            return View(gelenUye);
        }

        [HttpPost, ActionName("UyeSil")]
        public ActionResult UyeSilOk(int id)
        {
            var gelenUye = db.Uyelers.Where(m => m.UyeId == id).SingleOrDefault();
            if (gelenUye == null)
            {
                return HttpNotFound();
            }

            if (System.IO.File.Exists(Server.MapPath(gelenUye.UyeFoto)))
            {
                System.IO.File.Delete(Server.MapPath(gelenUye.UyeFoto));
            }

            foreach (var item in db.Yorumlars.Where(x=>x.UyeId==gelenUye.UyeId).ToList())
            {
                db.Yorumlars.Remove(item);
            }
            db.SaveChanges();

           
            db.Uyelers.Remove(gelenUye);
            db.SaveChanges();
            return RedirectToAction("UyeListele", "AdminUye");
        }

    }
}