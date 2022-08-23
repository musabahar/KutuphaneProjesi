using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Kutuphane.Web;
using Kutuphane.Data.EntityFramework;
using Kutuphane.Data.UnitOfWork;
using Kutuphane.Data.Entities;
using Kutuphane.Web.Models;

namespace Sistem.Web.Controllers
{
    public class kitaplarController : Controller
    {
        Fonksiyonlar fn = new Fonksiyonlar();

        [Route("kitaplar")]
        public IActionResult kitaplar(string sayfa)
        {
            int intToplam_Kayit = 0;
            int intGosterim_Adeti = fn.intGosterim_Adeti;
            int intSayfa = 1;
            if (fn.fnSayisal_Mi(sayfa))
                intSayfa = Convert.ToInt32(sayfa);
            int intBaslangic_Degeri = (intGosterim_Adeti * intSayfa) - intGosterim_Adeti;
            KutuphaneDBContext context = new KutuphaneDBContext();
            UnitOfWork baglanti = new UnitOfWork(context);

            IEnumerable<tblKitaplar> liste = baglanti.ItblKitaplar.fnListele(intBaslangic_Degeri, intGosterim_Adeti, out intToplam_Kayit, null, null);

            tblKitaplarModel.fnListele model = new tblKitaplarModel.fnListele();
            model.liste = liste;
            model.toplam_kayit_sayisi = intToplam_Kayit;

            return View(model);
        }

        [Route("kitap-detay")]
        public IActionResult kitap_detay(string kitap_ID)
        {
            tblKitaplarModel.fnGetir model = new tblKitaplarModel.fnGetir();
            model.entity = null;

            if (fn.fnSayisal_Mi(kitap_ID))
            {
                KutuphaneDBContext context = new KutuphaneDBContext();
                UnitOfWork baglanti = new UnitOfWork(context);

                int intKitap_ID = Convert.ToInt32(kitap_ID);

                int intToplam_Kayit = 0;
                IEnumerable<tblKitapHareketler> listeHareketler = baglanti.ItblKitapHareketler.fnListele(null, null, out intToplam_Kayit, (a => a.KitapID_FK == intKitap_ID), null);

                tblKitaplar sonuc = baglanti.ItblKitaplar.fnGetir(a => a.KitapID == intKitap_ID);
                if (sonuc != null)
                {
                    model.entity = sonuc;
                    model.hareketler = listeHareketler;
                } 
            }
            return View(model);
        }
    }
}
