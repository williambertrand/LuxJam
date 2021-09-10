using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{

    public float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy hit = collision.gameObject.GetComponent<Enemy>();
            hit.TakeDamage(damage);
        }

        if (collision.gameObject.CompareTag("Bomb"))
        {
            Bomb hit = collision.gameObject.GetComponent<Bomb>();
            hit.Explode();
        }
        // This removes the projectile when it hits out board bounds
        gameObject.SetActive(false); // This object is pooled so dont destroy it 
    }
}
