using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class AttackPriority : MonoBehaviour
{
    public AtacButton atacButton;
    public EnemyController enemyController;
    private bool playerStartedAttack = false;
    private bool enemyStartedAttack = false;
    public float enemyAttackStartTimer = 0;
    public float playerAttackStartTimer = 0;
    
    void Update()
    {
        CountingAttackTime();
    }

    public void CountingAttackTime()
    {
        if((atacButton.isAttacClick || atacButton.isAttacHold || atacButton.isAttackDoubleClick) && !playerStartedAttack)
        {
            playerStartedAttack = true;
            playerAttackStartTimer = Time.time;
            Debug.Log("Player started attack at " + playerAttackStartTimer);
        }
        if(enemyController.isAttack && !enemyStartedAttack)
        {
            enemyStartedAttack = true;
            enemyAttackStartTimer = Time.time;
            Debug.Log("Enemy started attack at " + enemyAttackStartTimer);
        }
        if(playerStartedAttack && enemyStartedAttack)
        {
            if (playerAttackStartTimer < enemyAttackStartTimer)
            {
                Debug.Log("Player attacked first");
            }
            else
            {
                Debug.Log("Enemy attacked first");
            }
            ResetTimers();
        }
    }

    private void ResetTimers()
    {
        playerStartedAttack = false;
        enemyStartedAttack = false;
        playerAttackStartTimer = 0;
        enemyAttackStartTimer = 0;
    }
}
