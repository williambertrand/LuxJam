using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    public bool hasTTL;
    public float duration;
    float spawnAt;

    public void OnSpawn()
    {
        spawnAt = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (hasTTL && Time.time - spawnAt >= duration)
        {
            gameObject.SetActive(false);
        }

    }
}
