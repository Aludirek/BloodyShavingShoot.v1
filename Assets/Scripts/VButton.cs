﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public bool value = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        value = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        value = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        value = false;
    }

}