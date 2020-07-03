using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private GameObject prefab;

    private List<GameObject> pool;

    public ObjectPool(GameObject prefab, int initialSize)
    {
        this.prefab = prefab;

        this.pool = new List<GameObject>();
        for (int i = 0; i < initialSize; i++)
        {
            AllocateInstance();
        }
    }

    public GameObject GetInstance()
    {
        if (pool.Count == 0)
        {
            AllocateInstance();
        }

        int lastIndex = pool.Count - 1;
        GameObject instance = pool[lastIndex];
        pool.RemoveAt(lastIndex);

        instance.SetActive(true);
        return instance;
    }

    public void ReturnInstance(GameObject instance)
    {
        instance.SetActive(false);
        pool.Add(instance);
    }

    protected virtual GameObject AllocateInstance()
    {
        GameObject instance = (GameObject)GameObject.Instantiate(prefab);
        instance.SetActive(false);
        pool.Add(instance);

        return instance;
    }

}
