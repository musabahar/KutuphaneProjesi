using Kutuphane.Data.Entities;
using System.Collections.Generic;
namespace Kutuphane.Web.Models
{
    public class tblKitapHareketlerModel
    {
        public class fnListele
        {
            public IEnumerable<tblKitapHareketler> liste { get; set; }
            public int toplam_kayit_sayisi { get; set; }
        }
        public class fnGetir
        {
            public tblKitapHareketler entity { get; set; }
        }
    }
}
