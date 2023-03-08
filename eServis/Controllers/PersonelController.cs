using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eServis.BusinessLayer;
using eServis.Entities;
namespace eServis.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        List<Personel> personel = new List<Personel>();
        Personel db_personel = new Personel();
        PersonelYonetim py = new PersonelYonetim();
        Aciklama aciklama = new Aciklama();
        AciklamaYonetim ay = new AciklamaYonetim();
        Aciklama db_aciklama = new Aciklama();
        public ActionResult Index()
        {
            IQueryable<Personel> personel = py.ListQueryable().Where(x => x.Durum == false);
            return View(personel.ToList());
        }
        public ActionResult Ekle()
        {


            return PartialView("_PartialPersonelEkle");
        }

        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personel personel = py.Find(x => x.ID == id.Value);
            if (personel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialPersonelSil", personel);
        }
        [HttpPost]
        public ActionResult Sil(int? id, Personel personel)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personel db_personel = py.Find(x => x.ID == id.Value);
            if (db_personel == null)
            {
                return HttpNotFound();
            }
            py.Delete(db_personel);
            return RedirectToAction("Index");

        }

        public ActionResult PersonelSil(int? id)
        {
            if (id == null)
            {
                HttpNotFound();
            }
            db_personel = py.Find(x => x.ID == id.Value);

            if (py.Delete(db_personel) > 0)
            {
                return Json(new { Result = db_personel });
            }
            else
            {
                return Json("0");
            }
        }

        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personel personel = py.Find(x => x.ID == id.Value);
            if (personel == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = id.Value;
            Session["Id"] = id.Value;
            return PartialView("_PartialPersonelGuncelle", personel);

        }

        [HttpPost]
        public ActionResult Duzenle(int? id, Personel personel)
        {
            id = Convert.ToInt32(Session["Id"]);
            if (id == null)
            {
                HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                Personel db_personel = py.Find(x => x.ID == id);
                db_personel.AdSoyad = personel.AdSoyad;
         
                db_personel.CocukSayisi = personel.CocukSayisi;
                db_personel.DogumTarihi = Convert.ToDateTime(personel.DogumTarihi.ToShortDateString());
                db_personel.EgitimDurumu = personel.EgitimDurumu;
                db_personel.IseBaslamaTarihi = Convert.ToDateTime(personel.IseBaslamaTarihi.ToShortDateString());
                db_personel.KimlikNo = personel.KimlikNo;
                db_personel.Maas = personel.Maas;
                db_personel.MedeniHali = personel.MedeniHali;
                db_personel.SigortaNo = personel.SigortaNo;
                db_personel.Sira = personel.Sira;
                var durum = py.Update(db_personel);
                if (durum == 1)
                {
                    return Json("1");
                }
                else

                {
                    return Json("0");
                }
            }
            return Json('0');
        }

        [HttpGet]
        public ActionResult AciklamaEkle(int? id)
        {
            Session["Id"] = id;
            return PartialView("_PartialAciklama");
        }
        [HttpPost]
        public JsonResult AciklamaEkle(Aciklama formValues)
        {
            formValues.PersonelID = Convert.ToInt32(Session["Id"]);
            if (ModelState.IsValid)
            {
                Random rnd = new Random();
                int NoteId = rnd.Next(100, 1000) + rnd.Next(1, 10) + rnd.Next(0, 800);
                var tarih = DateTime.Now.ToShortDateString();
                db_aciklama.PersonelID = formValues.PersonelID;
                db_aciklama.Tarih = Convert.ToDateTime(tarih.ToString());
                db_aciklama.NoteId = NoteId;
                db_aciklama.Note = formValues.Note;
                var durum = ay.Insert(db_aciklama);
                if (durum == 1)
                {

                    return Json(new
                    {
                        Result = db_aciklama
                    });
                }
                else
                {
                    return Json("0");
                }

            }
            return Json("0");


        }

        [HttpPost]
        public ActionResult AciklamaSil(int? id, Aciklama formValues)
        {
            if (id == null)
            {
                HttpNotFound();
            }
            db_aciklama = ay.Find(x => x.NoteId == id);
            if (db_aciklama != null)
            {
                if (ay.Delete(db_aciklama) > 0)
                {
                    return Json(new { Result = db_aciklama });
                }
                else
                {
                    return Json("0");
                }
            }
            else
            {
                return Json("1");
            }



        }

        [HttpGet]
        public ActionResult Arsiv(int? id)
        {
            if (id == null)
            {
                HttpNotFound();
            }
            Session["ID"] = id;
            Personel personel = py.Find(x => x.ID == id.Value);
            return PartialView("_PartialPersonelArsiv", personel);

        }
        public ActionResult ArsivListesi()
        {
            personel = py.List(x => x.Durum == true);

            return PartialView("_PartialPersonelArsivListesi",personel);
        }

        [HttpPost]

        public JsonResult Arsiv(int? id, Personel personel)
        {
            if (id == null)
            {
                HttpNotFound();
            }
            Personel db_personel = py.Find(x => x.ID == id.Value);
            if (db_personel != null)
            {
                db_personel.Durum = true;
                var durum = py.Update(db_personel);
                if (durum > 0)
                {
                    return Json(new { Result = db_personel });
                }
                else
                {
                    return Json("0");
                }


            }
            return Json("0");
        }
        [HttpPost]
        public ActionResult ArsivCikar(string id)
        {
            if (id == null)
            {
                HttpNotFound();
            }
            Personel db_personel = py.Find(x => x.KimlikNo == id);
            if (db_personel != null)
            {
                db_personel.Durum = false;
                var durum = py.Update(db_personel);
                if (durum > 0)
                {
                    return Json(new { Result = db_personel });
                }
                else
                {
                    return Json("0");
                }


            }
            return Json("0");
        }
        [HttpPost]
        public ActionResult Ekle(Personel personel)
        {
            ModelState.Remove("Aciklama");
            ModelState.Remove("KullainciAdi");
            ModelState.Remove("Sifre");
            
            if (ModelState.IsValid)
            {
                if (personel == null)
                {
                    HttpNotFound();
                }
                db_personel.AdSoyad = personel.AdSoyad;
     
                db_personel.CocukSayisi = personel.CocukSayisi;
                db_personel.DogumTarihi = Convert.ToDateTime(personel.DogumTarihi.ToShortDateString());
                db_personel.EgitimDurumu = personel.EgitimDurumu;
                db_personel.IseBaslamaTarihi = Convert.ToDateTime(personel.IseBaslamaTarihi.ToShortDateString());
                db_personel.KimlikNo = personel.KimlikNo;
                db_personel.Maas = personel.Maas;
                db_personel.MedeniHali = personel.MedeniHali;
                db_personel.SigortaNo = personel.SigortaNo;
                db_personel.Sira = personel.Sira;
                var durum = py.Insert(db_personel);
                if (durum == 1)
                {
                    return Json("1");
                }
                else

                {
                    return Json("0");
                }


            }
            return Json("0");
        }
    }

}