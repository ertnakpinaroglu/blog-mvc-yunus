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
    public class HomeController : Controller
    {
        db_blog db = new db_blog();
        // GET: Home
        public ActionResult Anasayfa(int sayfa=1)
        {
            var gelenKategoriler = db.Kategorilers.OrderBy(m=>m.KategoriAdi).ToPagedList(sayfa,4 );
            return View(gelenKategoriler);
        }


        public ActionResult _PartialKategoriler()
        {
            return View(db.Kategorilers.ToList());
        }

        public ActionResult MakaleListele(int id)
        {
            var gelenMakaleyeGore = db.Makalelers.Where(m => m.KategoriId == id).ToList();
            return View(gelenMakaleyeGore);
        }

        public ActionResult MakaleDetay(int id)
        {
            var gelenDeger = db.Makalelers.Where(m => m.MakaleId == id).SingleOrDefault();
            if (gelenDeger == null)
            {
                return HttpNotFound();
            }
            return View(gelenDeger);
        }


        public ActionResult SonMakaleler(int sayfa=1)
        {
            var gelenMakaleler = db.Makalelers.OrderByDescending(m => m.MakaleTarih).ToPagedList(sayfa,8);
            return View(gelenMakaleler);
        }

        public ActionResult OkunmasayisiniArttir(int id)
        {
            var gelenMakale = db.Makalelers.Where(m => m.MakaleId == id).SingleOrDefault();
            if (gelenMakale == null)
            {
                return HttpNotFound();
            }
            gelenMakale.OkunmaSayisi += 1;
            db.SaveChanges();
            return View("SonMakaleler", db.Makalelers.OrderByDescending(m => m.OkunmaSayisi));
        }

        public ActionResult EnPopulerler(int sayfa=1)
        {
            return View("SonMakaleler", db.Makalelers.OrderByDescending(m => m.OkunmaSayisi).ToPagedList(sayfa,8));
        }


        public JsonResult YorumYap(String yorum, int makaleId)
        {
            if (yorum != null)
            {
                db.Yorumlars.Add(new Yorumlar()
                {

                    YorumIcerik = yorum,
                    UyeId = Convert.ToInt32(Session["uyeid"]),
                    MakaleId = makaleId,
                    YorumTarihi = DateTime.Now
                });
                db.SaveChanges();
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult YorumSil(int id)
        {
            var uyeid = Session["uyeid"];
            var yorum = db.Yorumlars.Where(m => m.YorumId == id).SingleOrDefault();
            var makalemiz = db.Makalelers.Where(m => m.MakaleId == yorum.MakaleId).SingleOrDefault();
            if (Convert.ToInt32(uyeid) == yorum.UyeId)
            {
                db.Yorumlars.Remove(yorum);
                db.SaveChanges();
            }

            return RedirectToAction("MakaleDetay", "Home", new { id = makalemiz.MakaleId });

        }

        public ActionResult Hakkimizda()
        {
            return View();

        }

        public ActionResult _PartialSonYorumlar()
        {
            var sonYorumlar = db.Yorumlars.OrderByDescending(m => m.YorumTarihi).Take(4);
            if (sonYorumlar == null)
            {
                return HttpNotFound();
            }
            return View(sonYorumlar);
        }

        public ActionResult _PartialFooter()
        {
            return View();
        }

        public ActionResult Arama(String txtSearch)
        {
            var gelenVeri = db.Makalelers.Where(m => m.MakaleBasligi.StartsWith(txtSearch)).ToList();
            return View(gelenVeri);
        }
        

        public ActionResult _Partial_Uye_Yorum(int id)
        {
            var gelenUye = db.Uyelers.Where(m => m.UyeId == id).SingleOrDefault();
            
            return View();
        }
    }
}