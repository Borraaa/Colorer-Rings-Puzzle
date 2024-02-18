using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class Pool : MonoBehaviour
{


    public List<GameObject> objectList; // Kullanýlacak objelerin listesi
    public int currentIndex = 0; // Þu anki objenin indeksi
    //public ObjectPool objectPool; // Object Pool nesnesi
    GameManager gameManager;
    private void Start()
    {
        // Object Pool script'ine eriþelim
       // objectPool = GetComponent<ObjectPool>();

        // Ýlk objeyi kullanýma alalým
        UseNextObject();
        
    }

    // Bir sonraki objeyi kullanma
    public void UseNextObject()
    {
        
        if (currentIndex < objectList.Count)
        {
            //objectList[currentIndex].SetActive(true);
            //currentIndex++;

            GameObject obj = objectList[currentIndex];

            //levelPool[currentIndex].gameObject.SetActive(true);
            // Objeyi Object Pool'dan alalým ve aktif hale getirelim
            // GameObject pooledObj = objectPool.GetObjectFromPool(obj);

            obj.SetActive(true);
            indi();

            //gameManager.StandTamamlandi();

            //obj.SetActive(false);

            // Þu anki objenin indeksini bir arttýralým


        }
        else
        {
            Debug.Log("Listedeki tüm objeler kullanýldý.");

        }

    }
    public void indi()
    {
        currentIndex++;
        objectList[currentIndex].SetActive(false);

        UseNextObject();
        //GameObject obj = objectList[currentIndex];
        //obj.SetActive(false);
        Debug.Log("aa");
    }

    // Havuza geri dönen objeyi iþaretleyelim


    //public List<GameObject> levelPrefab = new List<GameObject>();
    //public GameObject instance;
    //public static Pool instance;
    //public List<GameObject> levelPool = new List<GameObject>();
    //private int currentIndex = 0;
    //private GameObject nolist;
    //GameManager gameManager;
    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //}

    //public void Start()
    //{
    //    level();
    //}
    //public void level()
    //{



    //    //levelPool[currentIndex].gameObject.SetActive(true);
    //    //currentIndex++;

    //    //levelPool[0].GetComponent<Transform>();
    //    //gameManager.StandTamamlandi();
    //    //nolist = levelPool[0];


    //    //levelPool.Add(nolist);
    //    //levelPool[0].gameObject.SetActive(true);
    //    //levelPool[0].GetComponent<Transform>();
    //    //nolist = levelPool[0];
    //    //levelPool.RemoveAt(0);

    //    //Invoke("GetTo", 0.1f);
    //}
    ////public void GetTo()
    ////{
    ////    levelPool.Add(nolist);
    ////    nolist.SetActive(false);
    ////    level();
    ////}
}


        


//    private void Start()
//    {
//        // Ýlk level'i instantiate et
//        InstantiateLevel();
//    }

//    private void InstantiateLevel()
//    {
//        // Level prefab'ýný havuzda oluþtur
//        GameObject newLevel = Instantiate(levelPrefab);
//        newLevel.SetActive(true);
//        levelPool.Add(newLevel);
//    }

//    public void CompleteLevel(GameObject completedLevel)
//    {
//        // Tamamlanan level'i havuzdan kaldýr
//        if (levelPool.Contains(completedLevel))
//        {
//            levelPool.Remove(completedLevel);
//            Destroy(completedLevel);

//            // Yeni level'i instantiate et
//            InstantiateLevel();
//        }
//    }

//    public void LoadNextScene()
//    {
//        // Yeni sahneye geçiþ yap
//        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
//        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
//        {
//            SceneManager.LoadScene(nextSceneIndex);
//        }
//        else
//        {
//            Debug.LogWarning("No next scene available!");
//        }
//    }
//}




//    [SerializeField]
//    private GameObject prefab = default;

//    [Header("Current Pooling this component:")]
//    public List<GameObject> Pools = new List<GameObject>();

//    public void PoolInstaller(int size)
//    {
//        for (int i = 0; i < size; i++)
//        {
//            InstantiateObject();
//        }
//    }

//    public GameObject Spawn()
//    {
//        foreach (var item in Pools)
//        {
//            if (!item.activeSelf)
//            {
//                item.SetActive(true);
//                return item;
//            }
//        }
//        var newObject = InstantiateObject();
//        newObject.SetActive(false);
//        return newObject;
//    }

//    public void BackToPool(GameObject obj)
//    {
//        if (Pools.Contains(obj))
//        {
//            obj.SetActive(false);
//        }
//        else
//        {
//            Debug.LogWarning("Level - " + obj.name + " Level");
//        }
//    }

//    private GameObject InstantiateObject()
//    {
//        var newObject = Instantiate(prefab);
//        newObject.transform.SetParent(transform);
//        newObject.SetActive(false);
//        Pools.Add(newObject);
//        return newObject;
//    }
//}







