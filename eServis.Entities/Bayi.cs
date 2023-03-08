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
    [Table("TBayi")]
    public class Bayi
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Adı"), Required(ErrorMessage = "{0}  alanı gereklidir."), StringLength(20, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        public string Adi { get; set; }
        public int Sira { get; set; }
        public bool Durum { get; set; }

    }
}
