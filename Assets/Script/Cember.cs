using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cember : MonoBehaviour
{
    // not: öncelikle çemberlerde neler olacak yani iþlem türü olarak. öncelikle çemberlerimi ait olduklarý stand larda 
    // tutuyor olmam gerekiyor. yani çember hangi standa ait benim bunu tutmama lazým ki ilgili stand ile ilgili
    // biliyorsun listeye eklenme ve çýkarma iþlemleri yapabileyim. 

    public GameObject _AitOlduguStand;
    public GameObject _AitOlduguCemberSoketi; // çemberin terk etmiþ olduðu pozisyona tekrar geri gelme olayý olacak o kýsýmda ilgili 
    // çemberin hangi sokete ait olduðunu benim tutuyor olmam gerekmekte.
    public bool HareketEdebilirMi;
    public string Renk;
    public GameManager _GameManager; // gamemanager scriptini buraya baðladýk.

    GameObject HareketPozisyonu;
    GameObject GidecegiStand;

    bool Secildi, PosDegistir, SoketOtur, SoketeGeriGit; // soketlerimizin hareketleri vardý. seçim hareketi yani seçtiðimizde soket oluþturduðumuz küpün oraya gidicek
        //sonrasýnda birde baþka bir standa gitme olayý var eðer baþka bir standa gidicekse yani diðer standýn hareket pozisyonuna gidicek ve sonrada sokete oturucuak. bu iþlemleri bool ile kontrol edicez

   
    public void HareketEt(string islem, GameObject Stand = null, GameObject Soket = null, GameObject GidilecekObje = null) 
        // gamemanager dan tetikleyeceðimiz ana fonksiyon. (String islem) ile pozisyonmu deðiþti yada seçildimi yada soketemi oturdu gibi bu iþlemleri kontrol etmek için parametre yazýyoruz. geri kalan parametreler isteðe baðlý kullanýlacaðý için null a eþitledik.
    {
        switch (islem)
        {
            case "Secim": //ben bir çemberi seçtiðimde çemberimin alacaðý tek pozisyon standýmýn hareket pozisyonu yani küpün pos u.
                HareketPozisyonu = GidilecekObje; //objenin hareket ediceði bölümü alaný verdik 
                Secildi = true; // true yapmam gerekiyorki update methodunda hareketimizi saðlayabilelim.
                    break;
            case "PozisyonDegis":
                GidecegiStand = Stand; // gidecek olan standý beni çemberimin ait olduðu standýna eþitlemem gerekiyor. çünkü çemberin mevcut standý deðiþiyor bunu benim göndermem lazým.
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
            transform.position = Vector3.Lerp(transform.position, HareketPozisyonu.transform.position, 2f); // çemberimize pozisyon verdik.lerp kullanarak yumuþak geçiþ yaptýk

            if (Vector3.Distance(transform.position, HareketPozisyonu.transform.position) < .10)// yine mesafe ölçüyorum yani iki pozisyon arasýndaki mesafeyi alarak aslýnda benim çemberimi ilgili pozisyona gelip gelmedðini anlamam için. eðerki bu iki pozisyon rasýndaki mesafe .10 a düþtüyse olay bitti demektir. yani secidi yi false yapki if dursun ratýk
            {
                Secildi = false;
            }
            

        }
        if (PosDegistir)
        {
            transform.position = Vector3.Lerp(transform.position, HareketPozisyonu.transform.position, 2f); // çemberimize pozisyon verdik.lerp kullanarak yumuþak geçiþ yaptýk

            if (Vector3.Distance(transform.position, HareketPozisyonu.transform.position) < .10)// yine mesafe ölçüyorum yani iki pozisyon arasýndaki mesafeyi alarak aslýnda benim çemberimi ilgili pozisyona gelip gelmedðini anlamam için. eðerki bu iki pozisyon rasýndaki mesafe .10 a düþtüyse olay bitti demektir. yani secidi yi false yapki if dursun ratýk
            {
                PosDegistir = false;
                SoketOtur = true;
            }
        }
        if (SoketOtur)
        {
            transform.position = Vector3.Lerp(transform.position, _AitOlduguCemberSoketi.transform.position, .04f); // çemberimize pozisyon verdik.lerp kullanarak yumuþak geçiþ yaptýk

            if (Vector3.Distance(transform.position, _AitOlduguCemberSoketi.transform.position) < .10) // bura artýk çemberimiz yeni oturacak soketin hareketpozisyonuna kadar geldi yani küpüne. artýk çemberin hangi sokete oturucaðýna karar vermemiz gerekiyor. 
            {

                transform.position = _AitOlduguCemberSoketi.transform.position;   // eðer aralarýndaki mesafe çok az kaldýysa buraya kadar geldi demektir. yani çemberin pozisyonunu aitolduðu çember soketinin pozisyonuna direk eþitledik. .10 luk mesafeye geldi anda oturacak yeni soketine
                SoketOtur = false; // false yaparak buradaki iþlemi bitiriyoruz. 

                _AitOlduguStand = GidecegiStand; // teknik iþlemler kaldý. yeni ait olduðu stand çünkü standý deðiþti. aitolduðu standý gideceði stanada eþitleyerek çemberimizin aitolduðu standý güncllemis oluyoruz

                if (_AitOlduguStand.GetComponent<Stand>()._Cemberler.Count>1) // þimdi yeni standta 2 tane çember olsun en üstteki çember hareket edebiliri true idi. ama en üstteki çemberin ustune yeni çember gelirse yeni çemberin altýndaki çemberin hareketeedebilirmi fonksiyonunu false yapmamýz gerekiyor
                {
                    _AitOlduguStand.GetComponent<Stand>()._Cemberler[^2].GetComponent<Cember>().HareketEdebilirMi = false;

                }
                _GameManager.HareketVar = false;

            }
        }
        if (SoketeGeriGit)
        {
            transform.position = Vector3.Lerp(transform.position, _AitOlduguCemberSoketi.transform.position, .04f); 
            // _AitolduðuCemberSoketi zaten çemberde tanýmlý olacaðý için zaten burada sürekli bir deðer var o sebeple bu deðiþmediði için çemberin o anki ait olduðu çember soketini zaten tanýmladýðýmýz için burada kullanabilmekteyiz. 
            if (Vector3.Distance(transform.position, _AitOlduguCemberSoketi.transform.position) < .10) 
            {

                transform.position = _AitOlduguCemberSoketi.transform.position;
                SoketeGeriGit = false; 

                
                _GameManager.HareketVar = false;

            }
        }
    }
}
