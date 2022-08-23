using Kutuphane.Data.EntityFramework;
using Kutuphane.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kutuphane.Web
{
    public class Fonksiyonlar
    {
        public string fnKitapBul(int? intKitapID)
        {
            string strSonuc = string.Empty;
            if (intKitapID != null)
            {
                KutuphaneDBContext context = new KutuphaneDBContext();
                UnitOfWork baglanti = new UnitOfWork(context);

                var sorgu = baglanti.ItblKitaplar.fnGetir(a => a.KitapID == intKitapID);
                if (sorgu != null)
                    strSonuc = sorgu.Baslik;
            }

            return strSonuc;
        }
        public string fnSonHareket(int intKitapID)
        {
            string strSonuc = string.Empty;
            KutuphaneDBContext context = new KutuphaneDBContext();
            UnitOfWork baglanti = new UnitOfWork(context);

            var sorgu = baglanti.ItblKitapHareketler.fnGetir(a => a.KitapID_FK == intKitapID && a.Durum == 0);
            if (sorgu != null)
                strSonuc = sorgu.Ad + " " + sorgu.Soyad + " adlı kişide şuan";

            return strSonuc;
        }

        public int intGosterim_Adeti = 10;

        public DateTime fnCalismaGununeGoreEkle(DateTime dt, int nDays)
        {
            int weeks = nDays / 5;
            nDays %= 5;
            while (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                dt = dt.AddDays(1);

            while (nDays-- > 0)
            {
                dt = dt.AddDays(1);
                if (dt.DayOfWeek == DayOfWeek.Saturday)
                    dt = dt.AddDays(2);
            }
            return dt.AddDays(weeks * 7);
        }
        public string fnAktif_Pasif(object objVeri)
        {
            string strDonen_Deger = "";
            if (objVeri != null)
            {
                string strVeri = objVeri.ToString().ToLower();
                if (strVeri == "0" || strVeri == "false")
                    strDonen_Deger = "<span class='label bg-danger'><i class='icon-ban-circle'></i> Aktif Değil</span>";
                else if (strVeri == "1" || strVeri == "true")
                    strDonen_Deger = "<span class='label bg-success'><i class='icon-ok-sign'></i> Aktif</span>";
            }
            return strDonen_Deger;
        }
        public string fnTeslim_Durumu(object objVeri)
        {
            string strDonen_Deger = "";
            if (objVeri != null)
            {
                string strVeri = objVeri.ToString().ToLower();
                if (strVeri == "0" || strVeri == "false")
                    strDonen_Deger = "<span class='label bg-danger'><i class='icon-ban-circle'></i> Teslim Edilmedi</span>";
                else if (strVeri == "1" || strVeri == "true")
                    strDonen_Deger = "<span class='label bg-success'><i class='icon-ok-sign'></i> Teslim Edildi</span>";
            }
            return strDonen_Deger;
        }
        public bool fnE_Posta_Gecerli_Mi(string parVeri)
        {
            bool blnDonen_Deger = false;
            if (string.IsNullOrEmpty(parVeri) == true)
                blnDonen_Deger = false;
            else
            {
                Regex desen = new Regex(@"^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$");
                blnDonen_Deger = desen.IsMatch(parVeri);
            }
            return blnDonen_Deger;
        }
        public bool fnTelefon_Gecerli_Mi(string parVeri)
        {
            bool blnDonen_Deger = false;
            if (string.IsNullOrEmpty(parVeri) == true)
                blnDonen_Deger = false;
            else
            {
                Regex desen = new Regex(@"(\d{3})[\-](\d{3})[\s](\d{4})?$");
                blnDonen_Deger = desen.IsMatch(parVeri);
            }
            return blnDonen_Deger;
        }
        public bool fnSayisal_Mi(object parVeri)
        {
            bool blnSonuc = false;
            if (parVeri != null)
            {
                string strVeri = parVeri.ToString();
                if (!string.IsNullOrEmpty(strVeri))
                {
                    Regex desen = new Regex("^[0-9]*$");
                    blnSonuc = desen.IsMatch(strVeri);
                }
            }
            return blnSonuc;
        }
        public bool fnTc_No_Gecerli_Mi(string strVeri)
        {
            bool blnDonen_Deger = false;
            if (strVeri.Length == 11)
            {
                long ATCNO, BTCNO, TcNo;
                long C1, C2, C3, C4, C5, C6, C7, C8, C9, Q1, Q2;

                TcNo = Int64.Parse(strVeri);

                ATCNO = TcNo / 100;
                BTCNO = TcNo / 100;

                C1 = ATCNO % 10; ATCNO = ATCNO / 10;
                C2 = ATCNO % 10; ATCNO = ATCNO / 10;
                C3 = ATCNO % 10; ATCNO = ATCNO / 10;
                C4 = ATCNO % 10; ATCNO = ATCNO / 10;
                C5 = ATCNO % 10; ATCNO = ATCNO / 10;
                C6 = ATCNO % 10; ATCNO = ATCNO / 10;
                C7 = ATCNO % 10; ATCNO = ATCNO / 10;
                C8 = ATCNO % 10; ATCNO = ATCNO / 10;
                C9 = ATCNO % 10; ATCNO = ATCNO / 10;
                Q1 = ((10 - ((((C1 + C3 + C5 + C7 + C9) * 3) + (C2 + C4 + C6 + C8)) % 10)) % 10);
                Q2 = ((10 - (((((C2 + C4 + C6 + C8) + Q1) * 3) + (C1 + C3 + C5 + C7 + C9)) % 10)) % 10);

                blnDonen_Deger = ((BTCNO * 100) + (Q1 * 10) + Q2 == TcNo);
            }
            return blnDonen_Deger;
        }
        public string fnTarih(object objTarih, bool blnSaat)
        {
            string strSonuc = string.Empty;
            try
            {
                if (objTarih != null)
                {
                    string parTarih = objTarih.ToString();
                    string[] arrAylar = { "", "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" };
                    string[] arrGunler = { "Pazar", "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi" };
                    DateTime dtmTarih = DateTime.Parse(parTarih);
                    string strTarih = dtmTarih.ToString("yyyy-MM-dd HH:mm");
                    int intYil = dtmTarih.Year;
                    string strYil = intYil.ToString();
                    int intAy = dtmTarih.Month;
                    string strAy = arrAylar[intAy];
                    int intGun = dtmTarih.Day;
                    string strGun = intGun.ToString();
                    string strZaman = dtmTarih.ToShortTimeString();
                    int intHaftanin_Gunu = (int)dtmTarih.Date.DayOfWeek;
                    string strGun_Adi = arrGunler[intHaftanin_Gunu];
                    strSonuc = strGun + " " + strAy + " " + strYil + " " + strGun_Adi;
                    if (blnSaat == true)
                        strSonuc = strSonuc + ", " + strZaman;
                }
            }
            catch
            {

            }
            return strSonuc;
        }
        public bool fnTarih_Mi(object objVeri)
        {
            bool blnSonuc = false;
            if (objVeri != null)
            {
                if (!string.IsNullOrEmpty(objVeri.ToString()))
                {
                    Regex desen = new Regex(@"(\d{4})[\-](0?[1-9]|1[012])[\-](0?[1-9]|[12][0-9]|3[01])?$");
                    blnSonuc = desen.IsMatch(objVeri.ToString());
                }
            }
            return blnSonuc;
        }
        public string fnSayfalama(string parAdres_Satiri, long parSayfa, long? parToplam_Icerik, int parGosterim_Adeti)
        {
            long lngSayfa_Alt_Siniri;
            long lngSayfa_Ust_Siniri;
            long lngSol_Grup_Sayisi;
            long lngSag_Alt_Sinir;

            if (parGosterim_Adeti > 0)
                intGosterim_Adeti = parGosterim_Adeti;

            float fltSayfa_Sayisi = (float)parToplam_Icerik / intGosterim_Adeti;
            long lngSayfa_Sayisi = (int)parToplam_Icerik / intGosterim_Adeti;
            string strSayfa_Sayisi = fltSayfa_Sayisi.ToString();

            if (strSayfa_Sayisi.IndexOf(",", 0) > -1 || strSayfa_Sayisi.IndexOf(".", 0) > -1)
                lngSayfa_Sayisi += 1;

            string strDonen_Deger = "<div class=''>";
            strDonen_Deger += "<ul class='pagination pagination-sm'>";

            if (parSayfa <= lngSayfa_Sayisi)
            {
                int intSayfa_Araligi = 2;

                lngSayfa_Alt_Siniri = parSayfa - intSayfa_Araligi;
                lngSayfa_Ust_Siniri = parSayfa + intSayfa_Araligi;
                lngSol_Grup_Sayisi = 1 + intSayfa_Araligi;
                lngSag_Alt_Sinir = lngSayfa_Sayisi - intSayfa_Araligi;

                //Adım 1
                if (lngSayfa_Ust_Siniri > lngSayfa_Sayisi)
                {
                    lngSayfa_Alt_Siniri = lngSayfa_Sayisi - (2 * intSayfa_Araligi);
                    lngSayfa_Ust_Siniri = lngSayfa_Sayisi;
                }

                //Adım 2
                if (lngSayfa_Alt_Siniri <= 0)
                {
                    lngSayfa_Alt_Siniri = 1;
                    lngSayfa_Ust_Siniri = lngSayfa_Alt_Siniri + (2 * intSayfa_Araligi);
                    if (lngSayfa_Ust_Siniri >= lngSayfa_Sayisi)
                        lngSayfa_Ust_Siniri = lngSayfa_Sayisi;
                }

                //Adım 3
                if (lngSol_Grup_Sayisi >= lngSayfa_Alt_Siniri)
                    lngSol_Grup_Sayisi = lngSayfa_Alt_Siniri - 1;

                //Adım 4
                if (lngSag_Alt_Sinir <= lngSayfa_Ust_Siniri)
                    lngSag_Alt_Sinir = lngSayfa_Ust_Siniri + 1;

                //Adım 5
                if (parSayfa > 1)
                {
                    strDonen_Deger += "<li><a href='" + parAdres_Satiri + "&sayfa=1' title='İlk Sayfa'><i class='fa fa-angle-double-left'></i></a></li>";
                    long lngOnceki_Sayfa = parSayfa - 1;
                    strDonen_Deger += "<li><a href='" + parAdres_Satiri + "&sayfa=" + lngOnceki_Sayfa + "' title='Önceki Sayfa'><i class='fa fa-angle-left'></i></a></li>";
                }

                //Adım 6
                for (long i = 1; i < lngSol_Grup_Sayisi + 1; i++)
                {
                    if (i == parSayfa)
                        strDonen_Deger += "<li class='active'><a title='" + i + ". Sayfa'>" + i + "</a></li>";
                    else
                        strDonen_Deger += "<li><a title='" + i + ". Sayfa' href='" + parAdres_Satiri + "&sayfa=" + i + "'>" + i + "</a></li>";
                }

                //Adım 7
                if (lngSayfa_Alt_Siniri - lngSol_Grup_Sayisi > 1)
                    strDonen_Deger += "<li><a href='javascript:void();'>. . .</a></li>";

                //Adım 8
                for (long i = lngSayfa_Alt_Siniri; i < lngSayfa_Ust_Siniri + 1; i++)
                {
                    if (i == parSayfa)
                        strDonen_Deger += "<li class='active'><a title='" + i + ". Sayfa'>" + i + "</a></li>";
                    else
                        strDonen_Deger += "<li><a title='" + i + ". Sayfa' href='" + parAdres_Satiri + "&sayfa=" + i + "'>" + i + "</a></li>";
                }

                //Adım 9
                if (lngSag_Alt_Sinir - lngSayfa_Ust_Siniri > 1)
                    strDonen_Deger += "<li><a>. . .</a></li>";

                //Adım 10
                for (long i = lngSag_Alt_Sinir; i < lngSayfa_Sayisi + 1; i++)
                {
                    if (i == parSayfa)
                        strDonen_Deger += "<li class='active'><a title='" + i + ". Sayfa'>" + i + "</a></li>";
                    else
                        strDonen_Deger += "<li><a title='" + i + ". Sayfa' href='" + parAdres_Satiri + "&sayfa=" + i + "'>" + i + "</a></li>";
                }

                //Adım 11
                if (parSayfa != lngSayfa_Sayisi)
                {
                    long lngSonraki_Sayfa = parSayfa + 1;
                    strDonen_Deger += "<li><a href='" + parAdres_Satiri + "&sayfa=" + lngSonraki_Sayfa + "' title='Sonraki Sayfa'><i class='fa fa-angle-right'></i></a></li>";
                    strDonen_Deger += "<li><a href='" + parAdres_Satiri + "&sayfa=" + lngSayfa_Sayisi + "' title='En Son Sayfa'><i class='fa fa-angle-double-right'></i></a></li>";
                }
            }
            strDonen_Deger += "</ul>";
            strDonen_Deger += "</div>";
            return strDonen_Deger;
        }
    }
}
