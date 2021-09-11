using UnityEngine;

public class PlayerMouseCommands : MonoBehaviour
{

    private PlayerShipCombat combat;

    // Start is called before the first frame update
    void Start()
    {
        combat = GetComponent<PlayerShipCombat>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 20);

            if (hit.collider != null && hit.collider.CompareTag("Bomb"))
            {
                hit.collider.GetComponent<Bomb>().Explode();
            }
        }


        // TESTING: Spawn bomb at mouse loc
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("REMOVE THIS BEFORE BUILD");
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PoolableObject b = combat.DropBomb();
            b.transform.position = pos;
        }

    }
}
