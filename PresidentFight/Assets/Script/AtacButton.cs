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
    public void Update()
    {
        if (atackTimer > 0f)
        {
            atackTimer -=Time.deltaTime;
            if (atackTimer <= 0f)
                SingleClick();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(playerControler.attackButton.enabled == true)
            atackTimer = 0.5f;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if(playerControler.attackButton.enabled == true)
            if(atackTimer >=0f)
                HoldClick();
    }

    public void SingleClick()
    {
        if(playerControler.attackButton.enabled == true)
            playerControler.animator.Play("Baidon_Attack_Deer");
    }
    public void HoldClick()
    {
        playerControler.animator.Play("Baiden_Attack_Jump");
    }

}
