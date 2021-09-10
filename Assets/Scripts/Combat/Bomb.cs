using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // What num in a chain of explosions is this bomb?
    public int chain;

    private void OnEnable()
    {
        Debug.Log("Bomb enabled");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Main mechanic of this game: Bombs that explode should explode other
    // bombs in their radius
    public void Explode()
    {
        //TODO
    }
}
