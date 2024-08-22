using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public PlayerControler playerControler;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(playerControler.blockButton.enabled == true)
        {
            // playerControler.animator.Play("Baiden_Block");
            playerControler.canTakeDamage = false;
            playerControler.isBlock = true;
        }

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if(playerControler.isBlock == true)
        {
            playerControler.animator.Play("Baiden_Block_End");
            playerControler.canTakeDamage = true;
        }
    }
}
