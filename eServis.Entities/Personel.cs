using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace eServis.Entities
{
    [Table("TPersonel")]
    public class Personel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DisplayName("AdiSoyadi"), Required(ErrorMessage = "{0}  alanı gereklidir."), StringLength(80, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        public string AdSoyad { get; set; }

        //[DisplayName("Soyadı"), Required(ErrorMessage = "{0}  alanı gereklidir."), StringLength(20, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        //public string Soyad { get; set; }


        [DisplayName("TC Kimlik Numarası"), Required(ErrorMessage = "{0}  alanı gereklidir.")]
        public string KimlikNo { get; set; }

        [DisplayName("Sigorta Numarası"), Required(ErrorMessage = "{0}  alanı gereklidir.")]
        public string SigortaNo { get; set; }

        [DisplayName("Doğum Tarihi"), Required(ErrorMessage = "{0}  alanı gereklidir.")]
        public DateTime DogumTarihi { get; set; }

        [DisplayName("İşe Başlama Tarihi"), Required(ErrorMessage = "{0}  alanı gereklidir.")]
        public DateTime IseBaslamaTarihi { get; set; }

        [DisplayName("Eğitim Durumu"), Required(ErrorMessage = "{0}  alanı gereklidir."), StringLength(20, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        public string EgitimDurumu { get; set; }

        [DisplayName("Medeni Hali"), Required(ErrorMessage = "{0}  alanı gereklidir."), StringLength(20, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        public string MedeniHali { get; set; }

        [DisplayName("Çocuk Sayısı"), Required(ErrorMessage = "{0}  alanı gereklidir.")]
        public int CocukSayisi { get; set; }

        [DisplayName("Maaş"), Required(ErrorMessage = "{0}  alanı gereklidir.")]
        public decimal Maas { get; set; }

        public int Sira { get; set; }
        public bool Durum { get; set; }

        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0}  alanı gereklidir."), StringLength(20, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        public string KullaniciAdi { get; set; }

        [DisplayName("Şifre"), Required(ErrorMessage = "{0}  alanı gereklidir."), StringLength(20, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        public string Sifre { get; set; }
    }
}
