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

    private bool canAtack = true;
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
        if (atackTimer > 0)
        {
            atackTimer -= Time.deltaTime;
            if (atackTimer <= 0)
            {
                Atac();
                atackTimer = atackCooldown;
            }
        }
    }
    private void Atac()
    {
        if (canAtack)
        {
            playerControler.TakeDamage(damage);
            currentStamina -= 1;
            animator.Play("Trump_Atak_Clik");

            if (currentStamina <= 0)
            {
                DisableCombat();
            }
        }

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
                currentStamina += 1;
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

    }

    private void Block()
    {

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


    void Die()
    {
        gameOverScreen.Setup("Player");
        Destroy(gameObject);
    }
}
