using eServis.BusinessLayer;
using eServis.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eServis.Controllers
{
    public class UrunController : Controller
    {
        Urun urun = new Urun();
        UrunYonetim uy = new UrunYonetim();
        Siralama sira = new Siralama();
        SiralamaYonetim sy = new SiralamaYonetim();
        public ActionResult Index()
        {
            sira = sy.Find(x => x.ID == 1);
            if (sira.UrunAlfabetikMi == true)
            {
                ViewBag.Urun = uy.List().OrderBy(x => x.Baslik);


                return View();

            }
            else
            {
                ViewBag.Urun = uy.List().OrderBy(x => x.Sira);

                return View();
            }
        }
        public ActionResult Ekle()
        {
            return PartialView("_PartialUrunEkle");
        }
        [HttpPost]
        public JsonResult Ekle(Urun urun)
        {

            if (ModelState.IsValid)
            {
                if (urun.Baslik != null)
                {
                    if (uy.Insert(urun) > 0)
                    {
                        return Json(new { Result = urun });

                    }
                }
            }

            return Json("0");


        }
        [HttpGet]
        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                HttpNotFound();
            }
            urun = uy.Find(x => x.ID == id.Value);
            if (urun == null)
            {
                HttpNotFound();
            }
            return PartialView("_PartialUrunSil", urun);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sil(int id)
        {
            if (id == null)
            {
                HttpNotFound();
            }
            urun = uy.Find(x => x.ID == id);
            if (urun == null)
            {
                HttpNotFound();
            }
            uy.Delete(urun);
            return RedirectToAction("Index");
        }
        public ActionResult Duzenle(int? id)
        {
            ViewBag.Id = id;
            if (id == null)
            {
                HttpNotFound();
            }
            urun = uy.Find(x => x.ID == id.Value);
            if (urun == null)
            {
                HttpNotFound();
            }
            return PartialView("_PartialUrunGuncelle", urun);
        }
        [HttpPost]
        public ActionResult Duzenle(int id, Urun urun)

        {
          
            if (ModelState.IsValid)
            {
                Urun db_urun = uy.Find(x => x.ID == id);
                if (db_urun.Baslik != null)
                {
                    db_urun.Baslik = urun.Baslik;
                    db_urun.Kod = urun.Kod;
                    if (uy.Update(urun) > 0)
                    {
                        return Json(new { Result = db_urun });

                    }
                }
            }

            return Json("0");
        }

        public ActionResult ListA()
        {


            sira = sy.Find(x => x.ID == 1);
            sira.UrunAlfabetikMi = true;
            sy.Update(sira);

            ViewBag.Urun = uy.List().OrderBy(x => x.Baslik);

            return RedirectToAction("Index", "Urun");
        }


        [HttpPost]
        public ActionResult Sirala(int? id, FormCollection form, Urun urun)
        {


            urun = uy.Find(x => x.ID == id);
            int Id = urun.ID;
            sira = sy.Find(x => x.ID == 1);
            sira.UrunAlfabetikMi = false;
            sy.Update(sira);

            Urun db_marka = uy.Find(x => x.ID == Id);
            string[] BaslikId = form.GetValues("ID");
            string[] Sira = form.GetValues("Sira");
            ViewBag.Marka = uy.List().OrderBy(x => x.Sira);
            for (int i = 0; i < BaslikId.Length; i++)
            {
                int BaslikId2 = Int32.Parse(BaslikId[i]);
                int sira2 = Int32.Parse(Sira[i]);
                urun = uy.Find(x => x.ID == BaslikId2);
                urun.Sira = sira2;
                uy.Update(urun);
            }
            ViewBag.Urun = uy.List().OrderBy(x => x.Sira);
            return RedirectToAction("Index", "Urun");


            //  return RedirectToAction("Listele", "AdresTanim", new { @id = id });

        }


    }
}