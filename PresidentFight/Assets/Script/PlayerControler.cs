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
    public Button atacButton;
    public Button evadeButton;
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
    public bool atacDeer = false;
    public bool atacJump = false;
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
            atacButton.enabled = false;
            canBloc = false;
            evadeButton.enabled = false;
        }
        if(currentStamina < maxStamina)
            StaminaBarLogic();
        if (timer.timeRemaning <= 0)
        {
            atacButton.enabled = false;
            canBloc = false;
            evadeButton.enabled = false;
        }
    }

    public void Atac()
    {
        if (atacButton.enabled == true)
        {
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
                canTakeDamage = false;
            }
            else 
            {
                canTakeDamage = true;
            }
        }
        else
        {
            canTakeDamage = true;
        }
            
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
        }
    }
    public void IsDoingSomeOfAnimation(string animationName)
    {
        if(animationName == "Atac")
        {
            canBloc = false;
            evadeButton.enabled = false;
            atacButton.enabled = false;
        }
        else if(animationName == "Evade")
        {
            atacButton.enabled = false;
            canBloc = false;
            evadeButton.enabled = false;
        }
        else if (animationName == "Bloc")
        {
            atacButton.enabled = false;
            evadeButton.enabled = false;
        }
        else
        {
            atacButton.enabled = true;
            canBloc = true;
            evadeButton.enabled = true;
            atacDeer = false;
            atacJump = false;
        }
    }

    void Die()
    {
        gameOverScreen.Setup("Enemy");
        Destroy(gameObject);
    }
}
