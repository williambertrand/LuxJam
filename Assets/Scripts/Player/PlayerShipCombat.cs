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
    [SerializeField] private Transform attackPos2;
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
        PoolableObject pro = ObjectPooler.Instance.SpawnFromPool(projectileTag, attackPos.position, transform.rotation);
        Vector2 projectileVel = transform.up * projectileSpeed;
        pro.GetComponent<Rigidbody2D>().velocity = rigidBody.velocity + projectileVel;

        // Messy, but quick
        if(attackPos2 != null)
        {
            PoolableObject pro2 = ObjectPooler.Instance.SpawnFromPool(projectileTag, attackPos2.position, transform.rotation);
            pro2.GetComponent<Rigidbody2D>().velocity = rigidBody.velocity + projectileVel;
        }

        lastFire = Time.time;
    }

    // NOTE: Temporarily public and returning the bomb for util elsewhere
    public PoolableObject DropBomb()
    {
        if (!inventory.ExpendBomb()) return null;

        PoolableObject pro = ObjectPooler.Instance.SpawnFromPool(bombTag, dropPos.position, transform.rotation);
        Vector2 dropVel = rigidBody.velocity * -0.6f;
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
