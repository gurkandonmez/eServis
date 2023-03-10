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
    [Table("TMarka")]
   public class Marka
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("Marka"),  StringLength(100, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        public string Baslik { get; set; }
        public int Sira { get; set; }
        public string Renk { get; set; }
        public bool Durum { get; set; }
    }
}
