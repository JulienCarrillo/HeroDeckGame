using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroController : MonoBehaviour
{
    public HealthBar healthBar;
    public static bool buttonIsClicked;

  
    //On Click sur le move bouton du panel
    public void HeroMove(Text moveName) 
    {
        //CardData card = GetComponent<CardData>();
        MoveData move = AssetDatabase.LoadAssetAtPath("Assets/ScriptableObject/Card/MoveData/"+moveName.text+".asset", typeof(MoveData)) as MoveData;
        
        int moveRange = move.range;
        
        float heroMovePoint = 0;

        bool isAttacking = false;
        
        if(move.attack_point == 0)
            heroMovePoint = move.defence_point;
        else
        {
            heroMovePoint = move.attack_point;
            isAttacking = true;
        }


        if (isAttacking)
            Debug.Log("hero move : -" + heroMovePoint);
        else
            Debug.Log("hero move : +" + heroMovePoint);

        
        //Desactivation de tous les autre cartes pour pouvoir cliquer sur le slot a attacker
        GameObject card = transform.parent.parent.gameObject;
        //Get le CombatController et desactive les raycats des carte
        CombatController _CombatController = transform.root.gameObject.GetComponentInChildren<CombatController>();
        _CombatController.GetComponent<CombatController>().DisableRayCastOnCards(card);

        chooseEnemySlot(heroMovePoint, moveRange, isAttacking);
       
    }
   public void chooseEnemySlot(float movepoint , int range , bool attaking)
    {
        int[] outRowLeft = new int[] {5, 11, 17, 23, 29, 35, 41, 47 }; // => 
        int[] outRowRight = new int[] { 0, 6, 12, 18, 24, 30, 36, 42 }; // <= 
        int[] outColUp = new int[] { 0, 1, 2, 3, 4, 5};//^
        int[] outColDown = new int[] { 36, 37, 38, 39, 40, 41 };//^


        Canvas canvasSlot = gameObject.transform.parent.parent.parent.parent.GetComponentInParent<Canvas>();
        Image slot = gameObject.transform.parent.parent.parent.GetComponent<Image>();
        int thisIndex = slot.transform.GetSiblingIndex();
        canvasSlot.transform.GetChild(0).GetChild(thisIndex).GetSiblingIndex();

        ////Desactivation de tous les autre cartes pour pouvoir cliquer sur le slot a attacker
        //GameObject card = transform.parent.parent.gameObject;

        ////Get le CombatController
        //CombatController _CombatController = transform.root.gameObject.GetComponentInChildren<CombatController>();
        

        buttonOutClicked();

        ////enelve les interaction de toute les carte 
        //for (int i = 0; i <= 41; i++)
        //{
        //    canvasSlot.transform.GetChild(0).GetChild(i).GetComponent<CanvasGroup>().interactable = false;
        //}

        for (int i = thisIndex+1; i <= thisIndex+range; i++) // =>
        {
            canvasSlot.transform.GetChild(0).GetChild(i).GetComponent<CanvasGroup>().interactable = true;
            if (canvasSlot.transform.GetChild(0).GetChild(i).childCount == 0) 
            { 
                canvasSlot.transform.GetChild(0).GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("SlotGreen");
            }
            else 
            { 
                canvasSlot.transform.GetChild(0).GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("SlotRed");
            }
            if (outRowLeft.Contains(i) || outRowLeft.Contains(thisIndex))
                break;

        }
        for (int i = thisIndex-1; i >= thisIndex - range; i--) // <=
        {
            canvasSlot.transform.GetChild(0).GetChild(i).GetComponent<CanvasGroup>().interactable = true;

            if (canvasSlot.transform.GetChild(0).GetChild(i).childCount == 0)
                canvasSlot.transform.GetChild(0).GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("SlotGreen");
            else
                canvasSlot.transform.GetChild(0).GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("SlotRed");

            if (outRowRight.Contains(i) || outRowRight.Contains(thisIndex))
                break;

        }
        for (int i = thisIndex+6; i <= thisIndex + range * 6; i += 6) //v
        {
            canvasSlot.transform.GetChild(0).GetChild(i).GetComponent<CanvasGroup>().interactable = true;

            if (canvasSlot.transform.GetChild(0).GetChild(i).childCount == 0)
                canvasSlot.transform.GetChild(0).GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("SlotGreen");
            else
                canvasSlot.transform.GetChild(0).GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("SlotRed");

            if (outColDown.Contains(i)|| outColDown.Contains(thisIndex))
                break;

        }
        for (int i = thisIndex - 6; i >= thisIndex - range * 6; i -= 6)//^
        {
            canvasSlot.transform.GetChild(0).GetChild(i).GetComponent<CanvasGroup>().interactable = true;

            if (canvasSlot.transform.GetChild(0).GetChild(i).childCount == 0)
                canvasSlot.transform.GetChild(0).GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("SlotGreen");
            else
                canvasSlot.transform.GetChild(0).GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("SlotRed");
            if (outColUp.Contains(i) || outColUp.Contains(thisIndex))
                break;

        }
        
       

        
    }



    //bool who trigger popup
    public void buttonClicked()
    {
        buttonIsClicked = true;
    }
    public void buttonOutClicked()
    {
        buttonIsClicked = false;
    }
  


}
