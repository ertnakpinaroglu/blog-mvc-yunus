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
    public class AdminController : Controller
    {
        db_blog db = new db_blog();
        // GET: Admin
        public ActionResult AdminKategoriEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminKategoriEkle(Kategoriler kategori, HttpPostedFileBase gelenResim)
        {
            var gelenKategori = db.Kategorilers.Where(m => m.KategoriAdi == kategori.KategoriAdi).FirstOrDefault();
            if (gelenKategori!=null)
            {
                ModelState.AddModelError("", "Bu kategori kullanılıyor.");
            }
            if (gelenResim==null)
            {
                ModelState.AddModelError("", "Lütfen resim seçiniz");
            }
            else if (gelenResim != null && gelenKategori==null)
            {
                // Fotografi yükle
                WebImage image = new WebImage(gelenResim.InputStream);
                FileInfo info = new FileInfo(gelenResim.FileName);
                String newFoto = Guid.NewGuid().ToString() + info.Extension;
                image.Resize(150, 150);
                image.Save("~/Images/MakaleResim/" + newFoto);
                kategori.Resim = "/Images/MakaleResim/" + newFoto;
                db.Kategorilers.Add(kategori);
                db.SaveChanges();
            }

            return View(kategori); /*RedirectToAction("AdminKategoriEkle", "Admin");*/
        }

        public ActionResult Listele(int sayfa=1)
        {

            return View(db.Kategorilers.OrderBy(m=>m.KategoriAdi).ToPagedList(sayfa,5));
        }

        [HttpGet]
        public ActionResult AdminKategoriDuzenle(int id)
        {
            var gelenKategori = db.Kategorilers.Where(m => m.KategoriId == id).SingleOrDefault();
            if (gelenKategori == null)
            {
                return HttpNotFound();
            }
            return View(gelenKategori);
        }

        [HttpPost]
        public ActionResult AdminKategoriDuzenle(int id, HttpPostedFileBase resimimiz, Kategoriler kategori)
        {
            var gelenKategori =  db.Kategorilers.Where(m => m.KategoriId == id).SingleOrDefault();
            if (gelenKategori==null)
            {
                return HttpNotFound();
            }

            if (resimimiz!=null)
            {
                if (System.IO.File.Exists(gelenKategori.Resim))
                {
                    System.IO.File.Delete(gelenKategori.Resim);
                }
                WebImage image = new WebImage(resimimiz.InputStream);
                FileInfo info = new FileInfo(resimimiz.FileName);
                String newFoto = Guid.NewGuid().ToString() + info.Extension;
                image.Resize(150, 150);
                image.Save("~/Images/MakaleResim/" + newFoto);
                gelenKategori.Resim = "/Images/MakaleResim/" + newFoto;

            }
            gelenKategori.Tanım = kategori.Tanım;
            gelenKategori.KategoriAdi = kategori.KategoriAdi;
            db.SaveChanges();
            return RedirectToAction("Listele", "Admin");
        }


        public ActionResult Detay(int id)
        {
            var gelenKategori = db.Kategorilers.Where(m => m.KategoriId == id).SingleOrDefault();
            if (gelenKategori==null)
            {
                return HttpNotFound();
            }
            return View(gelenKategori);
        }

        public ActionResult Sil(int id)
        {
            var gelenKategori = db.Kategorilers.Where(m => m.KategoriId == id).SingleOrDefault();
            if (gelenKategori==null)
            {
                return HttpNotFound();
            }
            return View(gelenKategori);
        }

        [HttpPost,ActionName("Sil")]
        public ActionResult SilOk(int id)
        {
            var gelenKategori = db.Kategorilers.Where(m => m.KategoriId == id).SingleOrDefault();
            if (gelenKategori == null)
            {
                return HttpNotFound();
            }
            if (System.IO.File.Exists(gelenKategori.Resim))
            {
                System.IO.File.Delete(gelenKategori.Resim);
            }

            db.Kategorilers.Remove(gelenKategori);
            db.SaveChanges();

            return RedirectToAction("Listele","Admin");
        }

        public ActionResult _PartialAdminDegerler()
        {
            ViewBag.MakaleSayisi = db.Makalelers.Count();
            ViewBag.YorumSayisi = db.Yorumlars.Count();
            ViewBag.KategoriSayisi = db.Kategorilers.Count();
            ViewBag.UyeSayisi = db.Uyelers.Count();
            return View();
        }
    }
}