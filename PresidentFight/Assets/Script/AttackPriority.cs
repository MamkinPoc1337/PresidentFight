using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPriority : MonoBehaviour
{
    public AtacButton atacButton;
    public EnemyController enemyController;
    public float enemyAttackStartTimer = 0;
    public float playerAttackStartTimer = 0;
    
    void Update()
    {
        CountingAttackTime();
    }

    public void CountingAttackTime()
    {
        if(atacButton.isAttacClick == true || atacButton.isAttacHold == true || atacButton.isAttackDoubleClick == true)
        {
            playerAttackStartTimer += Time.deltaTime;
        }
        if(enemyController.isAttack == true)
        {
            enemyAttackStartTimer += Time.deltaTime;
        }
        if(playerAttackStartTimer < enemyAttackStartTimer)
        {
            Debug.Log("Enemy Start first");
        }
        else
            Debug.Log("Player Sart First");
    }
}
