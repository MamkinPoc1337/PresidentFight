using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.EventSystems;

public class AtacButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public PlayerControler playerControler;
    private float doubleClickThreshold = 0.5f; // Время для определения двойного клика
    private float holdThreshold = 0.75f; // Время для определения удержания
    private int clickCount = 0;
    private bool isHeld = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (playerControler.attackButton.enabled)
        {
            clickCount++;

            if (clickCount == 1)
            {
                // Если это первый клик, устанавливаем таймеры для одиночного клика и удержания
                Invoke("SingleClick", doubleClickThreshold);
                Invoke("HoldClick", holdThreshold);
            }
            else if (clickCount == 2)
            {
                // Если второй клик произошел до истечения таймера, отменяем одиночный клик и удержание
                CancelInvoke("SingleClick");
                CancelInvoke("HoldClick");
                OnDoubleClick();
                ResetClickCount();
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (playerControler.attackButton.enabled)
        {
            // Если кнопку отпустили и она была удержана, ничего не делаем
            if (isHeld)
            {
                isHeld = false;
                return;
            }

            // Если кнопку отпустили до удержания и это был первый клик, отменяем удержание
            CancelInvoke("HoldClick");
            ResetClickCount();
        }
    }

    private void SingleClick()
    {
        if (playerControler.attackButton.enabled && clickCount == 1)
        {
            playerControler.animator.Play("Baidon_Attack_Deer");
            ResetClickCount();
        }
    }

    private void HoldClick()
    {
        if (playerControler.attackButton.enabled && clickCount == 1)
        {
            isHeld = true;
            playerControler.animator.Play("Baiden_Attack_Holdd");
            ResetClickCount();
        }
    }

    private void OnDoubleClick()
    {
        if (playerControler.attackButton.enabled)
        {
            playerControler.animator.Play("Baiden_Attack_Jump");
        }
    }

    private void ResetClickCount()
    {
        clickCount = 0;
        isHeld = false;
        CancelInvoke(); // Отменяем все запланированные вызовы
    }
}