using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    public HealthBar healthBar;
    public EnemyController enemy;
    public StaminaBar staminaBar;
    public GameOverScreen gameOverScreen;
    public Animator animator;
    public Timer timer;
    public Button attackButton;
    public Button evadeButton;
    public Button blockButton;
    public int maxHealth = 100;
    public int maxStamina = 5;
    public int damage = 1;
    public int currentHealth;
    public int currentStamina;
    public float staminaRegenTime = 2f;
    public bool canTakeDamage = true;
    public bool isBlock = false;
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
            attackButton.enabled = false;
            blockButton.enabled = false;
            evadeButton.enabled = false;
        }
        if(currentStamina < maxStamina)
            StaminaBarLogic();
        if (timer.timeRemaning <= 0)
        {
            attackButton.enabled = false;
            blockButton.enabled = false;
            evadeButton.enabled = false;
        }
    }

    public void Atac()
    {
        enemy.TakeDamage(damage);
        currentStamina -=1;
    }
    public void Evade(float isEvading)
    {
        if(evadeButton.enabled == true & isEvading == 1)
        {
            canTakeDamage = false;
        }
        else
            canTakeDamage = true;
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
            attackButton.enabled = true;
            evadeButton.enabled = true;
        }
    }
    public void IsDoingSomeOfAnimation(string animationName)
    {
        if(animationName == "DoAnimation")
        {
            blockButton.enabled = false;
            evadeButton.enabled = false;
            attackButton.enabled = false;
        }
        else
        {
            attackButton.enabled = true;
            blockButton.enabled = true;
            evadeButton.enabled = true;
            isBlock = false;
        }
    }

    void Die()
    {
        gameOverScreen.Setup("Enemy");
        Destroy(gameObject);
    }
}
