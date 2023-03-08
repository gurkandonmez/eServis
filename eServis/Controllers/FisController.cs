using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eServis.BusinessLayer;
using eServis.Entities;
namespace eServis.Controllers
{
    public class FisController : Controller
    {

        List<Fis> fis = new List<Fis>();
        FisYonetim fy = new FisYonetim();
        // GET: Fis
        public ActionResult Index()
        {
            fis = fy.List();
            return View(fis);
        }
        public ActionResult Ekle()
        {
            return PartialView("_PartialFisEkle");
        }

        public ActionResult Duzenle(int? id)
        {
            Fis fis = fy.Find(x => x.Id == id.Value);

            return PartialView("_PartialFisDuzenle",fis);
        }

        [HttpPost]
        public ActionResult Duzenle(int? id, Fis fis)
        {
            if(id==null)
            {
                HttpNotFound();
            }
            Fis db_fis = fy.Find(x => x.Id == id.Value);
            db_fis.Adi = fis.Adi;
            fy.Update(db_fis);
            return RedirectToAction("Index");
        }


        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fis fis = fy.Find(x => x.Id == id.Value);
            if (fis == null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialFisSil", fis);

        }
        [HttpPost]
        public ActionResult Sil(int? id,Fis fis)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fis db_fis = fy.Find(x => x.Id == id.Value);
            if (fis == null)
            {
                return HttpNotFound();
            }
         fy.Delete(db_fis);
           return RedirectToAction("Index");

        }
        [HttpPost]
        public ActionResult Ekle(Fis fis )
        {
            if (ModelState.IsValid)
            {

                fy.Insert(fis);
                return RedirectToAction("Index");
            }
            return View();
        }
    
    }
  

}