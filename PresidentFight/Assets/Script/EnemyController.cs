using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public PlayerControler playerControler;
    public HealthBar healthBar;
    public StaminaBar staminaBar;
    public GameOverScreen gameOverScreen;
    public Animator animator;
    public Timer timer;

    public int maxHealth = 10;
    public int currentHealth;
    public int maxStamina = 5;
    public int currentStamina;
    public int damage = 1;

    public float atackCooldown = 4f;
    private float atackTimer;
    public float staminaRegenCooldown = 2f;
    private float staminaRegenTimer;

    public float blockChance = 30f;
    public float evadeChance = 20f;

    public bool canAtack = true;
    private bool canBlock = true;
    private bool canEvade = true;


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
        atackTimer = atackCooldown;
        staminaRegenTimer = staminaRegenCooldown;
    }

    void Update()
    {
        healthBar.SetHealth(currentHealth);
        staminaBar.SetStamina(currentStamina);

        HandleAtackCooldown();
        HandleStaminaRegen();
        HandleCombatAvailability();

        if (timer.timeRemaning <= 0)
        {
            DisableCombat();
        }
    }

    public void TakeDamage(int damage)
    {
        if (canEvade && Random.Range(0, 100) <= evadeChance)
        {
            Evade();
        }
        else if (canBlock && Random.Range(0, 100) <= blockChance)
        {
            Block();
        }
        else
        {
            currentHealth -= damage;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void HandleAtackCooldown()
    {
        if (atackTimer > 0 && animator.GetBool("attackIsDone"))
        {
            atackTimer -= Time.deltaTime;
            if (atackTimer <= 0)
            {
                Atac();
                atackTimer = atackCooldown;
                animator.SetBool("attackIsDone", false);
            }
        }
    }
    private void Atac()
    {
        if (canAtack)
        {
            int choseAtac = Random.Range(0,3);
            switch(choseAtac)
            {
                case 0:
                    animator.Play("Trump_Atak_Clik");
                    break;
                case 1:
                    animator.Play("Tramp_Atak_2Clik");
                    break;
                case 2:
                    animator.Play("Trump_Atak_hold");
                    break;
            }
        }

    }
    public void PlayerTakeDamage()
    {
            playerControler.TakeDamage(damage);
            currentStamina --;
    }

    private void HandleStaminaRegen()
    {
        if (currentStamina < maxStamina)
        {
            if (staminaRegenTimer > 0f)
            {
                staminaRegenTimer -= Time.deltaTime;
            }
            else
            {
                currentStamina ++;
                staminaRegenTimer = staminaRegenCooldown;

                if (currentStamina > 0)
                {
                    EnableCombat();
                }
            }
        }
    }
    private void Evade()
    {
        animator.Play("Trump_Evade");
        currentStamina --;
    }

    private void Block()
    {
        animator.Play("Trump_Blok_Start");
    }
    private void HandleCombatAvailability()
    {
        canAtack = currentStamina > 0;
        canBlock = currentStamina > 0;
        canEvade = currentStamina > 0;
    }
    private void DisableCombat()
    {
        canAtack = false;
        canBlock = false;
        canEvade = false;
    }

    private void EnableCombat()
    {
        canAtack = true;
        canBlock = true;
        canEvade = true;
    }

    private void AnimatorEvents(string eventName)
    {
        if(eventName == "AtackEnd")
        {
            animator.SetBool("attackIsDone", true);
        }
    }


    void Die()
    {
        gameOverScreen.Setup("Player");
        Destroy(gameObject);
    }
}
