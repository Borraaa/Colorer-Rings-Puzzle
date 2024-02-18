using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;



public class GecisSave : MonoBehaviour
{

    [SerializeField] private int IDD;
    private string keyLv = "keyLv";
    private int lv;
    private string keyy;
    private int sahne_ýd;
    void Start()
    {
        Load();
        //save();

        
    }
    public void LoadNextScene()
    {


        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex + 1);

        //currentSceneIndex + 1
        save();
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);

        //save();
    }
    //public void NextScene()
    //{
    //    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    //    SceneManager.LoadScene(currentSceneIndex+1);
    //    save();

    //}
    public void save()
    {
        string keyy = keyLv + IDD.ToString();
        PlayerPrefs.SetString(keyy, "savedd");
        PlayerPrefs.SetInt(keyLv, lv);
    }
    public void Load()
    {
        string keyy = keyLv + IDD.ToString();
        string statuss = PlayerPrefs.GetString(keyy);

        if (statuss.Equals("savedd"))
        {
            LoadNextScene();
            //LoadStartScene();
            //ResetPrefabs();
            //NextScene();
            //Equals("savedd")
        }
    }





    //private int lv;
    //private string keylv = "keylv";

    //public void Start()
    //{
    //    load();
    //}
    //public void load()
    //{
    //    PlayerPrefs.GetInt(keylv, 0);
    //}
    //private void Save()
    //{
    //    PlayerPrefs.SetInt(keylv, lv);
    //}




    //public TMP_Text text;

    //public void Save()
    //{
    //    PlayerPrefs.SetInt("SceneSaved", SceneManager.GetActiveScene().buildIndex);
    //}
    //public void Load()
    //{
    //    SceneManager.LoadScene(PlayerPrefs.GetInt("SceneSaved"));
    //}
    //public void LoadNextScene()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}


    //private const string sceneIndexKey = "1";
    //public void Start()
    //{
    //    LoadSceneIndex();
    //}
    //public void SaveSceneIndex()
    //{
    //    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
    //    PlayerPrefs.SetInt(sceneIndexKey, currentSceneIndex);
    //    PlayerPrefs.Save();

    //}

    //public void LoadSceneIndex()
    //{
    //    if (PlayerPrefs.HasKey(sceneIndexKey))
    //    {
    //        int savedSceneIndex = PlayerPrefs.GetInt(sceneIndexKey);
    //        SceneManager.LoadScene(savedSceneIndex);

    //    }

    //    else
    //    {
    //        Debug.LogWarning("No scene index saved.");

    //    }

    //    }





    // private int sonrakiLevelIndex;

    //public void Start()
    //{
    //    sonrakiLevelIndex = SceneManager.GetActiveScene().buildIndex ;

    //    Yükle(); 

    //}

    //public void Yükle()
    //{
    //    if (PlayerPrefs.HasKey("sonrakiLevel"))
    //    {
    //        sonrakiLevelIndex = PlayerPrefs.GetInt("sonrakiLevel");
    //        SceneManager.LoadScene(sonrakiLevelIndex);
    //    }

    //}

    //public void Save()
    //{
    //    PlayerPrefs.SetInt("sonrakiLevel", sonrakiLevelIndex);
    //    PlayerPrefs.Save();
    //}

    //public void LoadNextLevel()
    //{
    //    sonrakiLevelIndex++;
    //    Save();
    //    SceneManager.LoadScene(sonrakiLevelIndex);
    //}

    //[SerializeField] private int ID;
    //private int lv;
    //private string keylv = "keylv";
    //private string keyy;

    //private void Start()
    //{
    //    Load();
    //}
    //public void LoadNextScane()
    //{
    //    int currentindex = SceneManager.GetActiveScene().buildIndex;
    //    currentindex++;
    //    SceneManager.LoadScene(currentindex);
    //    Save();

    //}
    //public void LoadStartScane()
    //{
    //    //SceneManager.LoadScene(0);

    //}

    //public void OnApplicationQuit()
    //{
    //    //Save();
    //}
    //public void Save()
    //{
    //    string keyy = keylv + ID.ToString();
    //    PlayerPrefs.SetString(keyy, "savedd");
    //    PlayerPrefs.SetInt(keyy, lv);
    //}
    //public void Load()
    //{
    //    string keyy = keylv + ID.ToString();
    //    string statuss = PlayerPrefs.GetString(keyy);
    //    if (statuss.Equals("savedd"))
    //    {
    //        LoadNextScane();
    //    }
    //}

    //private string levelKey = "PlayerLevel";
    //private int currentLevel;



    //void Start()
    //{

    //}
    //public void OnApplicationQuit()
    //{
    //    SaveGame();
    //    // Oyun kapatýldýðýnda ilerlemeyi kaydetme
    //}
    //public void SaveGame()
    //{
    //    PlayerPrefs.SetInt(levelKey, currentLevel);
    //    PlayerPrefs.Save();
    //}
    //public void LoadGame()
    //{
    //    if (PlayerPrefs.HasKey(levelKey))
    //    {
    //        currentLevel = PlayerPrefs.GetInt(levelKey);
    //        // Kaydedilmiþ seviyeyi yükleme
    //    }
    //}
    //// Update is called once per frame
    //void Update()
    //{

    //}

}
