using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShipHealth : MonoBehaviour
{

    #region Singleton
    public static PlayerShipHealth Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    [SerializeField] private float maxHealth;
    private float currentHealth;
    [SerializeField] Slider healthBar;

    [SerializeField] private float maxShield;
    [SerializeField] private float shieldRegenDelay;
    [SerializeField] private float shieldRegenTickTime;
    [SerializeField] private float shieldRegenTickAmt;
    [SerializeField] Slider shieldBar;
    private bool isShieldRegen = true;
    private float currentShield;

    private float lastDamageAt;
    private float lastShieldTick;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;

        healthBar.maxValue = maxHealth;
        shieldBar.maxValue = maxShield;
        healthBar.value = maxHealth;
        shieldBar.value = maxShield;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastDamageAt >= shieldRegenDelay && currentShield < maxShield)
        {
            isShieldRegen = true;
        }

        if (isShieldRegen && currentShield < maxShield)
        {
            if (Time.time - lastShieldTick >= shieldRegenTickTime)
            {
                currentShield += shieldRegenTickAmt;
                lastShieldTick = Time.time;

                if (currentShield > maxShield)
                {
                    currentShield = maxShield;
                }
                shieldBar.value = currentShield;
            }
        }
    }

    public void TakeDamage(float amount)
    {
        lastDamageAt = Time.time;
        isShieldRegen = false;
        float remaining = amount;

        if(currentShield < amount && currentShield > 0)
        {
            remaining = amount - currentShield;
            currentShield = 0;
            shieldBar.value = currentShield;
        } else if (currentShield >= amount)
        {
            currentShield -= amount;
            shieldBar.value = currentShield;
            return;
        }

        currentHealth -= remaining;
        if (currentHealth <= 0)
        {
            healthBar.value = 0;
            OnDeath();
            return;
        }

        shieldBar.value = currentShield;
        healthBar.value = currentHealth;
    }

    private void OnDeath()
    {
        // GameManager.Instance.OnGameOver()
    }


}
