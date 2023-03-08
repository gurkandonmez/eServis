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
    [Table("TMalzeme")]
    public  class Malzeme
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [DisplayName("AdSoyad"), Required(ErrorMessage = "{0}  alanı gereklidir."), StringLength(20, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        public string AdSoyad { get; set; }
        [DisplayName("Ad"), Required(ErrorMessage = "{0}  alanı gereklidir."), StringLength(20, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        public string MalzemeAdi { get; set; }
        [DisplayName("Kodu"), Required(ErrorMessage = "{0}  alanı gereklidir."), StringLength(20, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        public string Kod { get; set; }
        [DisplayName("Adet"), Required(ErrorMessage = "{0}  alanı gereklidir.")]
        public int Adet { get; set; }
        public int Sira { get; set; }
        public bool Durum { get; set; }
        public virtual Personel Personel { get; set; }

    }
}
