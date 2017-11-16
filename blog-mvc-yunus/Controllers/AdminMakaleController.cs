using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using blog_mvc_yunus.Models;
using System.Web.Helpers;
using System.IO;
using PagedList;
using PagedList.Mvc;

namespace blog_mvc_yunus.Controllers
{
    
    public class AdminMakaleController : Controller
    {
        db_blog db = new db_blog();
        // GET: AdminMakale
        public ActionResult MakaleListele(int sayfa=1)
        {
            var gelenMakaleler = db.Makalelers.OrderBy(m=>m.MakaleTarih).ToPagedList(sayfa,10);
            return View(gelenMakaleler);
        }

        // MakeleEkleme
        [HttpGet]
        public ActionResult MakaleEkle()
        {
            ViewBag.Kategoriler = new SelectList(db.Kategorilers, "KategoriId", "KategoriAdi").ToList();
            return View();
        }

        [HttpPost]
        public ActionResult MakaleEkle(Makaleler makale,HttpPostedFileBase makaleFotosu)
        {
            if (ModelState.IsValid)
            {
                if (makaleFotosu!=null)
                {
                    // fotoyu yükle
                    WebImage image = new WebImage(makaleFotosu.InputStream);
                    FileInfo info = new FileInfo(makaleFotosu.FileName);
                    String newFoto = Guid.NewGuid().ToString() + info.Extension;
                    image.Resize(250, 250);
                    image.Save("~/Images/MakaleResim/AdminMakaleFoto/" + newFoto);
                    makale.MakaleFoto = "/Images/MakaleResim/AdminMakaleFoto/" + newFoto;
                }
                makale.MakaleTarih = DateTime.Now;
                makale.OkunmaSayisi = 0;
                db.Makalelers.Add(makale);
                db.SaveChanges();
                return RedirectToAction("MakaleListele", "AdminMakale");
            }
            ViewBag.Kategoriler = new SelectList(db.Kategorilers, "KategoriId", "KategoriAdi").ToList();
            return View(makale);
        }

        public ActionResult MakaleDuzenle(int id)
        {
            var gelenMakale = db.Makalelers.Where(m => m.MakaleId == id).SingleOrDefault();
            if (gelenMakale==null)
            {
                return HttpNotFound();
            }
            ViewBag.Kategoriler = new SelectList(db.Kategorilers, "KategoriId", "KategoriAdi").ToList();
            return View(gelenMakale);
        }

        [HttpPost]
        public ActionResult MakaleDuzenle(int id,HttpPostedFileBase makaleFotomuz,Makaleler gelenMakale)
        {
            var gelenMakalemiz = db.Makalelers.Where(m => m.MakaleId == id).SingleOrDefault();
            if (gelenMakalemiz==null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                if (makaleFotomuz!=null)
                {
                    if (System.IO.File.Exists(makaleFotomuz.FileName))
                    {
                        System.IO.File.Delete(makaleFotomuz.FileName);
                    }
                    WebImage image = new WebImage(makaleFotomuz.InputStream);
                    FileInfo info = new FileInfo(makaleFotomuz.FileName);
                    String newFoto = Guid.NewGuid().ToString() + info.Extension;
                    image.Resize(250, 250);
                    image.Save("~/Images/MakaleResim/AdminMakaleFoto/" + newFoto);
                    gelenMakalemiz.MakaleFoto = "/Images/MakaleResim/AdminMakaleFoto/" + newFoto;
                }
                gelenMakalemiz.MakaleBasligi = gelenMakale.MakaleBasligi;
                gelenMakalemiz.KategoriId = gelenMakale.KategoriId;
                gelenMakalemiz.MakaleIcerik = gelenMakale.MakaleIcerik;
                gelenMakalemiz.MakaleTarih = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("MakaleListele", "AdminMakale");               
            }

            ViewBag.Kategoriler = new SelectList(db.Kategorilers, "KategoriId", "KategoriAdi").ToList();
            return View(gelenMakale);
        }

        public ActionResult MakaleDetay(int id)
        {
            var gelenler = db.Makalelers.Where(m => m.MakaleId == id).SingleOrDefault();
            if (gelenler==null)
            {
                return HttpNotFound();
            }
            return View(gelenler);
        }


        public ActionResult MakaleSil(int id)
        {
            var gelenMakale = db.Makalelers.Where(m => m.MakaleId == id).SingleOrDefault();
            if (gelenMakale==null)
            {
                return HttpNotFound();
            }
            return View(gelenMakale);
        }

        [HttpPost,ActionName("MakaleSil")]
        public ActionResult MakaleSilOk(int id)
        {
            var gelenMakale = db.Makalelers.Where(m => m.MakaleId == id).SingleOrDefault();
            if (gelenMakale==null)
            {
                return HttpNotFound();
            }

            if (System.IO.File.Exists(gelenMakale.MakaleFoto))
            {
                System.IO.File.Delete(gelenMakale.MakaleFoto);
            }

        
            // makalenin yorumlarını sil 
            foreach (var item in gelenMakale.Yorumlars.ToList())
            {
                db.Yorumlars.Remove(item);
            }


            db.Makalelers.Remove(gelenMakale);
            db.SaveChanges();
            return RedirectToAction("MakaleListele", "AdminMakale");
        }


    }
}