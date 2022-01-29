using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    RectTransform rectransform;
    Canvas  canvas;
    CanvasGroup canvasGroup;
    //get la position initial de la carte 
    Vector2 initialPos;
    //Get La card pour changer son parent 
    GameObject card;
    Image cardContainer;
    private void Start()
    {
        rectransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPos = rectransform.anchoredPosition;
        cardContainer = GetComponentInParent<Image>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Pose : " + rectransform.anchoredPosition);
        rectransform.anchoredPosition += eventData.delta/canvas.scaleFactor; // delta : vector2 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(cardContainer.name);
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        card = eventData.pointerDrag.gameObject;
        if (!Drop.PointerIsOnSlot) 
        {
            card.transform.SetParent(cardContainer.transform);
            rectransform.anchoredPosition = initialPos;
        }

    }

}
