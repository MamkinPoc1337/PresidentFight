using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class AtacButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public PlayerControler playerControler;
    public float atackTimer = 0f;
    public float clilCount = 0f;
    public void Update()
    {
        if (atackTimer > 0f)
        {
            atackTimer -=Time.deltaTime;
            if (atackTimer <= 0f)
                SingleClick();
            else if(clilCount == 2 & atackTimer >=0)
                DoubleClick();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(playerControler.attackButton.enabled == true)
            atackTimer = 0.5f;
        clilCount +=1;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if(playerControler.attackButton.enabled == true)
            if(atackTimer >0f)
                HoldClick();
    }

    public void SingleClick()
    {
        if(playerControler.attackButton.enabled == true)
        {
            playerControler.animator.Play("Baidon_Attack_Deer");
            clilCount = 0;
        }
    }
    public void HoldClick()
    {
        playerControler.animator.Play("Baiden_Attack_Jump");
        clilCount = 0;
    }
    public void DoubleClick()
    {
        Debug.Log("DoubleClick");
        clilCount = 0;
    }

}
