using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPooler : MonoBehaviour
{

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public PoolableObject prefab;
        public int capacity;
    }

    #region Singleton

    public static ObjectPooler Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        SetupPool();
    }

    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<PoolableObject>> poolDictionary;

    void SetupPool()
    {
        poolDictionary = new Dictionary<string, Queue<PoolableObject>>();

        foreach (Pool pool in pools)
        {
            Queue<PoolableObject> objectPool = new Queue<PoolableObject>();
            for (int i = 0; i < pool.capacity; i++)
            {
                PoolableObject obj = Instantiate(pool.prefab);
                obj.transform.SetParent(transform);
                obj.gameObject.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    void Start(){}

    public PoolableObject SpawnFromPool(string tag, Vector3 pos, Quaternion rot)
    {

        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("Tool with tag: " + tag + " Does not exist");
            return null;
        }

        PoolableObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.gameObject.SetActive(true);
        objectToSpawn.transform.position = pos;
        objectToSpawn.transform.rotation = rot;
        poolDictionary[tag].Enqueue(objectToSpawn);
        objectToSpawn.OnSpawn();
        return objectToSpawn;
    }


}