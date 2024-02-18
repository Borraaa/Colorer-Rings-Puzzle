using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    // dýþarýdan alýnacak olan ve standa lazým olan bütün objeleri tanýmlamalýyýz

    public GameObject HareketPozisyonu;
  
    // her çember için spesifik bir nokta veya pozisyon göndermem lazým ki çember gideceði standta kaçýncý sokete oturabilirceðini
    // bilsin 

    public GameObject[] Soketler;
    public int BosOlanSoket; // standa yeni gelen çemberin kaçýncý sokete oturacaðýný belirlemek için oluþturuldu.

    public List<GameObject> _Cemberler = new(); // _Cemberler adýnda yeni liste oluþtu.

    [SerializeField] GameManager _GameManager; // gamemanager script dosyasýnýda buraya dahil ettik.
    
    int CemberTamamlanmaSayisi;

    public GameObject EnUsttekiCemberiVer() // en üstteki çýkmaya musait olan objeyi almak istiyorum
    {
        // burada herhangi bir standýn en sonuncu elemanýný _Cemberler listinden alabiliriz. 
        // bu list in en sonuncu elemaný en üstteki elemanýdýr.

        return _Cemberler[^1]; //çemberlerin en sonuncu elemanýný al demektir [_Cember.cound - 1] = [^1] ayný iþlemler 
    } 
    public GameObject MusaitSoketiVer()
    {
        return Soketler[BosOlanSoket]; // diyelimki stand ta 2 tane çember var o zaman index 0 ve 1 dolu demektir. yani ben musait soketin pozisyonunu alabilmek için soketler listemin içindeki musait soketin pozisyonunu almýþ oluyoruz. 
    }

    public void SoketDegistirmeÝslemleri(GameObject SilinecekObje) // bir çember baþka bir standa gidicekse içindekiler olacak. benim soketimden ayýrýcak olduðum obje çemberler listesinden silicez.
    {
        _Cemberler.Remove(SilinecekObje); // silme iþelim
        
        if (_Cemberler.Count != 0) // çember listesinin eðerki eleman sayýsý 0 a eþit deðilse yani içinde eleman varsa.
        {
            BosOlanSoket--; // azaltým ayapmalýyýz çünkü liste içerisinden obje sildik. sildiysek boþ olan soket indeksini bir düþürmem lazým ki cemberler listesindeki kaçýncý indexin müsait olacaðýný bileyim. çemberler listesinde hiçbir eleman yoksa yani stand tamamen boþ olursa bu azaltma iþlemini bu if içinde yapmalýyým.

            _Cemberler[^1].GetComponent<Cember>().HareketEdebilirMi = true; // burada gelen objeyi sildik. indexi duþurduk. düþürdükten sonra listemin en son elemanýnýn cember scriptine eriþerek hareketedebilirmi deðerini true yaptým. 
        }
        else // eðerki benim cemberler listemin içerisi tamamen boþ ise boþ olan soketi 0 yapmalýyýz 
        {
            BosOlanSoket = 0; // listeye yeni eklenecek olan objenin indexi 0 olacaðý için direk onu en baþa ekle demektir
        }
    }
    public void CemberleriKontrolEt() // burada yapýlacak olan iþlem standýmýn içerisindeki çemberler listesinin içerisindeki bütün çemberlerin renklerini ayný olup olmadýðýný kontrol edeceðiz. bu fonksiyonu çaðýrmamýz gereken yer gameobject class ý bunu her çember stand deðiþtirdiði zaman yapýyor olmaýz lazým. 
    {
        if (_Cemberler.Count == 4) // standýn tamamlanýp tamamlanmadýðýný anlamamýz için cember listesinin 4 elemanýda dolu olduðunu kontrol etmemiz gerekir.
        {

            string Renk = _Cemberler[0].GetComponent<Cember>().Renk; // çemberler listin içerisindeki en alttaki elemanýn renkine eristik.

            foreach (var item in _Cemberler)
            {
                if (Renk == item.GetComponent<Cember>().Renk)// stand içerisindeki çemberlerin renklerine ulaþtýk ama benim bunu birþey ile karþýlaþtýrmam gerekiyor yani renkler hepsi ayný renk mi anlayabilmem için bir tane deðeri benim referans alamam lazým bunun içinde çemberimin listemin içerisindeki en alttaki çemberi alabilirim. 
                {
                    CemberTamamlanmaSayisi++; // renk ile bu çember içerisindeki sýrasý gelen o obje yani çemberin rengi birbirine eþit olduðu müddetçe sen bunu artýr. yani ben burada 4 rakamýna ulaþýrsam bütün çemberler ayný renkte demek oluyor 
                }
            }
            if (CemberTamamlanmaSayisi==4)
            {
                 _GameManager.StandTamamlandi();
                 TamamlanmisStandÝslemleri();

            }
            else
            {
                 CemberTamamlanmaSayisi = 0; // bir sonraki iþlemi yapabilmesi için tamamlanma sayýsýný tekrardan 0 lamamýz gerekir 
            }
              
        }
    }
    void TamamlanmisStandÝslemleri()
    {
        foreach (var item in _Cemberler) // çember listin içerisine tekrar girdik  
        {
            item.GetComponent<Cember>().HareketEdebilirMi = false; // stand tamamlandýðýnda tekrar çember seçimini kapattýk. buradaki en önemli olan unsur çemberin önce hareketini tamamen kapatýtoruz ve rengimizide ayný þekilde ayarlýyoruz.

            Color32 color = item.GetComponent<MeshRenderer>().material.GetColor("_Color");
            color.a = 150; //almýþ olduðumuz rengin alfa kanalýný 150 yaptýk.
            item.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
            gameObject.tag = "TamamlanmisStand"; // çemberler kitlenid ama platformlar kitlenmedi yani standa týklandýðýnda artýk týklanamasýn istiyorum. tamamlandýðý zaman bu þekilde tagýný deðiþtirebiliriz
            
        }  
    }
}
