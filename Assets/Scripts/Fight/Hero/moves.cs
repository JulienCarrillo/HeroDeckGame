using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moves : MonoBehaviour
{
    public GameObject MovesPanel;
    public void PopUp()
    {
        MovesPanel.SetActive(true);
    }

    public void ClosePopUp()
    {
        MovesPanel.SetActive(false);
    }
}
