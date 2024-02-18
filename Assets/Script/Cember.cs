using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cember : MonoBehaviour
{
    // not: �ncelikle �emberlerde neler olacak yani i�lem t�r� olarak. �ncelikle �emberlerimi ait olduklar� stand larda 
    // tutuyor olmam gerekiyor. yani �ember hangi standa ait benim bunu tutmama laz�m ki ilgili stand ile ilgili
    // biliyorsun listeye eklenme ve ��karma i�lemleri yapabileyim. 

    public GameObject _AitOlduguStand;
    public GameObject _AitOlduguCemberSoketi; // �emberin terk etmi� oldu�u pozisyona tekrar geri gelme olay� olacak o k�s�mda ilgili 
    // �emberin hangi sokete ait oldu�unu benim tutuyor olmam gerekmekte.
    public bool HareketEdebilirMi;
    public string Renk;
    public GameManager _GameManager; // gamemanager scriptini buraya ba�lad�k.

    GameObject HareketPozisyonu;
    GameObject GidecegiStand;

    bool Secildi, PosDegistir, SoketOtur, SoketeGeriGit; // soketlerimizin hareketleri vard�. se�im hareketi yani se�ti�imizde soket olu�turdu�umuz k�p�n oraya gidicek
        //sonras�nda birde ba�ka bir standa gitme olay� var e�er ba�ka bir standa gidicekse yani di�er stand�n hareket pozisyonuna gidicek ve sonrada sokete oturucuak. bu i�lemleri bool ile kontrol edicez

   
    public void HareketEt(string islem, GameObject Stand = null, GameObject Soket = null, GameObject GidilecekObje = null) 
        // gamemanager dan tetikleyece�imiz ana fonksiyon. (String islem) ile pozisyonmu de�i�ti yada se�ildimi yada soketemi oturdu gibi bu i�lemleri kontrol etmek i�in parametre yaz�yoruz. geri kalan parametreler iste�e ba�l� kullan�laca�� i�in null a e�itledik.
    {
        switch (islem)
        {
            case "Secim": //ben bir �emberi se�ti�imde �emberimin alaca�� tek pozisyon stand�m�n hareket pozisyonu yani k�p�n pos u.
                HareketPozisyonu = GidilecekObje; //objenin hareket edice�i b�l�m� alan� verdik 
                Secildi = true; // true yapmam gerekiyorki update methodunda hareketimizi sa�layabilelim.
                    break;
            case "PozisyonDegis":
                GidecegiStand = Stand; // gidecek olan stand� beni �emberimin ait oldu�u stand�na e�itlemem gerekiyor. ��nk� �emberin mevcut stand� de�i�iyor bunu benim g�ndermem laz�m.
                _AitOlduguCemberSoketi = Soket;
                HareketPozisyonu = GidilecekObje;
                PosDegistir = true;
                break;
           
            case "SoketeGeriGit":
                SoketeGeriGit = true;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Secildi)
        {
            transform.position = Vector3.Lerp(transform.position, HareketPozisyonu.transform.position, 2f); // �emberimize pozisyon verdik.lerp kullanarak yumu�ak ge�i� yapt�k

            if (Vector3.Distance(transform.position, HareketPozisyonu.transform.position) < .10)// yine mesafe �l��yorum yani iki pozisyon aras�ndaki mesafeyi alarak asl�nda benim �emberimi ilgili pozisyona gelip gelmed�ini anlamam i�in. e�erki bu iki pozisyon ras�ndaki mesafe .10 a d��t�yse olay bitti demektir. yani secidi yi false yapki if dursun rat�k
            {
                Secildi = false;
            }
            

        }
        if (PosDegistir)
        {
            transform.position = Vector3.Lerp(transform.position, HareketPozisyonu.transform.position, 2f); // �emberimize pozisyon verdik.lerp kullanarak yumu�ak ge�i� yapt�k

            if (Vector3.Distance(transform.position, HareketPozisyonu.transform.position) < .10)// yine mesafe �l��yorum yani iki pozisyon aras�ndaki mesafeyi alarak asl�nda benim �emberimi ilgili pozisyona gelip gelmed�ini anlamam i�in. e�erki bu iki pozisyon ras�ndaki mesafe .10 a d��t�yse olay bitti demektir. yani secidi yi false yapki if dursun rat�k
            {
                PosDegistir = false;
                SoketOtur = true;
            }
        }
        if (SoketOtur)
        {
            transform.position = Vector3.Lerp(transform.position, _AitOlduguCemberSoketi.transform.position, .04f); // �emberimize pozisyon verdik.lerp kullanarak yumu�ak ge�i� yapt�k

            if (Vector3.Distance(transform.position, _AitOlduguCemberSoketi.transform.position) < .10) // bura art�k �emberimiz yeni oturacak soketin hareketpozisyonuna kadar geldi yani k�p�ne. art�k �emberin hangi sokete oturuca��na karar vermemiz gerekiyor. 
            {

                transform.position = _AitOlduguCemberSoketi.transform.position;   // e�er aralar�ndaki mesafe �ok az kald�ysa buraya kadar geldi demektir. yani �emberin pozisyonunu aitoldu�u �ember soketinin pozisyonuna direk e�itledik. .10 luk mesafeye geldi anda oturacak yeni soketine
                SoketOtur = false; // false yaparak buradaki i�lemi bitiriyoruz. 

                _AitOlduguStand = GidecegiStand; // teknik i�lemler kald�. yeni ait oldu�u stand ��nk� stand� de�i�ti. aitoldu�u stand� gidece�i stanada e�itleyerek �emberimizin aitoldu�u stand� g�ncllemis oluyoruz

                if (_AitOlduguStand.GetComponent<Stand>()._Cemberler.Count>1) // �imdi yeni standta 2 tane �ember olsun en �stteki �ember hareket edebiliri true idi. ama en �stteki �emberin ustune yeni �ember gelirse yeni �emberin alt�ndaki �emberin hareketeedebilirmi fonksiyonunu false yapmam�z gerekiyor
                {
                    _AitOlduguStand.GetComponent<Stand>()._Cemberler[^2].GetComponent<Cember>().HareketEdebilirMi = false;

                }
                _GameManager.HareketVar = false;

            }
        }
        if (SoketeGeriGit)
        {
            transform.position = Vector3.Lerp(transform.position, _AitOlduguCemberSoketi.transform.position, .04f); 
            // _Aitoldu�uCemberSoketi zaten �emberde tan�ml� olaca�� i�in zaten burada s�rekli bir de�er var o sebeple bu de�i�medi�i i�in �emberin o anki ait oldu�u �ember soketini zaten tan�mlad���m�z i�in burada kullanabilmekteyiz. 
            if (Vector3.Distance(transform.position, _AitOlduguCemberSoketi.transform.position) < .10) 
            {

                transform.position = _AitOlduguCemberSoketi.transform.position;
                SoketeGeriGit = false; 

                
                _GameManager.HareketVar = false;

            }
        }
    }
}
