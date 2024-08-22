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

        //UpdateAnimClipTimes();
    }

    public void Atac()
    {
        enemyController.TakeDamage(damage);
        currentStamina -=1;
    }
    public void Evade(float isEvading)
    {
        if(evadeButton.enabled && isEvading == 1)
            canTakeDamage = false;
        else
            canTakeDamage = true;
    }

    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
            currentHealth -= damage;
        else
            currentStamina --;
        if (currentHealth <= 0)
            Die();
    }
    void HandleStamina()
    {
        if (currentStamina < maxStamina)
        {
            if (staminaRegenTimer > 0f)
            {
                staminaRegenTimer -= Time.deltaTime;
            }
            else
            {
                currentStamina++;
                staminaRegenTimer = staminaRegenTime;
            }
        }
    }

    void UpdateButtonStates()
    {
        bool hasStamina = currentStamina > 0;
        attackButton.enabled = hasStamina;
        evadeButton.enabled = hasStamina;
        blockButton.enabled = hasStamina && !isBlocking;
    }

    void DisableAllActions()
    {
        attackButton.enabled = false;
        evadeButton.enabled = false;
        blockButton.enabled = false;
    }

    public void IsDoingSomeOfAnimation(string animationName)
    {
        if(animationName == "DoAnimation")
            DisableAllActions();
        else
        {
            UpdateButtonStates();
            isBlocking  = false;
        }
    }

    void Die()
    {
        gameOverScreen.Setup("Enemy");
        Destroy(gameObject);
    }

    // public void UpdateAnimClipTimes()
    // {
    //     AnimatorClipInfo[ ] animationClip = animator.GetCurrentAnimatorClipInfo(0);
    //     if(animationClip.Length > 0)
    //     {
    //         int currentFrame = (int) (animationClip[0].weight * (animationClip [0].clip.length * animationClip[0].clip.frameRate));
    //         Debug.Log("CurrentFrame " + currentFrame);
    //     }
    // }
}
