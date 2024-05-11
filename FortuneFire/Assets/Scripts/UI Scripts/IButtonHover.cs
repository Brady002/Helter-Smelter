using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject OnHoverShow;

    public void Start()
    {
        OnHoverShow.SetActive(false);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        OnHoverShow.SetActive(true);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        OnHoverShow.SetActive(false);
    }
}
