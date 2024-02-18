using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject SeciliObje; // se�mi� oldu�umuz �emberi bu objede tutulacak.
    GameObject SeciliStand; // se�mi� oldu�umuz stand bu objede tutulacak.

    Cember _Cember; // cemberin script dosyas�n� ba�lad�k.

    UlManager _UlManager;
    GecisSave _GecisSave;
    Pool pool;

    public bool HareketVar; // multi se�imlerde herhangi bir hata olmamas� i�in her harekette kontrol etmemiz gerekiyor.

    public GameObject _finishScrean;
    // bu k�s�ma daha sonra gelinicek.
    public int HedefStandSayisi;
    int TamamlananStandSayisi;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit,100)) // kameradan fiziksel ���n g�nderiri
            {
                if (hit.collider != null && hit.collider.CompareTag("Stand")) 
                    // ���n�m�z�n tan�mli bir�eye �arp�p �arpmad���n� tan�mlamam�z gerekiyor ve t�klan�lan objenin tag�n� 
                    //kesinlikle stand olarak secmem gerekiyor 
                {
                    if (SeciliObje != null && SeciliStand != hit.collider.gameObject)//yeni se�ilen stand i�lemleri
                        // bu k�s�m else �al��t�ktan sonra �al��acak yani oyunda ilk ba�ta stand� se�tik bu se�im kodlar�da elsenin i�inde. art�k bir obje se�tik ve SeciliObje bo� olmayacak. haliyle secmis oldu�um stand yeni se�mi� oldu�um standa e�it de�ilse bu �emberin ba�ka bir yere gidece�ini g�steriyor. e�er e�itse yukaru kald�rd���m�z �ember eski yerine geri d�necek.
                    {
                        Stand _Stand = hit.collider.GetComponent<Stand>(); // yeni se�mi� oldu�um stand� ald�m
                        if (_Stand._Cemberler.Count != 4 && _Stand._Cemberler.Count != 0 ) // full olan standa �ember g�ndermeye �al��t���m�zda eski yerine d�nmesi i�in yap�lan k�s�m. e�erki benim stand�m�n uzunlu�u 4 e e�itde�ilse yani dolmad�ysa && benim �ember listem 0 a e�it de�ilse demekki bu i�lemleri yap diyoruz durumlar� s�n�rlad�k. 
                        {
                            if (_Cember.Renk == _Stand._Cemberler[^1].GetComponent<Cember>().Renk) // se�ili olan �emberin rengi benim yeni se�mi� oldu�um stand�m�n en �stteki �ember rengine e�itse 
                            {
                                SeciliStand.GetComponent<Stand>().SoketDegistirme�slemleri(SeciliObje); // se�ili stand�n listesindeki bu se�ilmi� olan objeyi bu stand�n bu listesinden ��kart ve bu stand� musaitle�tir. 
                                _Cember.HareketEt("PozisyonDegis", hit.collider.gameObject, _Stand.MusaitSoketiVer(), _Stand.HareketPozisyonu);  // �emberimize hareket verdik. sonras�nda �emberin gidecek oldu�u stand� vermem laz�m. hit le objeyi se�tik. sonras�nda  benim gidecek oldu�um stand�n m�sait olan soketini bulamam laz�m.   // sonras�nda benim gidecek oldu�u stand�n hareket pozisyonu laz�m oraya do�ru gidebilmesi i�in

                                _Stand.BosOlanSoket++; // stand�n i�indeki bo� soketi art�rmam�z laz�m ��nk� bu standa yeni �ember eklendi�i i�in bir sonraki �ember geli�indeki musaitlik durumunu set etmem laz�m. e�erki bo� olan soket de�erini art�rmazsam butun �emberler i� i�e ge�ebilir yani iki yada �� �ember ne kadar getirmi� isek hepsi i� i�e ge�er. 
                                _Stand._Cemberler.Add(SeciliObje); // se�ili olan objeyi bu listeye eklememiz gerekiyor. yani benim �emberim gitti ve giderken i�lemlerini yapt� yeni stand�na gitti soketine oturdu ve i�te bu sefer gitmi� oldu�u stand�n �emberler listesine eklenmesi laz�m. yani eski stand�nda kendini sildiriyo yeni stand�ndada kendini ekliyor. 
                                _Stand.CemberleriKontrolEt();
                                SeciliObje = null;
                                SeciliStand = null;
                            }
                            else
                            {
                                _Cember.HareketEt("SoketeGeriGit");
                                SeciliObje = null;
                                SeciliStand = null;
                            }
                            
                        }
                        else if (_Stand._Cemberler.Count == 0) // bo� bir standa soket �ember g�nderebilmemizin yolu 
                        {
                            SeciliStand.GetComponent<Stand>().SoketDegistirme�slemleri(SeciliObje); 
                            _Cember.HareketEt("PozisyonDegis", hit.collider.gameObject, _Stand.MusaitSoketiVer(), _Stand.HareketPozisyonu);  

                            _Stand.BosOlanSoket++; 
                            _Stand._Cemberler.Add(SeciliObje);
                            _Stand.CemberleriKontrolEt();
                            SeciliObje = null;
                            SeciliStand = null;
                        }
                        else // e�erki 4 e ve 0 a e�itse �emberi ��karm�� oldu�umuz sokete geri sok. 
                        {
                            _Cember.HareketEt("SoketeGeriGit"); // se�ili olan stand ve se�mi� oldu�um stand e�it ise haliyle �ember tekrar sokete geri git b�l�m�n� �al��t�racak
                            SeciliObje = null;
                            SeciliStand = null; // daha sonra benim se�ili objemle stand�m� temizle diyorum. 
                        }


                        



                    }

                    else if (SeciliStand==hit.collider.gameObject)// se�mi� oldu�um stand yeni se�mi� oldu�um standa e�itse yani amac�m�z se�mi� oldu�umuz �emberi tekrar eski yerine sokmak i�in bu ko�ulda bu eylemi yakal�yorum
                    {
                        _Cember.HareketEt("SoketeGeriGit"); // se�ili olan stand ve se�mi� oldu�um stand e�it ise haliyle �ember tekrar sokete geri git b�l�m�n� �al��t�racak
                        SeciliObje = null;
                        SeciliStand = null; // daha sonra benim se�ili objemle stand�m� temizle diyorum. 
                    } 

                    else
                    {
                        Stand _Stand = hit.collider.GetComponent<Stand>(); // se�mi� oldu�um stand�n scriptine eri�tim.
                        SeciliObje = _Stand.EnUsttekiCemberiVer(); // stand scriptine ba�lan�p enusttekicemberiver fonksiyonuna eri�ip
                        //stand�n en �stteki eleman�na ula�t�k
                        _Cember = SeciliObje.GetComponent<Cember>();//cember class�n�n i�erisine se�ili objem ve getcomponent diyerek
                        // Cember scriptini buraya �a��r�yoruz.
                        HareketVar = true; // bir obje se�ildi�i zaman gamemanager bunu anlamas� laz�m = true hareket ba�lad� demektir.

                        if (_Cember.HareketEdebilirMi) // burada se�mi� oldu�um objenin cember scriptine eri�tim ya �ncelikle �una bakmam laz�m bu �ember hareket edebilirmi ? e�er bu �ember hareket edebilir pozisyonda ise i�te o zaman i�lemlerimi yapaca��m.
                        {
                            _Cember.HareketEt("Secim", null, null, _Cember._AitOlduguStand.GetComponent<Stand>().HareketPozisyonu); // cemberin i�erisindeki HareketEt fonksiyonunu �a��rd�k. �emberimin ait oldu�u stand�n compenentlerinden eri�erek o hareket pozisyonunu yani �emberin ait oldu�u stand�n hareket pozisyonuna eri� diyoruz. 

                            SeciliStand = _Cember._AitOlduguStand; // se�ili platfom da i�lem yapabilmemiz i�in bu �ekilde tan�mlad�k


                        }
                    }
                }
            }
        }




    }
    public void FinishScrean()
    {
        _finishScrean.SetActive(true);
    }
    public void StandTamamlandi()
    {
        
        TamamlananStandSayisi++;
        if (TamamlananStandSayisi == HedefStandSayisi)
        {
            //pool.UseNextObject();
            FinishScrean();
            

            //Debug.Log("KAZANDIN"); // kazand�n paneli ��kacak. bir sonraki b�l�me ge�me i�lemleri burada olabilir. 
        }
    }
}
