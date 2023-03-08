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
    public class AracController : Controller
    {
        private List<Arac> arac = new List<Arac>();
        private AracYonetim ay = new AracYonetim();
        public ActionResult Index()
        {
            arac = ay.List(x=>x.Durum==false);
            return View(arac);
        }
        public ActionResult Ekle()
        {
            return PartialView("_PartialAracEkle");
        }
        [HttpGet]
        public ActionResult Arsiv(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arac arac = ay.Find(x => x.ID == id.Value);
            if (arac == null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialAracArsiv", arac);
        }
        public ActionResult Tip()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Tip(AracBakim aracBakim)
        {
            if (ModelState.IsValid)
            {
                if (aracBakim.Adi!= null)
                {
                    HttpNotFound();
                }
                AracBakimYonetim aracBakimYonetim = new AracBakimYonetim();
              if (aracBakimYonetim.Insert(aracBakim)>0)
                { 

                return Json(new { Result = aracBakim });
                }
              else
                {
                    return Json("0");
                }
            


            }
            return Json("0");
        }
        [HttpGet]
       public ActionResult ArsivListesi()
        {
            arac = ay.List(x => x.Durum == true);
            return PartialView("_PartialAracArsivListesi",arac);
        }

      [HttpPost]
      public ActionResult Arsiv(int? id,Arac arac)
        {
            Arac db_arac = ay.Find(x => x.ID == id);


            if (db_arac != null)
            {
                db_arac.Durum = true;
                if(ay.Update(db_arac)>0)
                { 
                return RedirectToAction("Index");
                }
            }
            return View();
        }


        [HttpPost]
        public JsonResult ArsivListesi(int? id,Arac arac)
        {
            if (id == null)
            {
                HttpNotFound();
            }
            Arac db_arac = ay.Find(x => x.ID == id);
            if (db_arac != null)
            {
                db_arac.Durum = false;
                var durum = ay.Update(db_arac);
                if (durum > 0)
                {
                    return Json(new { Result = db_arac });
                }
                else
                {
                    return Json("0");
                }


            }
            return Json("0");
        }

        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arac arac = ay.Find(x => x.ID == id.Value);
            return PartialView("_PartialAracDuzenle", arac);
            //db_adres.Baslik = adres.Baslik;
            //db_adres.Sira = adres.Sira;

          
        }
        [HttpPost]
        public ActionResult Duzenle(int? id,Arac arac)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arac db_arac = ay.Find(x => x.ID == id.Value);

            db_arac.Plaka = arac.Plaka;
            db_arac.Adi = arac.Adi;
            db_arac.AracTakipNo = arac.AracTakipNo;
            ay.Update(db_arac);
            if (db_arac == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");

        }
        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arac arac = ay.Find(x => x.ID == id.Value);
            if (arac == null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialAracSil", arac);

        }

        [HttpPost]
        public ActionResult Sil(int? id, Arac arac)
        {
            Arac db_arac = ay.Find(x => x.ID == id);


            if (db_arac != null)
            {
                ay.Delete(db_arac);
                return RedirectToAction("Index");
            }
            return View();

        }
        [HttpPost]
        public ActionResult Ekle(Arac arac)
        {
            if (ModelState.IsValid)
            {

                ay.Insert(arac);
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}