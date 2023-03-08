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
    [Table("TArac")]
    public class Arac
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Adi { get; set; } [StringLength(50, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        public string Plaka { get; set; }[StringLength(11, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        public string AracTakipNo { get; set; }
        public int Sira { get; set; }
        public bool Durum { get; set; }
    }
}
