using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpLogic : MonoBehaviour
{
    public GameObject PopUpPanel;
    public void PopUp()
    {
        HeroController.buttonIsClicked = false;
        if (Drop.PointerIsOnSlot)
        {
            PopUpPanel.SetActive(true);
        }
        
    }

    public void ClosePopUp()
    {
        //Debug.Log(HeroController.buttonIsClicked);
        PopUpPanel.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (HeroController.buttonIsClicked)
        {
            ClosePopUp();
        }
    }
}
