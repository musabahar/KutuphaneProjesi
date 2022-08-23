using Kutuphane.Data.Entities;
using System.Collections.Generic;
namespace Kutuphane.Web.Models
{
    public class tblKitaplarModel
    {
        public class fnListele
        {
            public IEnumerable<tblKitaplar> liste { get; set; }
            public int toplam_kayit_sayisi { get; set; }
        }
        public class fnGetir
        {
            public tblKitaplar entity { get; set; }
            public IEnumerable<tblKitapHareketler> hareketler { get; set; }
        }
    }
}
