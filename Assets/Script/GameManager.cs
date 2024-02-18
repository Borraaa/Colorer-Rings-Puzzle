using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject SeciliObje; // seçmiþ olduðumuz çemberi bu objede tutulacak.
    GameObject SeciliStand; // seçmiþ olduðumuz stand bu objede tutulacak.

    Cember _Cember; // cemberin script dosyasýný baðladýk.

    UlManager _UlManager;
    GecisSave _GecisSave;
    Pool pool;

    public bool HareketVar; // multi seçimlerde herhangi bir hata olmamasý için her harekette kontrol etmemiz gerekiyor.

    public GameObject _finishScrean;
    // bu kýsýma daha sonra gelinicek.
    public int HedefStandSayisi;
    int TamamlananStandSayisi;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit,100)) // kameradan fiziksel ýþýn gönderiri
            {
                if (hit.collider != null && hit.collider.CompareTag("Stand")) 
                    // ýþýnýmýzýn tanýmli birþeye çarpýp çarpmadýðýný tanýmlamamýz gerekiyor ve týklanýlan objenin tagýný 
                    //kesinlikle stand olarak secmem gerekiyor 
                {
                    if (SeciliObje != null && SeciliStand != hit.collider.gameObject)//yeni seçilen stand iþlemleri
                        // bu kýsým else çalýþtýktan sonra çalýþacak yani oyunda ilk baþta standý seçtik bu seçim kodlarýda elsenin içinde. artýk bir obje seçtik ve SeciliObje boþ olmayacak. haliyle secmis olduðum stand yeni seçmiþ olduðum standa eþit deðilse bu çemberin baþka bir yere gideceðini gösteriyor. eðer eþitse yukaru kaldýrdýðýmýz çember eski yerine geri dönecek.
                    {
                        Stand _Stand = hit.collider.GetComponent<Stand>(); // yeni seçmiþ olduðum standý aldým
                        if (_Stand._Cemberler.Count != 4 && _Stand._Cemberler.Count != 0 ) // full olan standa çember göndermeye çalýþtýðýmýzda eski yerine dönmesi için yapýlan kýsým. eðerki benim standýmýn uzunluðu 4 e eþitdeðilse yani dolmadýysa && benim çember listem 0 a eþit deðilse demekki bu iþlemleri yap diyoruz durumlarý sýnýrladýk. 
                        {
                            if (_Cember.Renk == _Stand._Cemberler[^1].GetComponent<Cember>().Renk) // seçili olan çemberin rengi benim yeni seçmiþ olduðum standýmýn en üstteki çember rengine eþitse 
                            {
                                SeciliStand.GetComponent<Stand>().SoketDegistirmeÝslemleri(SeciliObje); // seçili standýn listesindeki bu seçilmiþ olan objeyi bu standýn bu listesinden çýkart ve bu standý musaitleþtir. 
                                _Cember.HareketEt("PozisyonDegis", hit.collider.gameObject, _Stand.MusaitSoketiVer(), _Stand.HareketPozisyonu);  // çemberimize hareket verdik. sonrasýnda çemberin gidecek olduðu standý vermem lazým. hit le objeyi seçtik. sonrasýnda  benim gidecek olduðum standýn müsait olan soketini bulamam lazým.   // sonrasýnda benim gidecek olduðu standýn hareket pozisyonu lazým oraya doðru gidebilmesi için

                                _Stand.BosOlanSoket++; // standýn içindeki boþ soketi artýrmamýz lazým çünkü bu standa yeni çember eklendiði için bir sonraki çember geliþindeki musaitlik durumunu set etmem lazým. eðerki boþ olan soket deðerini artýrmazsam butun çemberler iç içe geçebilir yani iki yada üç çember ne kadar getirmiþ isek hepsi iç içe geçer. 
                                _Stand._Cemberler.Add(SeciliObje); // seçili olan objeyi bu listeye eklememiz gerekiyor. yani benim çemberim gitti ve giderken iþlemlerini yaptý yeni standýna gitti soketine oturdu ve iþte bu sefer gitmiþ olduðu standýn çemberler listesine eklenmesi lazým. yani eski standýnda kendini sildiriyo yeni standýndada kendini ekliyor. 
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
                        else if (_Stand._Cemberler.Count == 0) // boþ bir standa soket çember gönderebilmemizin yolu 
                        {
                            SeciliStand.GetComponent<Stand>().SoketDegistirmeÝslemleri(SeciliObje); 
                            _Cember.HareketEt("PozisyonDegis", hit.collider.gameObject, _Stand.MusaitSoketiVer(), _Stand.HareketPozisyonu);  

                            _Stand.BosOlanSoket++; 
                            _Stand._Cemberler.Add(SeciliObje);
                            _Stand.CemberleriKontrolEt();
                            SeciliObje = null;
                            SeciliStand = null;
                        }
                        else // eðerki 4 e ve 0 a eþitse çemberi çýkarmýþ olduðumuz sokete geri sok. 
                        {
                            _Cember.HareketEt("SoketeGeriGit"); // seçili olan stand ve seçmiþ olduðum stand eþit ise haliyle çember tekrar sokete geri git bölümünü çalýþtýracak
                            SeciliObje = null;
                            SeciliStand = null; // daha sonra benim seçili objemle standýmý temizle diyorum. 
                        }


                        



                    }

                    else if (SeciliStand==hit.collider.gameObject)// seçmiþ olduðum stand yeni seçmiþ olduðum standa eþitse yani amacýmýz seçmiþ olduðumuz çemberi tekrar eski yerine sokmak için bu koþulda bu eylemi yakalýyorum
                    {
                        _Cember.HareketEt("SoketeGeriGit"); // seçili olan stand ve seçmiþ olduðum stand eþit ise haliyle çember tekrar sokete geri git bölümünü çalýþtýracak
                        SeciliObje = null;
                        SeciliStand = null; // daha sonra benim seçili objemle standýmý temizle diyorum. 
                    } 

                    else
                    {
                        Stand _Stand = hit.collider.GetComponent<Stand>(); // seçmiþ olduðum standýn scriptine eriþtim.
                        SeciliObje = _Stand.EnUsttekiCemberiVer(); // stand scriptine baðlanýp enusttekicemberiver fonksiyonuna eriþip
                        //standýn en üstteki elemanýna ulaþtýk
                        _Cember = SeciliObje.GetComponent<Cember>();//cember classýnýn içerisine seçili objem ve getcomponent diyerek
                        // Cember scriptini buraya çaðýrýyoruz.
                        HareketVar = true; // bir obje seçildiði zaman gamemanager bunu anlamasý lazým = true hareket baþladý demektir.

                        if (_Cember.HareketEdebilirMi) // burada seçmiþ olduðum objenin cember scriptine eriþtim ya öncelikle þuna bakmam lazým bu çember hareket edebilirmi ? eðer bu çember hareket edebilir pozisyonda ise iþte o zaman iþlemlerimi yapacaðým.
                        {
                            _Cember.HareketEt("Secim", null, null, _Cember._AitOlduguStand.GetComponent<Stand>().HareketPozisyonu); // cemberin içerisindeki HareketEt fonksiyonunu çaðýrdýk. çemberimin ait olduðu standýn compenentlerinden eriþerek o hareket pozisyonunu yani çemberin ait olduðu standýn hareket pozisyonuna eriþ diyoruz. 

                            SeciliStand = _Cember._AitOlduguStand; // seçili platfom da iþlem yapabilmemiz için bu þekilde tanýmladýk


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
            

            //Debug.Log("KAZANDIN"); // kazandýn paneli çýkacak. bir sonraki bölüme geçme iþlemleri burada olabilir. 
        }
    }
}
