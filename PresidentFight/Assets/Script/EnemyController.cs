using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public HealthBar healthBar;
    public int damage = 1;
    public float atackTimer = 0f;
    public float blockChance = 30f;
    public float evadeChance = 20f;
    public PlayerControler playerControler;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if(atackTimer <=0)
            atackTimer =1f;
        if (atackTimer > 0f)
        {
            atackTimer -=Time.deltaTime;
            if (atackTimer <= 0f)
                Atac();
        }
    }

    public void TakeDamage(int damage)
    {
        if(Random.Range(0,100) <= 50)
            Evade(damage);
        else 
            Block(damage);
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
            Die();
    }

    void Atac()
    {
        playerControler.TakeDamage(damage);
    }
    void Evade(int damage)
    {
        if (Random.Range(0,100) < evadeChance)
        {
            currentHealth += damage;
            healthBar.SetHealth(currentHealth);
            Debug.Log("EnemyEvade");
        }
    }

    void Block(int damage)
    {
        if (Random.Range(0,100) < blockChance)
        {
            currentHealth += damage;
            healthBar.SetHealth(currentHealth);
            Debug.Log("EnemyBlock");
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
