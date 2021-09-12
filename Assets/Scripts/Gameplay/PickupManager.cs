using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{

    [SerializeField] public List<string> drops;
    [SerializeField] public List<int> weights;


    private void Drop(string d, Vector2 pos)
    {
        ObjectPooler.Instance.SpawnFromPool(
            d,
            pos,
            Quaternion.Euler(0, 0, Random.Range(0, 360))
        );
    }

    public void RequestDrop(Vector2 pos, float dropPct)
    {
        if(Random.Range(0.0f, 1.0f) > dropPct)
        {
            return;
        }

        int dropNum = Random.Range(0, 10);
        for(int i = 0; i < drops.Count; i++)
        {
            if (dropNum < weights[i])
            {
                Drop(drops[i], pos);
            }
        }
    }
}
