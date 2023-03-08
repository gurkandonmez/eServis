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
    public class FisTanimController : Controller
    {
        Fis fis = new Fis();
        FisTanimlama fisTanim = new FisTanimlama();
        FisYonetim fy = new FisYonetim();
        FisTanimlamaYonetim fty = new FisTanimlamaYonetim();
        Siralama sira = new Siralama();
        SiralamaYonetim sy = new SiralamaYonetim();
        // GET: FisTanim
        public ActionResult Index(int? id)
        {



            fisTanim = fty.Find(c => c.Id == id.Value);
            fis = fy.Find(x => x.Id == id);
            ViewBag.Baslik = fis.Adi;
            ViewBag.Id = fis.Id;
            Session["Id"] = fis.Id;
            sira = sy.Find(x => x.ID == 1);
            if (sira.FisTanimAlfabetikMi == true)
            {
                ViewBag.Fis = fty.List(x => x.FisId == id.Value).OrderBy(x => x.Baslik);


                return View();

            }
            else
            {
                ViewBag.Fis = fty.List(x => x.FisId == id.Value).OrderBy(x => x.Sira);
                return View();
            }


        }
        [HttpGet]
        public ActionResult Ekle()
        {
            return PartialView("_PartialFisTanimEkle");
        }

        public ActionResult ListA(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sira = sy.Find(x => x.ID == 1);
            sira.FisTanimAlfabetikMi = true;
            sy.Update(sira);

            ViewBag.Fis = fty.List(x => x.FisId == id).OrderBy(x => x.Baslik);

            return RedirectToAction("Index", "FisTanim", new { @id = id });
        }


        [HttpPost]
        public ActionResult Sirala(int? id, FormCollection form, FisTanimlama fisTanimlama)
        {


            fisTanimlama = fty.Find(x => x.Id == id);
            int FisId = fisTanimlama.Id;
            sira = sy.Find(x => x.ID == 1);
            sira.FisTanimAlfabetikMi = false;
            sy.Update(sira);

            FisTanimlama db_arac = fty.Find(x => x.Id == FisId);
            string[] BaslikId = form.GetValues("Id");
            string[] Sira = form.GetValues("Sira");
            ViewBag.AracListesi = fty.List(x => x.Id == FisId).OrderBy(x => x.Sira);
            for (int i = 0; i < BaslikId.Length; i++)
            {
                int BaslikId2 = Int32.Parse(BaslikId[i]);
                int sira2 = Int32.Parse(Sira[i]);
                fisTanimlama = fty.Find(x => x.Id == BaslikId2);
                fisTanimlama.Sira = sira2;
                fty.Update(fisTanimlama);
            }
            ViewBag.Fis = fty.List(x => x.FisId == id.Value).OrderBy(x => x.Sira);
            return RedirectToAction("Index", "FisTanim", new { @id = fisTanimlama.FisId });


            //  return RedirectToAction("Listele", "AdresTanim", new { @id = id });

        }




        [HttpGet]
        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fisTanim = fty.Find(x => x.Id == id.Value);
            return PartialView("_PartialFisTanimDuzenle", fisTanim);
        }


        [HttpGet]
        public ActionResult Sil(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fisTanim = fty.Find(x => x.Id == id.Value);
            if (fisTanim == null)
            {
                HttpNotFound();
            }
            return PartialView("_PartialFisTanimSil", fisTanim);
        }

        [HttpPost]
        public ActionResult Ekle(int? id, FisTanimlama fis)
        {

            ModelState.Remove("Adres_Id");
            ModelState.Remove("Adres");
            ModelState.Remove("Renk");
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    HttpNotFound();
                }

                fisTanim.FisId = id.Value;
                fisTanim.Kisaltma = fis.Kisaltma;
                fisTanim.Baslik = fis.Baslik;
                fty.Insert(fisTanim);
                return RedirectToAction("Index", "FisTanim", new { @id = id.Value });
            }
            else
            {
                return RedirectToAction("Index", "FisTanim", new { @id = id.Value });
            }

        }

        [HttpPost]
        public ActionResult Duzenle(int? id, FisTanimlama Gelenfis)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    HttpNotFound();
                }
                fisTanim = fty.Find(x => x.Id == id.Value);
                fisTanim.Baslik = Gelenfis.Baslik;
                fisTanim.Kisaltma = Gelenfis.Kisaltma;
                fty.Update(fisTanim);
                return RedirectToAction("Index", "FisTanim", new { @id = fisTanim.FisId });
            }
            else
            {
                return RedirectToAction("Index", "FisTanim", new { @id = id.Value });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sil(int? id, FisTanimlama fis)
        {

            if (id == null)
            {
                HttpNotFound();
            }
            fisTanim = fty.Find(x => x.Id == id.Value);
            var Id = fisTanim.FisId;
            fty.Delete(fisTanim);
            return RedirectToAction("Index", "FisTanim", new { @id = fisTanim.FisId });


        }
    }
}