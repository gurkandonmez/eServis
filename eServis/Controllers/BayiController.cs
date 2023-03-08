using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eServis.Entities;
using System.Web.Mvc;
using eServis.BusinessLayer;
using System.Net;

namespace eServis.Controllers
{
    public class BayiController : Controller
    {
        // GET: Bayi
        Bayi bayi = new Bayi();
       BayiYonetim by = new BayiYonetim();
        Siralama sira = new Siralama();
        SiralamaYonetim siralama = new SiralamaYonetim();


        public ActionResult Index()
        {
            sira = siralama.Find(x => x.ID == 1);
            if (sira.BayiAlfabetikMi == false)
            {
                ViewBag.Bayi = by.List(x => x.Durum == false).OrderBy(x => x.Sira);
           
                return View();
            }
            else
            {
                ViewBag.Bayi = by.List(x => x.Durum == false).OrderBy(x => x.Adi);
                return View();
            }
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            return PartialView("_PartialBayiEkle");
        }

        [HttpGet]
        public ActionResult ArsivListesi()
        {
            List<Bayi> bayi = by.List(x => x.Durum == true);
            return PartialView("_PartialBayiArsivListesi", bayi);
        }

        [HttpPost]
        public ActionResult ArsivCikar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bayi db_bayi = by.Find(x => x.Id == id);
            if (db_bayi != null)
            {
                db_bayi.Durum = false;
                var durum = by.Update(db_bayi);
                if (durum > 0)
                {
                    return Json(new { Result = db_bayi });
                }
                else
                {
                    return Json("0");
                }


            }
            return Json("0");
        }


        [HttpPost]
        public ActionResult SilArsiv(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bayi db_bayi = by.Find(x => x.Id == id);
            if (db_bayi != null)
            {
               
                var durum = by.Delete(db_bayi);
                if (durum > 0)
                {
                    return Json(new { Result = db_bayi });
                }
                else
                {
                    return Json("0");
                }


            }
            return Json("0");
        }
        [HttpPost]
        public ActionResult BayiKaydet(Bayi bayi)
        {
            if (ModelState.IsValid)
            {
                by.Insert(bayi);
                if (sira.BayiAlfabetikMi == false)
                {
                  ViewBag.Bayi = by.List(x=>x.Durum==false).OrderBy(x => x.Sira);
                    return View("Index");
                }
                else
                {
                    ViewBag.Bayi = by.List(x => x.Durum == false).OrderBy(x => x.Adi);
                    return View("Index");
                }
            }
            else
            {
                ViewBag.Bayi = by.List().OrderBy(x => x.Sira);
                return View("Index");

            }

        }
        [HttpGet]
        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();

            }
            else
            {

                bayi = by.Find(x => x.Id == id.Value);
                return PartialView("_PartialBayiSil", bayi);
            }
        }


        [HttpGet]
        public ActionResult Arsiv(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();

            }
            else
            {

                bayi = by.Find(x => x.Id == id.Value);
                return PartialView("_PartialBayiArsiv", bayi);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sil(int? id, Bayi bayi)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                Bayi db_bayi = by.Find(x => x.Id == id.Value);
                by.Delete(db_bayi);
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Arsiv(int? id, Bayi bayi)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                Bayi db_bayi = by.Find(x => x.Id == id.Value);
                db_bayi.Durum = true;
                by.Update(db_bayi);
                return RedirectToAction("Index");
            }

        }
        [HttpGet]
        public ActionResult Guncelle(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                bayi = by.Find(x => x.Id == id.Value);
                return PartialView("_PartialBayiGuncelle", bayi);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Guncelle(int? id, Bayi bayi)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                Bayi db_bayi = by.Find(x => x.Id == id.Value);
                db_bayi.Adi = bayi.Adi;
                by.Update(db_bayi);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Sirala(int? id, FormCollection form, Bayi bayi)
        {


            bayi = by.Find(x => x.Id == id);
            int BayiId = bayi.Id;
            sira = siralama.Find(x => x.ID == 1);
            sira.BayiAlfabetikMi = false;
            siralama.Update(sira);

            Bayi db_bayi = by.Find(x => x.Id == BayiId);
            string[] BaslikId = form.GetValues("ID");
            string[] Sira = form.GetValues("Sira");
            ViewBag.Bayi = by.List(x => x.Id == BayiId).OrderBy(x => x.Sira);
            for (int i = 0; i < BaslikId.Length; i++)
            {
                int BaslikId2 = Int32.Parse(BaslikId[i]);
                int sira2 = Int32.Parse(Sira[i]);
                bayi = by.Find(x => x.Id == BaslikId2);
                bayi.Sira = sira2;
                by.Update(bayi);
            }
            ViewBag.AracListesi = by.List(x => x.Id == id.Value).OrderBy(x => x.Sira);
            return RedirectToAction("Index");


            //  return RedirectToAction("Listele", "AdresTanim", new { @id = id });

        }

        public ActionResult ListA()
        {
            sira = siralama.Find(x => x.ID == 1);
            sira.BayiAlfabetikMi = true;
            siralama.Update(sira);

            ViewBag.Bayi = by.List().OrderBy(x => x.Adi);

            return RedirectToAction("Index");
        }
    }
}