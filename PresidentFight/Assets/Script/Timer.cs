using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timeText;
    public GameOverScreen gameOverScreen;
    public PlayerControler playerControler;
    public EnemyController enemyController;
    public float timeRemaning = 59;
    public bool timerIsRunning = false;
    void Start()
    {
        timerIsRunning = true;
    }

    void Update()
    {
        if(timerIsRunning)
        {
            if(timeRemaning >0)
            {
                timeRemaning -= Time.deltaTime;
                DisplayTime(timeRemaning);
            }
            else
            {
                if(playerControler.currentHealth < enemyController.currentHealth)
                    gameOverScreen.Setup("Time is end Enemy");
                else 
                    gameOverScreen.Setup("Time is end Player");
                Debug.Log("Time is over");
                timeRemaning = 0;
                timerIsRunning = false;
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}", seconds);
    }
}
