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
    public class AksesuarController : Controller
    {
        AksesuarYonetim ay = new AksesuarYonetim();
        SiralamaYonetim sy = new SiralamaYonetim();

        public ActionResult Index()
        {

            // ViewBag.CategoryId = new SelectList(cm.List(), "Id", "Title", note.CategoryId);
            ViewBag.Aksesuar = ay.List().OrderBy(x => x.Sira);


            return View();
        }

        public ActionResult ListA()
        {
            Siralama siralama = sy.Find(x => x.ID == 1);

            siralama.AksesuarAlfabetikMi = true;
            sy.Update(siralama);

            ViewBag.Aksesuar = ay.List().OrderBy(x => x.Ad);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Sirala(FormCollection form)
        {
            //AdresYonetim adresYonetim = new AdresYonetim();
            if (form.Count == 0)
            {

                ViewBag.List = ay.List();
                foreach (var item in ViewBag.List)
                {
                    form.Add("Id", item("Id"));
                    form.Add("Sira", item("Sira"));
                }

            }
            Siralama siralama = sy.Find(x => x.ID == 1);
            siralama.AksesuarAlfabetikMi = false;
            sy.Update(siralama);

            string[] BaslikId = form.GetValues("Id");
            string[] Sira = form.GetValues("Sira");
            for (int i = 0; i < BaslikId.Length; i++)
            {
                int BaslikId2 = Int32.Parse(BaslikId[i]);
                int sira2 = Int32.Parse(Sira[i]);
                Aksesuar aksesuar = ay.Find(x => x.Id == BaslikId2);
                if (aksesuar != null)
                {
                    aksesuar.Sira = sira2;
                    ay.Update(aksesuar);

                }

            }
            ViewBag.Aksesuar = ay.List().OrderBy(x => x.Sira);
            return RedirectToAction("Index", "Aksesuar");


            //  return RedirectToAction("Listele", "AdresTanim", new { @id = id });

        }





        [HttpPost]
        public JsonResult AksesuarSil(int id, Aksesuar formValues)
        {
            formValues = ay.Find(x => x.KodId == id);
            var durum = ay.Delete(formValues);
            if (durum > 0)
            {
                return Json("1");
            }
            else
            {
                return Json("0");
            }
        }


        public JsonResult Guncelle(int? id, Aksesuar formValues)
        {
            var veri = ay.List().FirstOrDefault(x => x.Id == id);


            return Json(new { Result = new { veri.Kod, veri.Ad, veri.Fiyat, veri.TknPrim, veri.Aciklama } }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AksesuarGuncelle(int? id, Aksesuar formValues)
        {
            Aksesuar db_akse = new Aksesuar();
            ModelState.Remove("Aciklama");
            ModelState.Remove("Sira");
            ModelState.Remove("Id");
            db_akse = ay.List().FirstOrDefault(x => x.KodId == id);
            if (formValues.Ad != null)
            {
                /// int KodId = id + 1;
                /// 
                formValues.Id = db_akse.Id;
                formValues.KodId = db_akse.KodId;
                db_akse.Kod = formValues.Kod;
                db_akse.Ad = formValues.Ad;
                db_akse.Fiyat = formValues.Fiyat;
                db_akse.TknPrim = formValues.TknPrim;
                db_akse.Aciklama = formValues.Aciklama;
                formValues.Sira = db_akse.Sira;
                var durum = ay.Update(db_akse);
                if (durum > 0)
                {
                    return Json(new
                    {
                        Result = new
                        {
                            db_akse.Id,
                            db_akse.KodId,
                            db_akse.Kod,
                            db_akse.Ad,
                            db_akse.Fiyat,
                            db_akse.TknPrim,
                            db_akse.Aciklama,
                            db_akse.Sira
                        }
                    });
                }
                else
                {
                    return Json("0");
                }

            }
            return Json('a');

        }

        //string Kod,string Ad, decimal Fiyat,int TknPrim, string Aciklama
        [HttpPost]
        public JsonResult AksesuarEkle(Aksesuar formValues)
        {

            ModelState.Remove("Aciklama");
            // ModelState.Remove("Sira");
            //  ModelState.Remove("Id");

            if (ModelState.IsValid)
            {


                Aksesuar db_akse = new Aksesuar();
                Random rnd = new Random();
                var id = rnd.Next(1, 900) + rnd.Next(1, 800) + rnd.Next(1, 20);

                var deger = ay.List().FirstOrDefault(x => x.Id == id);
                if (deger == null)
                {
                    int KodId = id + 1;
                    db_akse.KodId = KodId;
                    formValues.KodId = KodId;
                    db_akse.Kod = formValues.Kod;
                    db_akse.Ad = formValues.Ad;
                    db_akse.Fiyat = formValues.Fiyat;
                    db_akse.TknPrim = formValues.TknPrim;
                    db_akse.Aciklama = formValues.Aciklama;
                    formValues.Sira = 1;
                    ay.Insert(formValues);
                    return Json(new
                    {
                        Result = new
                        {
                            Id = db_akse.Id,
                            KodId = db_akse.KodId,
                            Kod = db_akse.Kod,
                            Ad = db_akse.Ad,
                            Fiyat = db_akse.Fiyat,
                            TknPrim = db_akse.TknPrim,
                            Aciklama = db_akse.Aciklama
                        }
                    }, JsonRequestBehavior.AllowGet);
                }
                else if (deger != null)
                {

                }

            }
            return Json(new { Result = new { formValues } });


        }
    }




}
