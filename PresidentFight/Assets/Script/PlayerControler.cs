using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public HealthBar healthBar;
    public EnemyController enemy;
    public int damage = 3;
    public float blockTime = 0.2f;
    public bool canTakeDamage = true;
    public float simpleTimer = 0f;
    public bool isEvading = false;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {

    }

    void Atac()
    {
        Debug.Log("Atac");
        enemy.TakeDamage(damage);
    }
    public void Block(bool _block)
    {
        if (_block == true)
        {
            Debug.Log("IsBlocing");
            canTakeDamage = false;
        }
        else 
        {
            Debug.Log("IsNotBlocking");
            canTakeDamage = true;
        }
    }
    void Evade()
    {
        if(simpleTimer <=0f)
        {
            simpleTimer -= Time.deltaTime;
            canTakeDamage = false;
        }
        else 
        {
            canTakeDamage = true;
            simpleTimer = 1;
        }
        Debug.Log("Evade");

    }

    public void TakeDamage(int damage)
    {
        if (canTakeDamage == true)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        }
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
      Destroy(gameObject);
    }
}
