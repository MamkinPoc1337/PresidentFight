using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public PlayerControler playerControler;
    public EnemyController enemyController;

    public void Update()
    {
        HoldAttackChecker();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(playerControler.blockButton.enabled)
        {
            playerControler.animator.SetBool("IsBlock",true);
            playerControler.canTakeDamage = false;
            playerControler.isBlocking  = true;
        }

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if(playerControler.isBlocking)
        {
            playerControler.animator.SetBool("IsBlock",false);
            playerControler.canTakeDamage = true;
            playerControler.isBlocking = false;
        }
    }
    public void HoldAttackChecker()
    {
        if(enemyController.isAttacHold && playerControler.isBlocking)
        {
            playerControler.animator.SetBool("IsBlock",false);
            playerControler.canTakeDamage = true;
            playerControler.isBlocking = false;
        }
    }
}
