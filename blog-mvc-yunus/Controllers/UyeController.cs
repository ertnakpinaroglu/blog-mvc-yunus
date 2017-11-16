using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using blog_mvc_yunus.Models;
using System.Web.Helpers;
using System.IO;
using System.Web.Helpers;

namespace blog_mvc_yunus.Controllers
{
    public class UyeController : Controller
    {
        db_blog db = new db_blog();
        // GET: Uye
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Uyeler uye,String Sifre)
        {
            var md5pass = Crypto.Hash(Sifre, "MD5");
            var gelenUye = db.Uyelers.Where(m => m.KullaniciAdi == uye.KullaniciAdi).FirstOrDefault();

            if (gelenUye == null)
            {
                ModelState.AddModelError("", "giriş başarısız..");
            }
  
            else if (gelenUye.KullaniciAdi == uye.KullaniciAdi && gelenUye.Sifre ==  md5pass)
            {
                Session["kullaniciAdi"] = gelenUye.KullaniciAdi;
                Session["yetkiId"] = gelenUye.YetkiId;
                Session["uyeId"] = gelenUye.UyeId;
                return RedirectToAction("Anasayfa", "Home");
            }

            else if (gelenUye.KullaniciAdi != uye.KullaniciAdi || gelenUye.Sifre != md5pass)
            {
                ModelState.AddModelError("", "giriş başarısız..");
            }


            return View(uye);

        }

        public ActionResult Logout()
        {
            Session["uyeId"] = null;
            Session["yetkiId"] = null;
            Session.Abandon();
            return RedirectToAction("Anasayfa", "Home");
        }


        public ActionResult UyeOlustur()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UyeOlustur(Uyeler uye, HttpPostedFileBase uyeFoto,String Sifre)
        {
            
            var tumUyeler = db.Uyelers.Where(m => m.KullaniciAdi == uye.KullaniciAdi).FirstOrDefault();
            var md5Pass = Crypto.Hash(Sifre, "MD5");
            if (tumUyeler != null)
            {
                ModelState.AddModelError("", "Bu kullanıcı adı kullanılıyor.");
               
            }
            if (ModelState.IsValid)
            {
                if (uyeFoto != null)
                {
                    WebImage image = new WebImage(uyeFoto.InputStream);
                    FileInfo info = new FileInfo(uyeFoto.FileName);
                    String newFoto = Guid.NewGuid().ToString() + info.Extension;
                    image.Resize(250, 250);
                    image.Save("~/Images/uyeFoto/" + newFoto);
                    uye.UyeFoto = "/Images/uyeFoto/" + newFoto;
                }

                uye.YetkiId = 3;
                uye.Sifre = md5Pass;
                uye.Tarih = DateTime.Now;
                db.Uyelers.Add(uye);
                db.SaveChanges();
                return RedirectToAction("Anasayfa", "Home");
            }

            return View(uye);
        }

        // uyeDetay Sayfasi 
        public ActionResult UyeDetay(int id)
        {
            var gelenUye = db.Uyelers.Where(m => m.UyeId == id).SingleOrDefault();
            if (gelenUye == null)
            {
                return HttpNotFound();
            }
            ViewBag.Yorumlar = gelenUye.Yorumlars.ToList();
            return View(gelenUye);
        }
    }
}