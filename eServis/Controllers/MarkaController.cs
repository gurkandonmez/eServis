using eServis.BusinessLayer;
using eServis.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace eServis.Controllers
{

    public class MarkaController : Controller
    {
        Siralama sira = new Siralama();
        SiralamaYonetim sy = new SiralamaYonetim();
        Marka marka = new Marka();
        MarkaYonetim my = new MarkaYonetim();
        // GET: Marka
        public ActionResult Index()
        {

            sira = sy.Find(x => x.ID == 1);
            if (sira.MarkaAlfabetikMi == true)
            {
                ViewBag.Marka = my.List(x => x.Durum == false).OrderBy(x => x.Baslik);


                return View();

            }
            else
            {
                ViewBag.Marka = my.List(x => x.Durum == false).OrderBy(x => x.Sira);

                return View();
            }


        }

        public ActionResult ListA()
        {


            sira = sy.Find(x => x.ID == 1);
            sira.MarkaAlfabetikMi = true;
            sy.Update(sira);

            ViewBag.Marka = my.List().OrderBy(x => x.Baslik);

            return RedirectToAction("Index", "Marka");
        }


        [HttpPost]
        public ActionResult Sirala(int? id, FormCollection form, Marka marka)
        {


            marka = my.Find(x => x.Id == id);
            int Id = marka.Id;
            sira = sy.Find(x => x.ID == 1);
            sira.MarkaAlfabetikMi = false;
            sy.Update(sira);

            Marka db_marka = my.Find(x => x.Id == Id);
            string[] BaslikId = form.GetValues("Id");
            string[] Sira = form.GetValues("Sira");
            ViewBag.Marka = my.List().OrderBy(x => x.Sira);
            for (int i = 0; i < BaslikId.Length; i++)
            {
                int BaslikId2 = Int32.Parse(BaslikId[i]);
                int sira2 = Int32.Parse(Sira[i]);
                marka = my.Find(x => x.Id == BaslikId2);
                marka.Sira = sira2;
                my.Update(marka);
            }
            ViewBag.Marka = my.List().OrderBy(x => x.Sira);
            return RedirectToAction("Index", "Marka");


            //  return RedirectToAction("Listele", "AdresTanim", new { @id = id });

        }





        public ActionResult Ekle()
        {
            return PartialView("_PartialMarkaEkle");
        }
        [HttpGet]
        public ActionResult Sil(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            marka = my.Find(x => x.Id == id.Value);
            if (marka == null)
            {
                HttpNotFound();
            }
            return PartialView("_PartialMarkaSil", marka);
        }

       
        [HttpGet]
        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            marka = my.Find(x => x.Id == id.Value);

            return PartialView("_PartialMarkaDuzenle", marka);
        }

        [HttpGet]
        public ActionResult ArsivListesi()
        {
            List<Marka> marka = new List<Marka>();
            marka = my.List(x => x.Durum == true);
            return PartialView("_PartialMarkaArsivListesi", marka);


        }
        [HttpPost]
        public ActionResult ArsivListesi(int? id, Marka marka)
        {
            if (id == null)
            {
                HttpNotFound();
            }
            Marka db_marka = my.Find(x => x.Id == id);
            if (db_marka != null)
            {
                db_marka.Durum = false;
                var durum = my.Update(db_marka);
                if (durum > 0)
                {
                    return Json(new { Result = db_marka });
                }
                else
                {
                    return Json("0");
                }


            }
            return Json("0");
        }
        [HttpGet]
        public ActionResult Arsiv(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            marka = my.Find(x => x.Id == id.Value);

            return PartialView("_PartialMarkaArsiv", marka);
        }



        [HttpPost]
        public ActionResult Arsiv(int? id, Marka gmarka)

        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    HttpNotFound();
                }

                marka = my.Find(x => x.Id == id.Value);
                marka.Durum = true;
                my.Update(marka);
                return RedirectToAction("Index", "Marka");
            }
            else
            {
                return RedirectToAction("Index", "Marka");
            }
        }
        [HttpPost]
        public ActionResult ArsivSil(int? id)
        {
            if (id == null)
            {
                HttpNotFound();
            }
            marka = my.Find(x => x.Id == id.Value);

         if( my.Delete(marka) >0)
            {
                return Json(new { Result = marka });
            }
         else
            {
                return Json("0");
            }
       
        }
        [HttpPost]
        public ActionResult Duzenle(int? id, Marka gmarka)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    HttpNotFound();
                }

                marka = my.Find(x => x.Id == id.Value);
                marka.Baslik = gmarka.Baslik;
                my.Update(marka);
                return RedirectToAction("Index", "Marka");
            }
            else
            {
                return RedirectToAction("Index", "Marka");
            }
        }
        [HttpPost]
        public ActionResult Ekle(Marka marka)
        {

            if (ModelState.IsValid)
            {
                my.Insert(marka);
                return RedirectToAction("Index", "Marka");
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sil(int? id, Marka marka)
        {

            if (id == null)
            {
                HttpNotFound();
            }
            marka = my.Find(x => x.Id == id.Value);

            my.Delete(marka);
            return RedirectToAction("Index", "Marka");


        }
    }
}