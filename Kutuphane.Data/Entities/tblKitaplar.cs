using System.Collections.Generic;

namespace Kutuphane.Data.Entities
{
    public class tblKitaplar:EntityBase, IEntity
    {
        public int KitapID { get; set; }
        public string Baslik { get; set; }
        public string ISBN { get; set; }
        public int YayinYili { get; set; }
        public decimal? KapakFiyati { get; set; }
        public ICollection<tblKitapHareketler> LIST_tblKitapHareketler_KitapID_FK { get; set; }
    }
}
