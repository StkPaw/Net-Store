using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace posss
{
    public class Model : DbContext
    {
        //Here we are creating a connection with database file
        public DbSet<Produkt> Produkty { get; set; }
        public DbSet<Transakcja> Transakcje { get; set; }

        public string DbPath { get; private set; }
        
        

        public Model()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}produkty.db";
            Database.EnsureCreated();
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }

    // And here we specifing model for stored data
    public class Produkt
    {
        public int ProduktId { get; set; }
        public string Nazwa { get; set; }
        public string KodKreskowy { get; set; }
        public double Cena { get; set; }
        public int Mag  { get; set; }
        public ICollection<Transakcja> transaks { get; set; }
    }
    public class Transakcja
    {
        
        public int TransakcjaId { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Produkt> produkts { get; set; }
        public string ilosc { get; set; }
        public string typ { get; set; }
    
    }
}
