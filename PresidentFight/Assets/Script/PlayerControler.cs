using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public HealthBar healthBar;
    public EnemyController enemy;
    public StaminaBar staminaBar;
    public GameOverScreen gameOverScreen;
    public Timer timer;
    public int maxHealth = 100;
    public int maxStamina = 5;
    public int damage = 1;
    public int currentHealth;
    public int currentStamina;
    public float blockTime = 0.2f;
    public float staminaRegenTime = 2f;
    public float simpleTimer = 0f;
    public bool canTakeDamage = true;
    public bool canAtac = true;
    public bool canBloc = true;
    public bool canEvade = true;
    public bool isEvading = false;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
    }

    void Update()
    {
        staminaBar.SetStamina(currentStamina);
        healthBar.SetHealth(currentHealth);
        if (currentStamina <=0)
        {
            canAtac = false;
            canBloc = false;
        }
        if(currentStamina < maxStamina)
            StaminaBarLogic();
        if (timer.timeRemaning <= 0)
        {
            canAtac = false;
            canBloc = false;
            canEvade = false;
        }
    }

    void Atac()
    {
        if (canAtac == true)
        {
            Debug.Log("Atac");
            enemy.TakeDamage(damage);
            currentStamina -=1;
        }
    }
    public void Block(bool _block)
    {
        if(canBloc == true)
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
        else
            canTakeDamage = true;
    }
    void Evade()
    {
        if(canEvade == true)
            Debug.Log("Evade");
    }

    public void TakeDamage(int damage)
    {
        if (canTakeDamage == true)
        {
            currentHealth -= damage;
        }
        else
            currentStamina -=1;
        if (currentHealth <= 0)
            Die();
    }
    public void StaminaBarLogic()
    {
        if(staminaRegenTime >0f)
        {
            staminaRegenTime -= Time.deltaTime;
        }
        else
        {
            staminaRegenTime = 2f;
            currentStamina +=1;
            canAtac = true;
            canBloc = true;
        }
    }

    void Die()
    {
        gameOverScreen.Setup("Enemy");
        Destroy(gameObject);
    }
}
