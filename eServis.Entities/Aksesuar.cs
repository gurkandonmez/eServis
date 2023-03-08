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
    [Table("TAksesuar")]
    public class Aksesuar
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("Kodu"), Required(ErrorMessage = "{0}  alanı gereklidir."), StringLength(100, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        public string Kod { get; set; }

        public int KodId { get; set; }

        public int Sira { get; set; }

        [DisplayName("Aksesuar Adı"), Required(ErrorMessage = "{0}  alanı gereklidir."), StringLength(100, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        public string Ad { get; set; }

        [DisplayName("Aksesuar Fiyatı"), Required(ErrorMessage = "{0}  alanı gereklidir.")]
        public decimal Fiyat { get; set; }
        [DisplayName("TKN Primi"), Required(ErrorMessage = "{0}  alanı gereklidir.")]
        public int TknPrim { get; set; }

        [DisplayName("TKN Primi"), StringLength(400, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        public string Aciklama { get; set; }

    }
}
