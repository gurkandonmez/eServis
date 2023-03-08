using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eServis.BusinessLayer;
using eServis.Entities;

namespace eServis.Controllers
{
    public class AdresTanimController : Controller
    {
        private AdresTanimlama AdresTanimlama = new AdresTanimlama();
        private AdresTanimlamaYonetim ay = new AdresTanimlamaYonetim();

        private AdresYonetim adres = new AdresYonetim();
        private AdresYonetim ady = new AdresYonetim();

        public ActionResult ListA(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adres adres = ady.Find(x => x.Id == id);

            adres.AlfabetikMi = true;
            ady.Update(adres);
  
            ViewBag.adres = ay.List(x => x.AdresID == id).OrderBy(x => x.Baslik);

            return RedirectToAction("Listele","AdresTanim", new{@id=id});
        }



        public ActionResult Listele(int? id, Adres gelenAdres)
        {

            
            Adres db_adres = ady.Find(x => x.Id == id);
            ViewBag.Baslik = db_adres.Baslik;
            ViewBag.Id = db_adres.Id;
            ViewBag.Id = id;
            if (db_adres.AlfabetikMi == true)
            {
                ViewBag.adres = ay.List(x => x.AdresID == id).OrderBy(x => x.Baslik);
            }
            else
            { 
            ViewBag.adres = ay.List(x => x.AdresID == id).OrderBy(x => x.Sira);
            }
            if (ViewBag.adres == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Sirala(int? id, FormCollection form, AdresTanimlama adres,Adres gelenAdres)
        {
            AdresYonetim adresYonetim = new AdresYonetim();

          AdresTanimlama adresTanimlama = ay.Find(x => x.Id ==id);
            int AdresId = adresTanimlama.AdresID;
            Adres ad = adresYonetim.Find(x => x.Id == AdresId);
            ad.AlfabetikMi = false;
            adresYonetim.Update(ad);
          
            AdresTanimlama db_adres = ay.Find(x => x.AdresID == id);
            string[] BaslikId = form.GetValues("Id");
            string[] Sira = form.GetValues("Sira");
            ViewBag.adres = ay.List(x => x.AdresID == id.Value).OrderBy(x => x.Sira);
            for (int i = 0; i < BaslikId.Length; i++)
            {
                int BaslikId2 = Int32.Parse(BaslikId[i]);
                int sira2 = Int32.Parse(Sira[i]);
                adres = ay.Find(x => x.Id == BaslikId2);
                adres.Sira = sira2;
                ay.Update(adres);
            }
            ViewBag.adres = ay.List(x => x.AdresID == id.Value).OrderBy(x => x.Sira);
            return RedirectToAction("Listele", "AdresTanim", new { @id = adres.AdresID });


            //  return RedirectToAction("Listele", "AdresTanim", new { @id = id });

        }

        public ActionResult Ekle()
        {
            return PartialView("_ModalEklePartial");
        }


        [HttpPost]
        public ActionResult Ekle(int? id, AdresTanimlama adresTanimlama)
        {

            if (ModelState.IsValid)
            {

                adresTanimlama.AdresID = id.Value;
                adresTanimlama.Sira = 1;
                ay.Insert(adresTanimlama);
                return RedirectToAction("Listele", "AdresTanim", new { @id = id });
            }
            var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
            //   ViewBag.adres = new SelectList(adres.List(), "Id", "Baslik", adresTanimlama.AdresID);
            return RedirectToAction("Listele", "AdresTanim", new { @id = ViewBag.Id });
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdresTanimlama adres = ay.Find(x => x.Id == id.Value);
            if (adres == null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialAdresTanimSil", adres);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            AdresTanimlama adres = ay.Find(x => x.Id == id);

            ay.Delete(adres);
            return RedirectToAction("Listele", "AdresTanim", new { @id = adres.AdresID });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(int? id, AdresTanimlama adres)
        {
            if (ModelState.IsValid)
            {

                AdresTanimlama db_adres = ay.Find(x => x.Id == id.Value);
                db_adres.Baslik = adres.Baslik;

                ay.Update(db_adres);
                return RedirectToAction("Listele", "AdresTanim", new { @id = db_adres.AdresID });
            }

            return View(adres);
        }


        public ActionResult Duzenle(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdresTanimlama adres = ay.Find(x => x.Id == id.Value);
            if (adres == null)
            {
                return HttpNotFound();
            }
            // ViewBag.CategoryId = new SelectList(cm.List(), "Id", "Title", note.CategoryId);
            return PartialView("_PartialAdresTanimDuzenle", adres);
        }

    }
}