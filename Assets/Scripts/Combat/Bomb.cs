using System.Collections;
using UnityEngine;

public class Bomb : Explodable
{
    
    private void OnEnable()
    {
        Reset();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode(0);
    }

}
