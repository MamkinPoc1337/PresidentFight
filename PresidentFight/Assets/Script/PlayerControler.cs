using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    public HealthBar healthBar;
    public EnemyController enemyController;
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
    private float staminaRegenTimer;
    public bool canTakeDamage = true;
    public bool isBlocking  = false;
    public bool isDoAnimation = false;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
        staminaRegenTimer = staminaRegenTime;
    }

    void Update()
    {
        staminaBar.SetStamina(currentStamina);
        healthBar.SetHealth(currentHealth);

        HandleStamina();
        UpdateButtonStates();

        if (timer.timeRemaning <= 0)
        {
            DisableAllActions();
        }
    }

    public void Atac()
    {
        enemyController.TakeDamage(damage);
        currentStamina -=1;
    }
    public void Evade(float isEvading)
    {
        if(evadeButton.enabled && isEvading == 1 && enemyController.isAttacDoubleClick == false)
            canTakeDamage = false;
        else
            canTakeDamage = true;
    }

    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            currentHealth -= damage;
            animator.SetTrigger("takeDamage");
        }
        else
            currentStamina --;
        if (currentHealth <= 0)
            Die();
    }
    void HandleStamina()
    {
        if (currentStamina < maxStamina)
        {
            if (isDoAnimation)
            {
                staminaRegenTimer -= Time.deltaTime;
            }
            else
            {
                staminaRegenTimer -= Time.deltaTime * 2; 
            }
            if(staminaRegenTimer <= 0f)
            {
                currentStamina++;
                staminaRegenTimer = staminaRegenTime;
            }
        }
    }

    void UpdateButtonStates()
    {
        bool hasStamina = currentStamina > 0;
        attackButton.enabled = hasStamina && !isDoAnimation;
        evadeButton.enabled = hasStamina && !isDoAnimation;
        blockButton.enabled = hasStamina && !isDoAnimation && !isBlocking;
    }

    void DisableAllActions()
    {
        attackButton.enabled = false;
        evadeButton.enabled = false;
        blockButton.enabled = false;
        isDoAnimation = true;
    }

    public void IsDoingSomeOfAnimation(string animationName)
    {
        if(animationName == "DoAnimation")
            DisableAllActions();
        else
        {
            UpdateButtonStates();
            isBlocking  = false;
            isDoAnimation = false;
        }
    }

    void Die()
    {
        gameOverScreen.Setup("Enemy");
        Destroy(gameObject);
    }
}
