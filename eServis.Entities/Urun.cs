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
    [Table("TUrun")]
  public  class Urun
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int UrunID { get; set; }
        [DisplayName("Ürün"), StringLength(100, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        public string Baslik { get; set; }
        public string Kod { get; set; }
        public int Sira { get; set; }
    }
}
