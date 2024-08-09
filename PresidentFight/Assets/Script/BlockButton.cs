using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public PlayerControler playerControler;
    public bool buttonPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(playerControler.canBloc == true)
        {
            playerControler.animator.Play("Baiden_Blok");
            buttonPressed = true;
            Debug.Log("1");
        }

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        playerControler.animator.Play("Baiden_Blok_end");
        buttonPressed = false;
        Debug.Log("2");
    }
}
