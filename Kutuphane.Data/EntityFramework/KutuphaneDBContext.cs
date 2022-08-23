using Kutuphane.Data.Entities;
using Kutuphane.Data.EntityFramework.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Kutuphane.Data.EntityFramework
{
    public class KutuphaneDBContext : DbContext
    {
        public DbSet<tblKitaplar> tblKitaplar { get; set; }
        public DbSet<tblKitapHareketler> tblKitapHareketler { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //            .AddJsonFile("appsettings.json")
            //            .Build();
            //optionsBuilder.UseSqlServer(configuration.GetConnectionString("SQLServerProvider"));

            optionsBuilder.UseSqlServer(@"Server=.;Database=VeriparkDB;Trusted_Connection=True;Connect Timeout=30;MultipleActiveResultSets=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new tblKitaplar_Map());
            modelBuilder.ApplyConfiguration(new tblKitapHareketler_Map());
        }
    }
}
