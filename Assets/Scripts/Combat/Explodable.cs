using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explodable : MonoBehaviour
{

    // What num in a chain of explosions is this bomb?
    public int chain = 1;
    public bool hasExploded = false;


    [SerializeField] private string explosionTag;
    [SerializeField] private float chainRadiusFactor;
    [SerializeField] private float chainDamageFactor;
    [SerializeField] private float baseDamage;
    [SerializeField] private float baseRadius;
    public float explodeDelay;
    [SerializeField] private float maxRadius;


    public void Explode(int lastChain)
    {
        chain = lastChain + 1;

        if (chain > ScoreManager.Instance.MaxChain)
        {
            ScoreManager.Instance.MaxChain = chain;
        }

        float radius = Mathf.Min(chain * baseRadius, maxRadius);

        ExplodeEffect(radius);

        float damage = chain * chainDamageFactor * baseDamage;

        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D col in collisions)
        {
            if (col.CompareTag("Enemy"))
            {
                // Enemy hit
                Enemy e = col.GetComponent<Enemy>();
                e.TakeDamage(damage, chain, false);
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

    private void ExplodeEffect(float radius)
    {
        Quaternion randRot = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
        PoolableObject exp = ObjectPooler.Instance.SpawnFromPool(explosionTag, transform.position, randRot);
        exp.transform.localScale = new Vector3(radius * 2f, radius * 2f, 1);
    }


    public void ExplodeAfter(int prevChain, float delay)
    {
        if (hasExploded) return;
        StartCoroutine(ExplodeAfterDelay(delay, prevChain));
    }

    private IEnumerator ExplodeAfterDelay(float delay, int lastChain)
    {
        yield return new WaitForSeconds(delay);
        Explode(lastChain);
    }

    public void Reset()
    {
        hasExploded = false;
        chain = 1;
    }
}
