using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public PlayerControler playerControler;
    public HealthBar healthBar;
    public StaminaBar staminaBar;
    public GameOverScreen gameOverScreen;
    public Timer timer;
    public int maxHealth = 10;
    public int currentHealth;
    public int maxStamina = 5;
    public int currentStamina;
    public int damage = 1;
    public float atackTimer = 0f;
    public float staminaRegenTime = 2f;
    public float blockChance = 30f;
    public float evadeChance = 20f;
    public bool canAtac = true;
    public bool canBloc = true;
    public bool canEvade = true;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
    }

    void Update()
    {
        healthBar.SetHealth(currentHealth);
        staminaBar.SetStamina(currentStamina);
        if (currentStamina <=0)
        {
            canAtac = false;
            canBloc = false;
            canEvade = false;
        }
        if(currentStamina < maxStamina)
            StaminaBarLogic();
        if(atackTimer <=0)
            atackTimer =1f;
        if (atackTimer > 0f)
        {
            atackTimer -=Time.deltaTime;
            if (atackTimer <= 0f)
                Atac();
        }
        if(timer.timeRemaning <= 0)
        {
            canAtac = false;
            canBloc = false;
            canEvade =false;
        }
    }

    public void TakeDamage(int damage)
    {
        if(Random.Range(0,100) <= 50)
            Evade(damage);
        else 
            Block(damage);
        currentHealth -= damage;
        if (currentHealth <= 0)
            Die();
    }

    void Atac()
    {
        if (canAtac == true)
        {
            playerControler.TakeDamage(damage);
            currentStamina -=1;
            if (currentStamina <=0)
                canAtac = false;
        }

    }
    void Evade(int damage)
    {
        if(canEvade == true)
        {
            if (Random.Range(0,100) < evadeChance)
            {
                currentHealth += damage;
                Debug.Log("EnemyEvade");
            }
        }
    }

    void Block(int damage)
    {
        if(canBloc == true)
        {
            if (Random.Range(0,100) < blockChance)
            {
                currentHealth += damage;
                Debug.Log("EnemyBlock");
            }
        }
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
            canEvade = true;
        }
    }

    void Die()
    {
        gameOverScreen.Setup("Player");
        Destroy(gameObject);
    }
}
