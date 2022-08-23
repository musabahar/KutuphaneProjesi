using System;

namespace Kutuphane.Data.Entities
{
    public class tblKitapHareketler: EntityBase, IEntity
    {
        public int KitapHareketID { get; set; }
        public int? KitapID_FK { get; set; }
        public tblKitaplar KEY_KitapID_FK { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public long? TCNo { get; set; }
        public string EPosta { get; set; }
        public string Telefon { get; set; }
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public DateTime? TeslimTarihi { get; set; }
        public double GunlukCezaTutari { get; set; }
        public double? ToplamCezaTutari { get; set; }

    }
}
