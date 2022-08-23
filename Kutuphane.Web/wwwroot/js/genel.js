var desen_string = /^[A-Za-z0-9\-]+$/;
var desen_sifre = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{3,20}$/;
var desen_e_posta = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,10})$/;
var desen_para = /^[0-9]*(?:\.[0-9]*)?$/;
var desen_sayi = /^[0-9]+$/;
var desen_sayi_11 = /^[0-9]{11}$/;
var desen_sayi_10 = /^[0-9]{10}$/;
var desen_telefon = /^(\d{3})[\-](\d{3})[\s](\d{4})$/;
var desen_tarih = /^(\d{4})[\-](0?[1-9]|1[012])[\-](0?[1-9]|[12][0-9]|3[01])$/;
var desen_saat = /^([0-1][0-9]|[2][0-3]):([0-5][0-9])?$/;
var desen_guid = /^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/;

jQuery("body").on("click", ".form-kitap-detay .buton", function () {
    var strAd = jQuery(".jq-ad").val();
    var strSoyad = jQuery(".jq-soyad").val();
    var strTcNo = jQuery(".jq-tcno").val();
    var strE_Posta = jQuery(".jq-eposta").val();
    var strTelefon = jQuery(".jq-telefon").val();
    var strBaslangicTarihi = jQuery(".jq-baslangic-tarihi").val();
    var strBitisTarihi = jQuery(".jq-bitis-tarihi").val();
    var strSonuc = "";
    if (strAd === '')
        strSonuc += "- Lütfen <strong>Ad</strong> giriniz!<br />";
    if (strSoyad === '')
        strSonuc += "- Lütfen <strong>Soyad</strong> giriniz!<br />";
    if (!fnTC_No_Gecerli_Mi(strTcNo))
        strSonuc += "- Lütfen geçerli bir <strong>T.C. No</strong> giriniz!<br />";
    if (!desen_e_posta.test(strE_Posta))
        strSonuc += "- Geçerli bir <strong>E-Posta</strong> adresi giriniz!<br />";
    if (!desen_telefon.test(strTelefon))
        strSonuc += "- Geçerli bir <strong>Telefon</strong> giriniz!<br />";
    if (!desen_tarih.test(strBaslangicTarihi))
        strSonuc += "- Geçerli bir <strong>Başlangıç Tarihi</strong> giriniz!<br />";
    if (!desen_tarih.test(strBitisTarihi))
        strSonuc += "- Geçerli bir <strong>Bitiş Tarihi</strong> giriniz!<br />";
    if (strSonuc !== "") {
        fnModal_Popup4('', 'Uyarı', strSonuc, 'warning');
        return false;
    }
    return true;
});

jQuery("body").on("click", ".form-hareket-detay .buton", function () {
    var strTeslimTarihi = jQuery(".jq-teslim-tarihi").val();
    var strSonuc = "";
    if (!desen_tarih.test(strTeslimTarihi))
        strSonuc += "- Geçerli bir <strong>Teslim Tarihi</strong> giriniz!<br />";
    if (strSonuc !== "") {
        fnModal_Popup4('', 'Uyarı', strSonuc, 'warning');
        return false;
    }
    return true;
});

/* Datepicker (BAŞLANGIÇ) */
fnDatepicker();
function fnDatepicker() {
    jQuery('.jq-datepicker').datetimepicker({
        timepicker: false,
        format: 'Y-m-d',
        lang: 'tr',
        allowBlank: true
    });
}
/* Datepicker (BİTİŞ) */
/* Mask (BAŞLANGIÇ) */
fnMask();
function fnMask() {
    jQuery(".jq-mask-telefon").mask("999-999 9999");
}
/* Mask (BİTİŞ) */
/* Modal Popup 4 (BAŞLANGIÇ) */
function fnModal_Popup4(parTip, parBaslik, parMesaj, parAdres) {
    if (parTip === '' || parTip === 'varsayilan') {
        var myModal = new jBox('Modal', {
            title: parBaslik,
            content: parMesaj,
            theme: 'ModalBorder',
            overlay: true,
            closeOnClick: false,
            draggable: 'title'
        });
        myModal.open();
    }
    else if (parTip === 'ajax') {
        var myModal = new jBox('Modal', {
            title: parBaslik,
            theme: 'ModalBorder',
            overlay: true,
            closeOnClick: false,
            draggable: 'title',
            ajax: {
                url: parAdres,
                reload: true
            }
        });
        myModal.open();
    }
    if (parTip === 'kapat') {
        var myModal = new jBox({});
        myModal.close();
    }
}
/* Modal Popup 4 (BİTİŞ) */

var desen_tc = /^[0-9]{11}$/;
function fnTC_No_Gecerli_Mi(parTC) {
    var blnSonuc = true;
    var strTC = String(parTC);
    if (desen_tc.test(strTC) == false) {
        blnSonuc = false;
    }
    int1 = parseInt(strTC.substr(0, 1));
    int2 = parseInt(strTC.substr(1, 1));
    int3 = parseInt(strTC.substr(2, 1));
    int4 = parseInt(strTC.substr(3, 1));
    int5 = parseInt(strTC.substr(4, 1));
    int6 = parseInt(strTC.substr(5, 1));
    int7 = parseInt(strTC.substr(6, 1));
    int8 = parseInt(strTC.substr(7, 1));
    int9 = parseInt(strTC.substr(8, 1));
    int10 = parseInt(strTC.substr(9, 1));
    int11 = parseInt(strTC.substr(10, 1));

    if (int1 === 0) {
        blnSonuc = false;
    }
    if ((int1 + int3 + int5 + int7 + int9 + int2 + int4 + int6 + int8 + int10) % 10 != int11) {
        blnSonuc = false;
    }
    if (((int1 + int3 + int5 + int7 + int9) * 7 + (int2 + int4 + int6 + int8) * 9) % 10 != int10) {
        blnSonuc = false;
    }
    if (((int1 + int3 + int5 + int7 + int9) * 8) % 10 != int11) {
        blnSonuc = false;
    }
    return blnSonuc;
}