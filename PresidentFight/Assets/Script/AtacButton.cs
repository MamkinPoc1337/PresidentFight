using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AtacButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public PlayerControler playerControler;
    private  float atackTimer = 0f;
    private  int clickCount = 0;
    void Update()
    {
        if (atackTimer > 0f)
        {
            atackTimer -=Time.deltaTime;

            if (atackTimer <= 0f && clickCount == 1)
                SingleClick();
            else if(clickCount >= 2)
            {
                DoubleClick();
                atackTimer = 0f;
            }
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (playerControler.attackButton.enabled)
        {
            clickCount++;
            if (clickCount == 1)
            {
                atackTimer = 0.5f;
            }
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if(playerControler.attackButton.enabled && clickCount == 1 && atackTimer > 0f)
        {
            HoldClick();
            atackTimer = 0f;
        }
    }

    public void SingleClick()
    {
        if(playerControler.attackButton.enabled)
        {
            playerControler.animator.Play("Baidon_Attack_Deer");
            ResetClickCount();
        }
    }
    public void HoldClick()
    {
        if (playerControler.attackButton.enabled)
        {
            playerControler.animator.Play("Baiden_Attack_Jump");
            ResetClickCount();
        }
    }
    private void DoubleClick()
    {
        if (playerControler.attackButton.enabled)
        {
            //playerControler.animator.Play("Baiden_Attack_Double");
            ResetClickCount();
        }
    }

    private void ResetClickCount()
    {
        clickCount = 0;
    }
}
