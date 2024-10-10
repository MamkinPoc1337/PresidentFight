using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public PlayerControler playerControler;
    public AtacButton atacButton;
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

    public float blockChance = 0.20f;
    public float evadeChance = 20f;
    private float blockDuration;

    public bool canAtack = true;
    private bool canBlock = true;
    private bool canEvade = true;
    private bool isBlocking = false;
    public bool isAttack = false;
    public bool isAttacHold = false;
    public bool isAttacDoubleClick = false;
    public bool isDie = false;
    private bool canTakeDamage = true;


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
        HandleBlockDuration();

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
        else if (canTakeDamage == true)
        {
            currentHealth -= damage;
            animator.SetTrigger("takeDamage");
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
                ChoseWhatToDo();
                atackTimer = atackCooldown;
            }
        }
    }

    private void ChoseWhatToDo()
    {
        if(Random.Range(0,100) <= blockChance && canBlock)
            Block();
        else    
            Atac();
    }
    private void Atac()
    {
        if (canAtack)
        {
            int choseAtac = Random.Range(0,3);
            isAttack = true;
            switch(choseAtac)
            {
                case 0:
                    animator.Play("Trump_Atak_Clik");
                    break;
                case 1:
                    animator.Play("Tramp_Atak_2Clik");
                    isAttacDoubleClick = true;
                    break;
                case 2:
                    animator.Play("Trump_Atak_hold");
                    isAttacHold = true;
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
            if (isBlocking || isAttack)
            {
                staminaRegenTimer -= Time.deltaTime;
            }
            else
            {
                staminaRegenTimer -= Time.deltaTime * 2; 
            }
            if (staminaRegenTimer <=0)
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

    private void HandleBlockDuration()
    {
        if(isBlocking)
        {
            if (blockDuration > 0f)
            {
                blockDuration -= Time.deltaTime;
                if(atacButton.isAttacHold)
                {
                    canTakeDamage = true;
                    animator.Play("Trump_Blok_End");
                }
                else
                {
                    canTakeDamage = false;
                }
            }
            else
            {
                animator.Play("Trump_Blok_End");
                isBlocking = false;
                canTakeDamage = true;
            }
        }
    }

    private void Block()
    {
        animator.Play("Trump_Blok_Start");
        isBlocking = true;
        blockDuration = Random.Range(1,4);
    }
    private void HandleCombatAvailability()
    {
        canAtack = currentStamina > 0 && !isDie;
        canBlock = currentStamina > 0 && !isDie;
        canEvade = currentStamina > 0 && !isDie;
    }
    private void DisableCombat()
    {
        canAtack = false;
        canBlock = false;
        canEvade = false;
    }

    private void EnableCombat()
    {
        if(!isDie)
        {
            canAtack = true;
            canBlock = true;
            canEvade = true;
        }
    }

    private void AnimatorEvents(string eventName)
    {
        if(eventName == "AtackEnd")
        {
            animator.SetTrigger("animationIsDone");
            isAttack = false;
            isAttacHold = false;
            isAttacDoubleClick = false;
        }
    }

    void Die()
    {
        isDie = true;
        isBlocking = false;
        DisableCombat();
        playerControler.Win();
        animator.Play("Tramp_loosing");
    }

    public void Win()
    {
        isDie = true;
        isBlocking = false;
        DisableCombat();
        animator.Play("Tramp_Win");
    }

    public void GameOver()
    {
        gameOverScreen.Setup("Player");
    }
}
