using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseCommands : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider.CompareTag("Bomb"))
            {
                Debug.Log("Clicked Bomb");
                hit.collider.GetComponent<Bomb>().Explode();
            }
        }

    }
}
