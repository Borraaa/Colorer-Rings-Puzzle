using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public Dictionary<GameObject, List<GameObject>> objectPool = new Dictionary<GameObject, List<GameObject>>();

    // Belirli bir prefab i�in kullan�labilir bir obje d�nd�rme
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

            // E�er havuzda kullan�labilir bir obje yoksa, yeni bir tane olu�tural�m
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
