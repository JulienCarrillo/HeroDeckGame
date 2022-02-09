using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drop : MonoBehaviour, IDropHandler,IPointerClickHandler
{
    public static bool PointerIsOnSlot = false;
    public static GameObject cardInSlot;
    public static Image slot;

  
    public void OnDrop(PointerEventData eventData)
    {
        slot = GetComponent<Image>(); //Je get Le slot
        cardInSlot = eventData.pointerDrag.gameObject; // Je get la card
        if (eventData.pointerDrag != null && slot.transform.childCount == 0)//
        {
            //Set slot as parent of card
            cardInSlot.transform.SetParent(slot.transform); //Je la met dans le slot parent 
            cardInSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(65f, -70f); //je met la carte au centre de son parent
        }
        else
        {
            PointerOutSlot();
        }
        
       
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.transform.name);

    }

    public void PointerOnSlot()
    {
        PointerIsOnSlot = true;
    }
    public void PointerOutSlot()
    {
        PointerIsOnSlot = false;
    }
}
