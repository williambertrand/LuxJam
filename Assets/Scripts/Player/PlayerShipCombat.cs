using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipCombat : MonoBehaviour
{


    // Basic shooting / projectiles
    [SerializeField] private float playerFireTime;
    [SerializeField] private float lastFire;
    [SerializeField] private string projectileTag;
    [SerializeField] private Transform attackPos;
    [SerializeField] private float projectileSpeed;

    // Bombs:
    [SerializeField] private float playerDropTime;
    [SerializeField] private float lastDrop;
    [SerializeField] private string bombTag;
    [SerializeField] private Transform dropPos;


    // Ref to ships rigidbody for use when shooting
    private Rigidbody2D rigidBody;
    private PlayerInventory inventory;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        inventory = GetComponent<PlayerInventory>();
    }

    private void Fire()
    {
        GameObject pro = ObjectPooler.Instance.SpawnFromPool(projectileTag, attackPos.position, transform.rotation);
        Vector2 projectileVel = transform.up * projectileSpeed;
        pro.GetComponent<Rigidbody2D>().velocity = rigidBody.velocity + projectileVel;
        pro.GetComponent<TTL>().OnSpawn(); // Used for deleting projectile after it's been "alive" for too much time
        lastFire = Time.time;
    }

    // NOTE: Temporarily public and returning the bomb for util elsewhere
    public GameObject DropBomb()
    {
        if (!inventory.ExpendBomb()) return null;

        GameObject pro = ObjectPooler.Instance.SpawnFromPool(bombTag, dropPos.position, transform.rotation);
        Vector2 dropVel = rigidBody.velocity * 0.85f;
        pro.GetComponent<Rigidbody2D>().velocity = dropVel;
        pro.GetComponent<Rigidbody2D>().angularVelocity = 30;
        lastDrop = Time.time;

        return pro;
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if(Time.time - lastFire >= playerFireTime)
            {
                Fire();
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if (Time.time - lastDrop >= playerDropTime)
            {
                DropBomb();
            }
        }
    }
}
