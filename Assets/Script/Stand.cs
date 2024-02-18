using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    // d��ar�dan al�nacak olan ve standa laz�m olan b�t�n objeleri tan�mlamal�y�z

    public GameObject HareketPozisyonu;
  
    // her �ember i�in spesifik bir nokta veya pozisyon g�ndermem laz�m ki �ember gidece�i standta ka��nc� sokete oturabilirce�ini
    // bilsin 

    public GameObject[] Soketler;
    public int BosOlanSoket; // standa yeni gelen �emberin ka��nc� sokete oturaca��n� belirlemek i�in olu�turuldu.

    public List<GameObject> _Cemberler = new(); // _Cemberler ad�nda yeni liste olu�tu.

    [SerializeField] GameManager _GameManager; // gamemanager script dosyas�n�da buraya dahil ettik.
    
    int CemberTamamlanmaSayisi;

    public GameObject EnUsttekiCemberiVer() // en �stteki ��kmaya musait olan objeyi almak istiyorum
    {
        // burada herhangi bir stand�n en sonuncu eleman�n� _Cemberler listinden alabiliriz. 
        // bu list in en sonuncu eleman� en �stteki eleman�d�r.

        return _Cemberler[^1]; //�emberlerin en sonuncu eleman�n� al demektir [_Cember.cound - 1] = [^1] ayn� i�lemler 
    } 
    public GameObject MusaitSoketiVer()
    {
        return Soketler[BosOlanSoket]; // diyelimki stand ta 2 tane �ember var o zaman index 0 ve 1 dolu demektir. yani ben musait soketin pozisyonunu alabilmek i�in soketler listemin i�indeki musait soketin pozisyonunu alm�� oluyoruz. 
    }

    public void SoketDegistirme�slemleri(GameObject SilinecekObje) // bir �ember ba�ka bir standa gidicekse i�indekiler olacak. benim soketimden ay�r�cak oldu�um obje �emberler listesinden silicez.
    {
        _Cemberler.Remove(SilinecekObje); // silme i�elim
        
        if (_Cemberler.Count != 0) // �ember listesinin e�erki eleman say�s� 0 a e�it de�ilse yani i�inde eleman varsa.
        {
            BosOlanSoket--; // azalt�m ayapmal�y�z ��nk� liste i�erisinden obje sildik. sildiysek bo� olan soket indeksini bir d���rmem laz�m ki cemberler listesindeki ka��nc� indexin m�sait olaca��n� bileyim. �emberler listesinde hi�bir eleman yoksa yani stand tamamen bo� olursa bu azaltma i�lemini bu if i�inde yapmal�y�m.

            _Cemberler[^1].GetComponent<Cember>().HareketEdebilirMi = true; // burada gelen objeyi sildik. indexi du�urduk. d���rd�kten sonra listemin en son eleman�n�n cember scriptine eri�erek hareketedebilirmi de�erini true yapt�m. 
        }
        else // e�erki benim cemberler listemin i�erisi tamamen bo� ise bo� olan soketi 0 yapmal�y�z 
        {
            BosOlanSoket = 0; // listeye yeni eklenecek olan objenin indexi 0 olaca�� i�in direk onu en ba�a ekle demektir
        }
    }
    public void CemberleriKontrolEt() // burada yap�lacak olan i�lem stand�m�n i�erisindeki �emberler listesinin i�erisindeki b�t�n �emberlerin renklerini ayn� olup olmad���n� kontrol edece�iz. bu fonksiyonu �a��rmam�z gereken yer gameobject class � bunu her �ember stand de�i�tirdi�i zaman yap�yor olma�z laz�m. 
    {
        if (_Cemberler.Count == 4) // stand�n tamamlan�p tamamlanmad���n� anlamam�z i�in cember listesinin 4 eleman�da dolu oldu�unu kontrol etmemiz gerekir.
        {

            string Renk = _Cemberler[0].GetComponent<Cember>().Renk; // �emberler listin i�erisindeki en alttaki eleman�n renkine eristik.

            foreach (var item in _Cemberler)
            {
                if (Renk == item.GetComponent<Cember>().Renk)// stand i�erisindeki �emberlerin renklerine ula�t�k ama benim bunu bir�ey ile kar��la�t�rmam gerekiyor yani renkler hepsi ayn� renk mi anlayabilmem i�in bir tane de�eri benim referans alamam laz�m bunun i�inde �emberimin listemin i�erisindeki en alttaki �emberi alabilirim. 
                {
                    CemberTamamlanmaSayisi++; // renk ile bu �ember i�erisindeki s�ras� gelen o obje yani �emberin rengi birbirine e�it oldu�u m�ddet�e sen bunu art�r. yani ben burada 4 rakam�na ula��rsam b�t�n �emberler ayn� renkte demek oluyor 
                }
            }
            if (CemberTamamlanmaSayisi==4)
            {
                 _GameManager.StandTamamlandi();
                 TamamlanmisStand�slemleri();

            }
            else
            {
                 CemberTamamlanmaSayisi = 0; // bir sonraki i�lemi yapabilmesi i�in tamamlanma say�s�n� tekrardan 0 lamam�z gerekir 
            }
              
        }
    }
    void TamamlanmisStand�slemleri()
    {
        foreach (var item in _Cemberler) // �ember listin i�erisine tekrar girdik  
        {
            item.GetComponent<Cember>().HareketEdebilirMi = false; // stand tamamland���nda tekrar �ember se�imini kapatt�k. buradaki en �nemli olan unsur �emberin �nce hareketini tamamen kapat�toruz ve rengimizide ayn� �ekilde ayarl�yoruz.

            Color32 color = item.GetComponent<MeshRenderer>().material.GetColor("_Color");
            color.a = 150; //alm�� oldu�umuz rengin alfa kanal�n� 150 yapt�k.
            item.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
            gameObject.tag = "TamamlanmisStand"; // �emberler kitlenid ama platformlar kitlenmedi yani standa t�kland���nda art�k t�klanamas�n istiyorum. tamamland��� zaman bu �ekilde tag�n� de�i�tirebiliriz
            
        }  
    }
}
