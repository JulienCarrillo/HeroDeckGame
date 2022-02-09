using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HeroController : MonoBehaviour
{
    public HealthBar healthBar;
    public static bool buttonIsClicked;

  

    public void HeroMove(Text moveName) //La fonction est instaintier avant la recuperation du cardData
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

        chooseEnemySlot(heroMovePoint, moveRange, isAttacking);
       
    }
   public void chooseEnemySlot(float movepoint , int range , bool attaking)
    {
        int[] outRowLeft = new int[] {5, 11, 17, 23, 29, 35, 41, 47 }; // => 
        int[] outRowRight = new int[] { 0, 6, 12, 18, 24, 30, 36, 42 }; // <= 
        int[] outColUp = new int[] { 0, 1, 2, 3, 4, 5};//^
        int[] outColDown = new int[] { 42, 43, 44, 45, 46, 47 };//^


        Canvas canvasSlot = gameObject.transform.parent.parent.parent.parent.GetComponentInParent<Canvas>();
        Image slot = gameObject.transform.parent.parent.parent.GetComponent<Image>();
        int thisIndex = slot.transform.GetSiblingIndex();
        canvasSlot.transform.GetChild(0).GetChild(thisIndex).GetSiblingIndex();
        //Debug.Log(canvasSlot.name);
        //Desactivation de toues les autre cartes
        buttonOutClicked();


        for (int i = thisIndex+1; i <= thisIndex+range; i++) // =>
        {
            if (canvasSlot.transform.GetChild(0).GetChild(i).childCount == 0)
                canvasSlot.transform.GetChild(0).GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("SlotGreen");
            else
                canvasSlot.transform.GetChild(0).GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("SlotRed");
            if (outRowLeft.Contains(i) || outRowLeft.Contains(thisIndex))
                break;

        }
        for (int i = thisIndex-1; i >= thisIndex - range; i--) // <=
        {
            if (canvasSlot.transform.GetChild(0).GetChild(i).childCount == 0)
                canvasSlot.transform.GetChild(0).GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("SlotGreen");
            else
                canvasSlot.transform.GetChild(0).GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("SlotRed");

            if (outRowRight.Contains(i) || outRowRight.Contains(thisIndex))
                break;

        }
        for (int i = thisIndex+6; i <= thisIndex + range * 6; i += 6) //v
        {
            if (canvasSlot.transform.GetChild(0).GetChild(i).childCount == 0)
                canvasSlot.transform.GetChild(0).GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("SlotGreen");
            else
                canvasSlot.transform.GetChild(0).GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("SlotRed");

            if (outColDown.Contains(i)|| outColDown.Contains(thisIndex))
                break;

        }
        for (int i = thisIndex - 6; i >= thisIndex - range * 6; i -= 6)//^
        {
            if (canvasSlot.transform.GetChild(0).GetChild(i).childCount == 0)
                canvasSlot.transform.GetChild(0).GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("SlotGreen");
            else
                canvasSlot.transform.GetChild(0).GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("SlotRed");
            if (outColUp.Contains(i) || outColUp.Contains(thisIndex))
                break;

        }

    }

    //bool how trigger popup
    public void buttonClicked()
    {
        buttonIsClicked = true;
    }
    public void buttonOutClicked()
    {
        buttonIsClicked = false;
    }
}
