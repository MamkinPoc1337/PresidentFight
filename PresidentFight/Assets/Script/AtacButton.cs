using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.EventSystems;

public class AtacButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public PlayerControler playerControler;
    public UnityEngine.Events.UnityEvent onSingleClick;
    public UnityEngine.Events.UnityEvent onDoubleClick;
    public UnityEngine.Events.UnityEvent onHoldClick;

    private float doubleClickThreshold = 0.3f; // Время для определения двойного клика
    private float holdThreshold = 0.25f; // Время для определения удержания
    private float lastClickTime = 0f;
    private bool isHeld = false;
    private bool isClickedOnce = false;
    private bool holdTriggered = false; 
    public bool isAttacClick = false;
    public bool isAttackDoubleClick = false;
    public bool isAttacHold = false;

    public void Update()
    {
        ResetPlayerState();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (playerControler != null && playerControler.attackButton.enabled)
        {

            if (!isClickedOnce)
            {
                isClickedOnce = true;
                lastClickTime = Time.time;
                Invoke("SingleClick", doubleClickThreshold);
                Invoke("HoldClick", holdThreshold);
            }
            else if (Time.time - lastClickTime < doubleClickThreshold)
            {
                // Если второй клик произошел до истечения таймера, отменяем одиночный клик и удержание
                CancelInvoke("SingleClick");
                CancelInvoke("HoldClick");
                OnDoubleClick();
                ResetClickState();
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (playerControler != null && playerControler.attackButton.enabled)
        {
            // Если кнопку отпустили и она была удержана, ничего не делаем
            if (isHeld)
            {
                isHeld = false;
                return;
            }
            CancelInvoke("HoldClick");
        }
    }

    private void SingleClick()
    {
        if (isClickedOnce && !holdTriggered)
        {
            onSingleClick?.Invoke();
            isAttacClick = true;
            ResetClickState();
        }
    }

    private void HoldClick()
    {
        if (isClickedOnce)
        {
            isHeld = true;
            holdTriggered = true;
            onHoldClick?.Invoke();
            isAttacHold = true;
            ResetClickState();
        }
    }

    private void OnDoubleClick()
    {
        onDoubleClick?.Invoke();
        isAttackDoubleClick = true;
    }

    private void ResetClickState()
    {
        isClickedOnce = false;
        isHeld = false;
        holdTriggered = false;
        CancelInvoke("SingleClick");
        CancelInvoke("HoldClick");

    }
    private void ResetPlayerState()
    {
        if(playerControler.isDoAnimation == false)
        {
            isAttacClick = false;
            isAttacHold = false;
            isAttackDoubleClick = false;
        }
    }
}