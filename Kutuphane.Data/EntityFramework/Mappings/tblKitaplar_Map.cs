using Kutuphane.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Kutuphane.Data.EntityFramework.Mappings
{
    public class tblKitaplar_Map : IEntityTypeConfiguration<tblKitaplar>
    {
        public void Configure(EntityTypeBuilder<tblKitaplar> builder)
        {
            builder.HasKey(a => a.KitapID);
            builder.Property(a => a.KitapID).ValueGeneratedOnAdd();
            builder.Property(a => a.Baslik).HasColumnType("VARCHAR(255)");
            builder.Property(a => a.ISBN).HasColumnType("VARCHAR(50)");
            builder.Property(a => a.YayinYili).HasColumnType("INT");
            builder.Property(a => a.KapakFiyati).HasColumnType("DECIMAL(18,2)");
            builder.Property(a => a.Durum).HasColumnType("TINYINT");
            builder.ToTable(nameof(tblKitaplar));
        }
    }
}
