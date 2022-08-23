using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Kutuphane.Web;
using Kutuphane.Data.EntityFramework;
using Kutuphane.Data.UnitOfWork;
using Kutuphane.Data.Entities;
using Kutuphane.Web.Models;
using Microsoft.AspNetCore.Http;

namespace Kutuphane.Web.Controllers
{
    public class hareketlerController : Controller
    {
        Fonksiyonlar fn = new Fonksiyonlar();

        [Route("hareketler")]
        public IActionResult hareketler(string kitap_ID, string sayfa)
        {
            int intToplam_Kayit = 0;
            int intGosterim_Adeti = fn.intGosterim_Adeti;
            int intSayfa = 1;
            if (fn.fnSayisal_Mi(sayfa))
                intSayfa = Convert.ToInt32(sayfa);
            int intBaslangic_Degeri = (intGosterim_Adeti * intSayfa) - intGosterim_Adeti;
            KutuphaneDBContext context = new KutuphaneDBContext();
            UnitOfWork baglanti = new UnitOfWork(context);

            IEnumerable<tblKitapHareketler> liste = null;
            if (fn.fnSayisal_Mi(kitap_ID))
            {
                int intKitapID = Convert.ToInt32(kitap_ID);
                liste = baglanti.ItblKitapHareketler.fnListele(intBaslangic_Degeri, intGosterim_Adeti, out intToplam_Kayit, (a => a.KitapID_FK == intKitapID), null);
            }

            tblKitapHareketlerModel.fnListele model = new tblKitapHareketlerModel.fnListele();
            model.liste = liste;
            model.toplam_kayit_sayisi = intToplam_Kayit;

            return View(model);
        }
        [Route("hareket-kaydet")]
        public IActionResult hareket_kaydet(IFormCollection fcDegerler)
        {
            string strKitap_ID = fcDegerler["kitap_ID"];
            string strAd = fcDegerler["ad"];
            string strSoyad = fcDegerler["soyad"];
            string strTCNo = fcDegerler["tcno"];
            string strEPosta = fcDegerler["eposta"];
            string strTelefon = fcDegerler["telefon"];
            string strBaslangicTarihi = fcDegerler["baslangic_tarihi"];
            string strBitisTarihi = fcDegerler["bitis_tarihi"];

            string strSonuc = string.Empty;
            string strYonlendir = "/kitap-detay?kitap_ID=" + strKitap_ID;

            if (!fn.fnSayisal_Mi(strKitap_ID))
                strSonuc = "&tip=kirmizi&bilgi=Lütfen kitap seçiniz!";
            if (string.IsNullOrEmpty(strAd))
                strSonuc = "&tip=kirmizi&bilgi=Lütfen ad giriniz!";
            if (string.IsNullOrEmpty(strSoyad))
                strSonuc = "&tip=kirmizi&bilgi=Lütfen soyad giriniz!";
            if (!fn.fnTc_No_Gecerli_Mi(strTCNo))
                strSonuc = "&tip=kirmizi&bilgi=Lütfen geçerli bir T.C. No giriniz!";
            if (!fn.fnE_Posta_Gecerli_Mi(strEPosta))
                strSonuc = "&tip=kirmizi&bilgi=Lütfen geçerli bir eposta giriniz!";
            if (!fn.fnTelefon_Gecerli_Mi(strTelefon))
                strSonuc = "&tip=kirmizi&bilgi=Lütfen geçerli bir telefon giriniz!";
            if (!fn.fnTarih_Mi(strBaslangicTarihi))
                strSonuc = "&tip=kirmizi&bilgi=Lütfen ödünç başlangıç tarihi belirtiniz!";
            if (!fn.fnTarih_Mi(strBitisTarihi))
                strSonuc = "&tip=kirmizi&bilgi=Lütfen ödünç bitiş tarihi belirtiniz!";

            if (string.IsNullOrEmpty(strSonuc))
            {
                HttpContext hcContext = HttpContext;
                DateTime dtmBaslangicTarihi = Convert.ToDateTime(strBaslangicTarihi);
                DateTime dtmBitisTarihi = fn.fnCalismaGununeGoreEkle(dtmBaslangicTarihi, 15);

                int intKitapID = Convert.ToInt32(strKitap_ID);
                tblKitapHareketler entity = new tblKitapHareketler()
                {
                    Ad = strAd,
                    Soyad = strSoyad,
                    BaslangicTarihi = dtmBaslangicTarihi,
                    BitisTarihi = dtmBitisTarihi,
                    Durum = 0,
                    EPosta = strEPosta,
                    Telefon = strTelefon,
                    GunlukCezaTutari = 5,
                    KitapHareketID = 0,
                    KitapID_FK = intKitapID,
                    TCNo = Convert.ToInt64(strTCNo),
                    TeslimTarihi = null,
                    ToplamCezaTutari = null,
                };

                KutuphaneDBContext context = new KutuphaneDBContext();
                UnitOfWork baglanti = new UnitOfWork(context);

                var sorgu = baglanti.ItblKitapHareketler.fnGetir(a => a.KitapID_FK == intKitapID && a.BaslangicTarihi <= DateTime.Now && a.BitisTarihi >= DateTime.Now && a.Durum == 0);
                if (sorgu != null)
                {
                    strSonuc = "&tip=kirmizi&bilgi=Bu kitap şuanda " + sorgu.Ad + " " + sorgu.Soyad + " adlı kişide olduğu için ödünç işlemi yapılamıyor.";
                }
                else
                {
                    baglanti.ItblKitapHareketler.fnEkle(entity);
                    int intSonuc = baglanti.fnKaydet();
                    if (intSonuc == 1)
                    {
                        strYonlendir = "/kitaplar?kitap_ID=" + strKitap_ID;
                        strSonuc = "&tip=yesil&bilgi=Ödünç işlemi başarılı bir şekilde yapılmıştır!";
                    }
                } 
            }
            strYonlendir += strSonuc;

            return Redirect(strYonlendir);
        }
        [Route("hareket-detay")]
        public IActionResult hareket_detay(string hareket_ID)
        {
            tblKitapHareketlerModel.fnGetir model = new tblKitapHareketlerModel.fnGetir();
            model.entity = null;

            if (fn.fnSayisal_Mi(hareket_ID))
            {
                KutuphaneDBContext context = new KutuphaneDBContext();
                UnitOfWork baglanti = new UnitOfWork(context);

                int intHareket_ID = Convert.ToInt32(hareket_ID);

                tblKitapHareketler sonuc = baglanti.ItblKitapHareketler.fnGetir(a => a.KitapHareketID == intHareket_ID);
                if (sonuc != null)
                {
                    sonuc.ToplamCezaTutari = 0;
                    if (sonuc.BitisTarihi != null && DateTime.Now > sonuc.BitisTarihi)
                    {
                        TimeSpan tsZaman_Farki = DateTime.Now - (DateTime)sonuc.BitisTarihi;
                        double dblGun = tsZaman_Farki.TotalDays;
                        sonuc.ToplamCezaTutari = Math.Floor(dblGun) * 5;
                    }
                    model.entity = sonuc;
                }
            }
            return View(model);
        }
        [Route("hareket-guncelle")]
        public IActionResult hareket_guncelle(IFormCollection fcDegerler)
        {
            string strHareket_ID = fcDegerler["hareket_ID"];
            string strTeslim_Tarihi = fcDegerler["teslim_tarihi"];

            string strSonuc = string.Empty;
            string strYonlendir = "/hareket-detay?hareket_ID=" + strHareket_ID;

            if (!fn.fnSayisal_Mi(strHareket_ID))
                strSonuc = "&tip=kirmizi&bilgi=Lütfen hareket seçiniz!";
            if (!fn.fnTarih_Mi(strTeslim_Tarihi))
                strSonuc = "&tip=kirmizi&bilgi=Lütfen teslim tarihi belirtiniz!";

            if (string.IsNullOrEmpty(strSonuc))
            {
                HttpContext hcContext = HttpContext;
                DateTime dtmTeslimTarihi = Convert.ToDateTime(strTeslim_Tarihi);
                TimeSpan tsZaman_Farki = dtmTeslimTarihi - DateTime.Now;
                double dblGun = tsZaman_Farki.TotalDays;
                dblGun = Math.Floor(dblGun);

                double? dblToplamCeza = null;
                if (dblGun > 0)
                    dblToplamCeza = dblGun * 5;

                int intHareketID = Convert.ToInt32(strHareket_ID);

                KutuphaneDBContext context = new KutuphaneDBContext();
                UnitOfWork baglanti = new UnitOfWork(context);

                var sorgu = baglanti.ItblKitapHareketler.fnGetir(a => a.KitapHareketID == intHareketID);
                if (sorgu == null)
                    strSonuc = "&tip=kirmizi&bilgi=Kayıtlarda böyle bir hareket bulunamadığı için işlem yapamazsınız";
                else
                {
                    if (sorgu.Durum == 1)
                    {
                        strSonuc = "&tip=kirmizi&bilgi=Bu kitap teslim alındığı için işlem yapamazsınız";
                    }
                    else
                    {
                        sorgu.TeslimTarihi = dtmTeslimTarihi;
                        sorgu.ToplamCezaTutari = dblToplamCeza;
                        sorgu.Durum = 1;

                        baglanti.ItblKitapHareketler.fnGuncelle(sorgu);
                        int intSonuc = baglanti.fnKaydet();
                        if (intSonuc == 1)
                        {
                            strYonlendir = "/kitaplar";
                            strSonuc = "?tip=yesil&bilgi=Ödünç teslim alma işlemi başarılı bir şekilde yapılmıştır!";
                        }
                    }
                }
            }
            strYonlendir += strSonuc;

            return Redirect(strYonlendir);
        }
    }
}
