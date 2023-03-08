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
    [Table("TAracBakim")]
    public class AracBakim
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [DisplayName("Adı"), Required(ErrorMessage = "{0}  alanı gereklidir."), StringLength(50, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        public string Adi { get; set; }
        

    }
}
