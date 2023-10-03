using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool 
{
    private Stack<GameObject> objectPool;
    private GameObject objectPrefab;
    private Transform poolParent;
    [SerializeField] private int initialSize;

    private int maxPoolSize; // S? l??ng t?i ?a cho Object Pool
    bool isDelete;

    public ObjectPool(GameObject prefab, int initialSize, int maxPoolSize, Transform parentTransform = null)
    {
        objectPool = new Stack<GameObject>();
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
        if (objectPool.Count == 0)
        {
            CreateObject();
        }

        GameObject nextObject = objectPool.Pop();
        nextObject.gameObject.SetActive(true);
        return nextObject;

    }

    public void ReturnObject(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        objectPool.Push(obj);
    }

    private GameObject CreateObject()
    {
        GameObject newObj = Object.Instantiate(objectPrefab);
        newObj.gameObject.SetActive(false);

        if (poolParent != null)
        {
            newObj.transform.parent = poolParent;
        }

        objectPool.Push(newObj);
        return newObj;
    }
}