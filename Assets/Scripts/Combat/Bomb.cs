using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // What num in a chain of explosions is this bomb?
    public int chain = 1;
    public bool hasExploded = false;


    [SerializeField] private string explosionTag;
    [SerializeField] private float chainRadiusFactor;
    [SerializeField] private float chainDamageFactor;
    [SerializeField] private float baseDamage;
    [SerializeField] private float baseRadius;
    [SerializeField] private float explodeDelay;
    [SerializeField] private float maxRadius;

    private void OnEnable()
    {
        Reset();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ExplodeEffect(float radius)
    {
        Quaternion randRot = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
        PoolableObject exp = ObjectPooler.Instance.SpawnFromPool(explosionTag, transform.position, randRot);
        exp.transform.localScale = new Vector3(radius, radius, 1);
    }


    // Main mechanic of this game: Bombs that explode should explode other
    // bombs in their radius
    public void Explode()
    {

        // First bomb to explode should have chain=1

        float radius = Mathf.Min(chain * baseRadius, maxRadius);

        ExplodeEffect(radius);

        float damage = chain * chainDamageFactor * baseDamage;

        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach(Collider2D col in collisions)
        {
            if (col.CompareTag("Enemy"))
            {
                // Enemy hit
                Enemy e = col.GetComponent<Enemy>();
                e.TakeDamage(damage);
            }
            else if (col.CompareTag("Bomb"))
            {
                // Bomb hit - propegate chain reaction
                Bomb b = col.GetComponent<Bomb>();
                b.ExplodeAfter(chain, explodeDelay);
                
            }
        }

        gameObject.SetActive(false);
    }

    public void ExplodeAfter(int prevChain, float delay)
    {
        if (hasExploded) return;
        chain = prevChain + 1;
        StartCoroutine(ExplodeAfterDelay(delay));
    }

    private IEnumerator ExplodeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Explode();
    }

    private void Reset()
    {
        hasExploded = false;
        chain = 1;
    }
}
