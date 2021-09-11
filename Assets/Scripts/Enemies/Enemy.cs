using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float healthRegenRate; // TBD
    private float currentHealth;

    [SerializeField] private string explosionTag;


    [SerializeField] private string projectileTag;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackTime;
    [SerializeField] private float lastAttack;
    [SerializeField] private float projectileSpeed;

    [SerializeField] private float minFireAngle;
    [SerializeField] private float maxFireAngle;

    private bool isDead = false;
    Explodable explodableBehavior;
    EnemyFlockAgent flockAgent;

    private Transform target;

    private void Awake()
    {
        explodableBehavior = GetComponent<Explodable>();
        flockAgent = GetComponent<EnemyFlockAgent>();
        flockAgent.hasTarget = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        explodableBehavior.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead) return;

        if(target != null)
        {
            if(Time.time - attackTime >= lastAttack)
            {
                FireAtTarget();
            }
        }
    }

    private void FireAtTarget()
    {
        Vector3 vectorToTarget = target.transform.position - transform.position;

        float dot = Vector3.Dot(vectorToTarget.normalized, transform.up);
        float fireAngle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        // Add 90 as our sprite is facing up
        if (fireAngle > maxFireAngle) return;

        float relAngle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        float angle = relAngle + 90.0f;


        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        PoolableObject pro = ObjectPooler.Instance.SpawnFromPool(projectileTag, transform.position, q);
        Vector3 vel = (target.position - transform.position).normalized * projectileSpeed;
        pro.GetComponent<Rigidbody2D>().velocity = vel;

        lastAttack = Time.time;
    }

    public void TakeDamage(float amount, int lastChain, bool direct)
    {
        if (isDead) return;
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            OnDeath(lastChain, direct);
        }
    }

    private void OnDeath(int lastChain, bool direct)
    {
        isDead = true;
        ScoreManager.Instance.EnemyKillCount += 1;
        if(direct)
        {
            explodableBehavior.Explode(lastChain);
        } else
        {
            explodableBehavior.ExplodeAfter(lastChain, explodableBehavior.explodeDelay);
        }
        //DestroySelfAfter(explodableBehavior.explodeDelay);
        //gameObject.SetActive(false);
    }

    private void DestroySelfAfter(float delay)
    {
        StartCoroutine(SetInactive(delay));
    }

    private IEnumerator SetInactive(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false); // Send back to object pool
    }

    public void SetTarget(Transform t)
    {
        target = t;
        flockAgent.hasTarget = true;
    }

    public void LoseTarget()
    {
        target = null;
        flockAgent.hasTarget = false;
    }

}
