using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawningManager : MonoBehaviour
{

    [SerializeField] private float spawnTimeStart;
    [SerializeField] private float spawnRadius;
    [SerializeField] private float factor;
    [SerializeField] private float spawnTimeTick;
    private float spawnTime;
    [SerializeField] private float lastSpawn;

    [SerializeField] private EnemyFlock flock;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = spawnTimeStart;
        StartCoroutine(IncreaseSpawnRate());
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastSpawn >= spawnTime)
        {
            flock.SpawnFlockMember(spawnRadius);
            lastSpawn = Time.time;
        }
    }

    IEnumerator IncreaseSpawnRate()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnTimeTick);
            spawnTime *= factor;
        }
    }
}
