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
    [Table("TAracTanimlama")]
    public class AracTanimlama
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int AracId { get; set; }
        [DisplayName("Adı"), Required(ErrorMessage = "{0}  alanı gereklidir."), StringLength(20, ErrorMessage = "{0} alanı max {1} karakterdir.")]

        public string Adi { get; set; }

        public DateTime Tarih { get; set; }

        [DisplayName("Km"), StringLength(20, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        public string Km { get; set; }

        [DisplayName("Tedarik"), StringLength(20, ErrorMessage = "{0} alanı max {1} karakterdir.")]

        public string Tedarik { get; set; }
        [DisplayName("Ücret")]
        public decimal Ucret { get; set; }
        [DisplayName("Açıklama"), MaxLength(5000, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        public string Aciklama { get; set; }
        [DisplayName("Araç Takip Numarası")]
        public string AracTakipNo { get; set; }
        public int Sira { get; set; }
        public virtual Arac Arac { get; set; }
       

    }
}
