using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool 
{
    private List<GameObject> objectPool = new List<GameObject>();
    private GameObject objectPrefab;
    private Transform poolParent;
    [SerializeField] private int initialSize;

    private int maxPoolSize; // S? l??ng t?i ?a cho Object Pool
    bool isDelete;

    public ObjectPool(GameObject prefab, int initialSize, int maxPoolSize, Transform parentTransform = null)
    {
        objectPrefab = prefab;
        poolParent = parentTransform;
        this.maxPoolSize = maxPoolSize;
        this.InitialSize = initialSize;
        for (int i = 0; i < initialSize; i++)
        {
            CreateObject();
        }
    }

    public int InitialSize { get => initialSize; set => initialSize = value; }

    public GameObject GetObject()
    {
        if (objectPool.Count > maxPoolSize)
        {
            for (int i = 0; i < objectPool.Count - InitialSize; i++)
            {
                if (!objectPool[i].gameObject.activeInHierarchy)
                {
                    GameObject objToRemove = objectPool[i];
                    objectPool.RemoveAt(i);
                    GameObject.Destroy(objToRemove.gameObject);
                }
            }
        }

        foreach (GameObject obj in objectPool)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        if (objectPool.Count < maxPoolSize)
        {   
            GameObject newObj = CreateObject();
            newObj.gameObject.SetActive(true);
            return newObj;
        }
        else
        {
            for (int i = 0; i < objectPool.Count - InitialSize; i++)
            {
                if (!objectPool[i].gameObject.activeInHierarchy)
                {
                    GameObject objToRemove = objectPool[i];
                    objectPool.RemoveAt(i);
                    GameObject.Destroy(objToRemove.gameObject);
                }
            }

            GameObject newObj = CreateObject();
            newObj.gameObject.SetActive(true);
            return newObj;
        }

    }

    public void ReturnObject(GameObject obj)
    {
        obj.gameObject.SetActive(false);
    }

    private GameObject CreateObject()
    {
        GameObject newObj = Object.Instantiate(objectPrefab);
        newObj.gameObject.SetActive(false);

        if (poolParent != null)
        {
            newObj.transform.parent = poolParent;
        }

        objectPool.Add(newObj);
        return newObj;
    }
}