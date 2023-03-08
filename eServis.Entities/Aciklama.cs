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
    [Table("TAciklama")]
  public  class Aciklama
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("Açıklama"), Required(ErrorMessage = "{0}  alanı gereklidir."), StringLength(200, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        public string Note { get; set; }
        public int NoteId { get; set; }
        public int Sira { get; set; }
        public  DateTime Tarih { get; set; }
        [Required]
        public int PersonelID { get; set; }
        public virtual List<Personel> Personel { get; set; }

    }
}
