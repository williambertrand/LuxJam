using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float healthRegenRate; // TBD
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        // TODO
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        //TODO: Increment killed enemy count
        gameObject.SetActive(false);
    }
}
