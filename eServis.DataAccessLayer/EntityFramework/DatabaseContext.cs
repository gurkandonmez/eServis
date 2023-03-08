using eServis.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eServis.DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Adres> TAdres { get; set; }
        public DbSet<AdresTanimlama> TAdresTanimlama { get; set; }
        public DbSet<Personel> TPersonel { get; set; }
        public DbSet<Aciklama> TAciklama { get; set; }
        public DbSet<Aksesuar> TAksesuar { get; set; }
        public DbSet<Siralama> TSiralama { get; set; }
        public DbSet<Arac> TArac { get; set; }
        public DbSet<AracTanimlama> TAracTanimlama { get; set; }
        public DbSet<AracBakim> TAracBakim { get; set; }
        public DbSet<Bayi> TBayi { get; set; }
        public DbSet<Fis> TFis { get; set; }
        public DbSet<FisTanimlama> TFisTanimlama { get; set; }
        public DbSet<Marka> TMarka { get; set; }
        public DbSet<Urun> TUrun { get; set; }
        public DbSet<Malzeme>TMalzeme { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer(new MyInitializer());
        }

    }
}
