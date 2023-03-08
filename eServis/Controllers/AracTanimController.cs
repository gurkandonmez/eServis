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

    public class AracTanimController : Controller
    {
        private AracBakimYonetim aracBakimYonetim = new AracBakimYonetim();

        List<Arac> arac = new List<Arac>();
        AracYonetim aracYonetim = new AracYonetim();
        AracTanimlama aracTanimlama = new AracTanimlama();
        AracTanimlamaYonetim aty = new AracTanimlamaYonetim();
        Siralama sira = new Siralama();
        SiralamaYonetim syonetim = new SiralamaYonetim();
        // GET: AracTanim
        public ActionResult Index(int? id)
        {

            if (id != null)
            {
                Arac db_arac = new Arac();
                db_arac = aracYonetim.Find(z => z.ID == id.Value);
                ViewBag.Baslik = db_arac.Plaka + " " + db_arac.Adi;
                ViewBag.Id = db_arac.ID;

                sira = syonetim.Find(x => x.ID == 1);
                if (sira.AracTanimlamaAlfabetikMi == true)
                {
                    ViewBag.AracListesi = aty.List(x => x.AracId == id.Value).OrderBy(x => x.Adi);
                }
                else
                {
                    ViewBag.AracListesi = aty.List(x => x.AracId == id.Value).OrderBy(x => x.Sira);
                }

                return View();
            }
            return HttpNotFound();
        }
        public ActionResult ListA(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sira = syonetim.Find(x => x.ID == 1);
            sira.AracTanimlamaAlfabetikMi = true;
            syonetim.Update(sira);

            ViewBag.AracListesi = aty.List(x => x.AracId == id).OrderBy(x => x.Adi);

            return RedirectToAction("Index", "AracTanim", new { @id = id });
        }


        [HttpPost]
        public ActionResult Sirala(int? id, FormCollection form, AracTanimlama aracTanimlama)
        {


            aracTanimlama = aty.Find(x => x.ID == id);
            int AracId = aracTanimlama.ID;
            sira = syonetim.Find(x => x.ID == 1);
            sira.AracTanimlamaAlfabetikMi = false;
            syonetim.Update(sira);

            AracTanimlama db_arac = aty.Find(x => x.ID == AracId);
            string[] BaslikId = form.GetValues("ID");
            string[] Sira = form.GetValues("Sira");
            ViewBag.AracListesi = aty.List(x => x.ID == AracId).OrderBy(x => x.Sira);
            for (int i = 0; i < BaslikId.Length; i++)
            {
                int BaslikId2 = Int32.Parse(BaslikId[i]);
                int sira2 = Int32.Parse(Sira[i]);
                aracTanimlama = aty.Find(x => x.ID == BaslikId2);
                aracTanimlama.Sira = sira2;
                aty.Update(aracTanimlama);
            }
            ViewBag.AracListesi = aty.List(x => x.AracId == id.Value).OrderBy(x => x.Sira);
            return RedirectToAction("Index", "AracTanim", new { @id = aracTanimlama.AracId });


            //  return RedirectToAction("Listele", "AdresTanim", new { @id = id });

        }



        [HttpPost]
        public ActionResult Ekle(int? id, AracTanimlama arac, FormCollection c)
        {
            if (ModelState.IsValid)
            {


                arac.AracId = id.Value;
                aty.Insert(arac);
                return RedirectToAction("Index", "AracTanim", new { @id = id.Value });

            }

            return RedirectToAction("Index", "AracTanim", new { @id = id.Value });


        }
        [HttpGet]
        public ActionResult Tip()
        {
            return PartialView("_PartialAracTip");
        }
        [HttpPost]
        public ActionResult Tip(AracBakim aracBakim)
        {
            if (ModelState.IsValid)
            {
                if (aracBakim.Adi == null)
                {
                    HttpNotFound();
                }
                AracBakimYonetim aracBakimYonetim = new AracBakimYonetim();
                if (aracBakimYonetim.Insert(aracBakim) > 0)
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
        public ActionResult Ekle(int id)
        {


            List<AracBakim> arac = new List<AracBakim>();
            AracBakimYonetim aracBakimYonetim = new AracBakimYonetim();

            ViewBag.Adi = new SelectList(aracBakimYonetim.List(), "Adi", "Adi");
            return PartialView("_PartialAracTanimlamaEkle");
        }

        [HttpPost]

        public ActionResult Sil(int id)
        {
            AracTanimlama arac = aty.Find(x => x.ID == id);
            ViewBag.Id = arac.AracId;
            aty.Delete(arac);
            return RedirectToAction("Index", "AracTanim", new { @id = ViewBag.Id });

        }

        [HttpGet]
        public ActionResult Sil(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AracTanimlama arac = aty.Find(x => x.ID == id.Value);
            if (arac == null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialAracTanimlamaSil", arac);

        }
        [HttpPost]
        public ActionResult Guncelle(int id, AracTanimlama arac)
        {
            AracTanimlama db_arac = aty.Find(x => x.ID == id);
            ViewBag.Id = db_arac.AracId;
            db_arac.Adi = arac.Adi;
            db_arac.Tedarik = arac.Tedarik;
            db_arac.Tarih = arac.Tarih;
            db_arac.Ucret = arac.Ucret;
            db_arac.Km = arac.Km;
            db_arac.Aciklama = arac.Aciklama;

            aty.Update(db_arac);
            return RedirectToAction("Index", "AracTanim", new { @id = ViewBag.Id });

        }

        [HttpGet]
        public ActionResult Guncelle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AracTanimlama arac = aty.Find(x => x.ID == id.Value);
            if (arac == null)
            {
                return HttpNotFound();
            }


            ViewBag.Adi = new SelectList(aracBakimYonetim.List(), "Adi", "Adi");
            return PartialView("_PartialAracTanimlamaGuncelle", arac);

        }
    }
}