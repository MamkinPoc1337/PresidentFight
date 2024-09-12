using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public PlayerControler playerControler;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(playerControler.blockButton.enabled)
        {
            playerControler.animator.Play("Baiden_Block_Start");
            playerControler.canTakeDamage = false;
            playerControler.isBlocking  = true;
        }

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if(playerControler.isBlocking)
        {
            playerControler.animator.Play("Baiden_Block_End");
            playerControler.canTakeDamage = true;
            playerControler.isBlocking = false;
        }
    }
}
