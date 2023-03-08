﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eServis.Entities
{
    [Table("TAdresTanimlama")]
   public class AdresTanimlama
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AdresID { get; set; }
        [DisplayName("Başlık"), Required(ErrorMessage = "{0}  alanı gereklidir."), StringLength(200, ErrorMessage = "{0} alanı max {1} karakterdir.")]
        public string Baslik { get; set; }
        public int Sira { get; set; }
        public virtual List<Adres> Adres { get; set; }
    }
}
