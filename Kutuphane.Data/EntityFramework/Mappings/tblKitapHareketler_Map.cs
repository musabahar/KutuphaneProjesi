using Kutuphane.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Kutuphane.Data.EntityFramework.Mappings
{
    public class tblKitapHareketler_Map : IEntityTypeConfiguration<tblKitapHareketler>
    {
        public void Configure(EntityTypeBuilder<tblKitapHareketler> builder)
        {
            builder.HasKey(a => a.KitapHareketID);
            builder.Property(a => a.KitapHareketID).ValueGeneratedOnAdd();
            builder.Property(a => a.Ad).HasColumnType("VARCHAR(50)");
            builder.Property(a => a.Soyad).HasColumnType("VARCHAR(50)");
            builder.Property(a => a.TCNo).HasColumnType("BIGINT");
            builder.Property(a => a.EPosta).HasColumnType("VARCHAR(50)");
            builder.Property(a => a.Telefon).HasColumnType("VARCHAR(12)");
            builder.Property(a => a.BaslangicTarihi).HasColumnType("DATETIME");
            builder.Property(a => a.BitisTarihi).HasColumnType("DATETIME");
            builder.Property(a => a.TeslimTarihi).HasColumnType("DATETIME");
            builder.Property(a => a.GunlukCezaTutari).HasColumnType("DECIMAL(18,2)").HasDefaultValue("5");
            builder.Property(a => a.ToplamCezaTutari).HasColumnType("DECIMAL(18,2)");
            builder.Property(a => a.Durum).HasColumnType("TINYINT");

            builder.HasOne<tblKitaplar>(a => a.KEY_KitapID_FK).WithMany(a => a.LIST_tblKitapHareketler_KitapID_FK).HasForeignKey(a => a.KitapID_FK).HasConstraintName("FK_tblKitapHareketler_KitapID_FK");
            builder.ToTable(nameof(tblKitapHareketler));
        }
    }
}
