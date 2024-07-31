using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public HealthBar healthBar;
    public EnemyController enemy;
    public int damage = 3;
    public float blockTime = 0.2f;
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
        Debug.Log("3");
        enemy.TakeDamage(damage);
    }
    void Block(bool _block)
    {

        Debug.Log("2");
    }
    void Evade()
    {
        Debug.Log("1");

    }

    public void TakeDamage(int damage)
    {

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
      Destroy(gameObject);
    }
}
