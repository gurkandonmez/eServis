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
    [Table("TSiralama")]
    public class Siralama
    {

        public int ID { get; set; }

        public bool AksesuarAlfabetikMi { get; set; } 

        public bool AracAlfabetikMi { get; set; } 
        public bool AracTanimlamaAlfabetikMi { get; set; }

        public bool BayiAlfabetikMi { get; set; } 
 
        public bool FisTanimAlfabetikMi { get; set; } 

        public bool MarkaAlfabetikMi { get; set; } 

        public bool UrunAlfabetikMi { get; set; }

        public bool MalzemeAlfabetikMi { get; set; }


    }
}
