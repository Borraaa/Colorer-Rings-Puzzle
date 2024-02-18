using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public Dictionary<GameObject, List<GameObject>> objectPool = new Dictionary<GameObject, List<GameObject>>();

    // Belirli bir prefab için kullanýlabilir bir obje döndürme
    public GameObject GetObjectFromPool(GameObject prefab)
    {
        if (objectPool.ContainsKey(prefab))
        {
            List<GameObject> prefabPool = objectPool[prefab];

            foreach (GameObject obj in prefabPool)
            {
                if (!obj.activeInHierarchy)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }

            // Eðer havuzda kullanýlabilir bir obje yoksa, yeni bir tane oluþturalým
            GameObject newObj = Instantiate(prefab);
            prefabPool.Add(newObj);
            return newObj;
        }
        else
        {
            Debug.LogError("Prefab not found in Object Pool.");
            return null;
        }
    }

    
    
}
