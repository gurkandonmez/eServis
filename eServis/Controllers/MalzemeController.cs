using eServis.BusinessLayer;
using eServis.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eServis.Controllers
{

    public class MalzemeController : Controller
    {
        Malzeme m = new Malzeme();
        MalzemeYonetim my = new MalzemeYonetim();
        Personel p = new Personel();
        PersonelYonetim py = new PersonelYonetim();
        // GET: Malzeme
        public ActionResult Index()
        {
            return View(my.List().Where(x => x.Durum == true).ToList());
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            ViewBag.AdSoyad = new SelectList(py.List(), "AdSoyad", "AdSoyad", p.AdSoyad);
            return PartialView("_PartialMalzemeEkle");

        }
        [HttpGet]
        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                HttpNotFound();
            }
            Malzeme malzeme = my.Find(x => x.ID == id.Value);
            if (id == null)
            {
                HttpNotFound();
            }
            return PartialView("_PartialMalzemeSil", malzeme);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sil(int id)
        {
            if (id == null)
            {
                HttpNotFound();
            }
            Malzeme m = my.Find(x => x.ID == id);
            if (m == null)
            {
                HttpNotFound();
            }
            my.Delete(m);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Duzenle(int? id)
        {
            ViewBag.Id = id;
            if (id == null)
            {
                HttpNotFound();
            }
            Malzeme m = my.Find(x => x.ID == id.Value);
            if (m == null)
            {
                HttpNotFound();
            }
            ViewBag.AdSoyad = new SelectList(py.List(), "AdSoyad", "AdSoyad", p.AdSoyad);
            return PartialView("_PartialMalzemeDuzenle", m);
        }
        [HttpPost]
        public ActionResult Duzenle(int id, Malzeme malzeme)

        {

            if (ModelState.IsValid)
            {
                Malzeme db_m = my.Find(x => x.ID == id);
                if (db_m.MalzemeAdi != null)
                {
                    db_m.AdSoyad = malzeme.AdSoyad;
                    db_m.MalzemeAdi = malzeme.MalzemeAdi;
                    db_m.Kod = malzeme.Kod;
                    db_m.Adet = malzeme.Adet;
                    if (my.Update(malzeme) > 0)
                    {

                        return Json(new { Result = db_m });

                    }
                    ViewBag.AdSoyad = new SelectList(py.List(), "AdSoyad", "AdSoyad", p.AdSoyad);
                }
            }

            return Json("0");
        }
        [HttpPost]
        public ActionResult Ekle(Malzeme malzeme)
        {

            //  malzeme.AdSoyad = form["Personel"].ToString();
            if (ModelState.IsValid)
            {
                malzeme.Durum = true;
                if (my.Insert(malzeme) > 0)
                {
                    return Json(new { Result = malzeme });

                }
            }
            ViewBag.AdSoyad = new SelectList(py.List(), "AdSoyad", "AdSoyad", p.AdSoyad);
            return Json("0");
        }

    }
}