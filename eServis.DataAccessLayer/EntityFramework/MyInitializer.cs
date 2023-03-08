using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using eServis.Entities;

namespace eServis.DataAccessLayer
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            Fis IslemT = new Fis()
            {
                Adi = "İŞLEM TİPİ TANIMLAMALARI"
            };
            Fis IscilikTipi = new Fis()
            {
                Adi = "İŞÇİLİK TİPİ  TANIMLARI"

            };
            Fis HizmetTipi = new Fis()
            {
                Adi = "HİZMET TİPİ TANIMLARI"
            };
              Fis IslemDurumu = new Fis()
              {
                Adi = "İŞLEM DURUMU"
              };

            context.TFis.Add(IslemT);
            context.TFis.Add(IscilikTipi);
            context.TFis.Add(HizmetTipi);
            context.TFis.Add(IslemDurumu);
            context.SaveChanges();

            FisTanimlama IslemTipiTanimlama = new FisTanimlama()
            {
        
                FisId = 1,
                Baslik = "GARANTİ HARİCİ",
                Kisaltma = "GH"
            };
            FisTanimlama IslemTipiTanimlama2 = new FisTanimlama()
            {
                FisId = 1,
                Baslik = "RANDEVU ERTELEME",
                Kisaltma = "ERTL"
            };

            FisTanimlama IslemTipiTanimlama3 = new FisTanimlama()
            {
                FisId = 1,
                Baslik = "GARANTİ DAHİLİ",
                Kisaltma = "GD"
            };
            context.TFisTanimlama.Add(IslemTipiTanimlama);
            context.TFisTanimlama.Add(IslemTipiTanimlama2);
            context.TFisTanimlama.Add(IslemTipiTanimlama3);
            context.SaveChanges();



            FisTanimlama IscilikTipiTanimlama = new FisTanimlama()
            {
                FisId = 2,
                Baslik = "MALZEMELİ İŞÇİLİK",
                Kisaltma = "MLİ"
            };
            FisTanimlama IscilikTipiTanimlama2 = new FisTanimlama()
            {
                FisId = 2,
                Baslik = "MALZEME SİPARİŞ",
                Kisaltma = "MSİP"
            };
            FisTanimlama IscilikTipiTanimlama3 = new FisTanimlama()
            {
                FisId = 2,
                Baslik = "MALZEMESİZ İŞÇİLİK",
                Kisaltma = "MSİZ"
            };

            context.TFisTanimlama.Add(IscilikTipiTanimlama);
            context.TFisTanimlama.Add(IscilikTipiTanimlama2);
            context.TFisTanimlama.Add(IscilikTipiTanimlama3);
         
            FisTanimlama HizmetTipiTanimlama = new FisTanimlama()
            {
                FisId=3,
                Baslik = "BAYİDEN CİHAZ TESLİM"
            };
            FisTanimlama HizmetTipiTanimlama2 = new FisTanimlama()
            {
                FisId=3,
                Baslik = "ÜCRETLİ MONTAJ"
            };
            FisTanimlama HizmetTipiTanimlama3 = new FisTanimlama()
            {
                FisId=3,
                Baslik = "NAKLİYE"
            };
            context.TFisTanimlama.Add(HizmetTipiTanimlama);
            context.TFisTanimlama.Add(HizmetTipiTanimlama2);
            context.TFisTanimlama.Add(HizmetTipiTanimlama3);
          
            FisTanimlama IslemTipi = new FisTanimlama()
            {
                FisId = 4,
                Baslik = "YENİ İŞ"
            };

            FisTanimlama IslemTipi2 = new FisTanimlama()
            {
                FisId = 4,
                Baslik = "SONUÇLANMIŞ İŞ"
            };
            context.TFisTanimlama.Add(IslemTipi);
            context.TFisTanimlama.Add(IslemTipi2);
            context.SaveChanges();
            Siralama sira = new Siralama()
            {
                UrunAlfabetikMi = false,
                AksesuarAlfabetikMi = false,
                AracTanimlamaAlfabetikMi = false,
                FisTanimAlfabetikMi = false,
                MarkaAlfabetikMi = false,
                BayiAlfabetikMi = false,
                AracAlfabetikMi = false,
                MalzemeAlfabetikMi = false



            };
              context.TSiralama.Add(sira);
             context.SaveChanges();

        }
        
      
      
          
    }
}

