using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[RequireComponent(typeof(CanvasGroup))]
public class UIWidgetPanel : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private RectTransform _content;

    private CanvasGroup canvasGroup;


    public event Action OnPanelClickEvent;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        HideWidget();
    }
    public void ShowWidget()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        _content.gameObject.SetActive(true);
    }
    public void HideWidget()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        _content.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        OnPanelClickEvent?.Invoke();
    }
}

