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
    public class AdresController : Controller
    {

        private AdresYonetim ay = new AdresYonetim();

        public ActionResult Index()
        {
            return View(ay.List().OrderBy(x => x.Sira));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adres adres = ay.Find(x => x.Id == id.Value);

            //db_adres.Baslik = adres.Baslik;
            //db_adres.Sira = adres.Sira;

            if (adres == null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialAdresDuzenle", adres);
        }
        [HttpPost]
        public ActionResult Details(int? id,Adres adres)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adres db_adres = ay.Find(x => x.Id == id.Value);

            db_adres.Baslik = adres.Baslik;
            db_adres.Sira = adres.Sira;
            ay.Update(db_adres);
            if (adres == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            return PartialView("_PartialAdresEkle");
        }


        [HttpPost]
        public ActionResult Ekle(Adres adres)
        {
            if (ModelState.IsValid)
            {

                ay.Insert(adres);
                return RedirectToAction("Index");
            }

            return PartialView("_PartialAdresEkle");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adres adres = ay.Find(x => x.Id == id.Value);
            if (adres == null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialAdresSil", adres);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Adres adres = ay.Find(x => x.Id == id);

            ay.Delete(adres);
            return RedirectToAction("Index");

        }


    }
}
