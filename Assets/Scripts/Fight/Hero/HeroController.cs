using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HeroController : MonoBehaviour
{
    public HealthBar healthBar;
    public CardData cardData;


    private void Start()
    {
        
    }
    public void AddCardData(CardData data)
    {
        cardData = AssetDatabase.LoadAssetAtPath("Assets/ScriptableObject/Card/CardData/" + data.name + ".asset", typeof(CardData)) as CardData; // ATTENTION CHANGER EN RESSOURCE.Load 
    }

    public void HeroMove(string move) //La fonction est instaintier avant la recuperation du cardData
    {
        Debug.Log(cardData.move1.moveName);
        
        float heroMove = 0;
        
        switch (move)
        {
            case "Move1":
                string moveName = cardData.move1.moveName;
                if (cardData.move1.attack_point == 0)
                {
                     heroMove = cardData.move1.defence_point;
                }
                else
                {
                     heroMove = cardData.move1.attack_point;
                }
                Debug.Log(moveName + " : " + heroMove.ToString());
                
                break;



            default:
                Debug.Log   ("Nothing");
            break;
        }
        //1) recup l'attaque 
        //2) recup la range
        //3) changer la couleur des slots interactable 

    }
}
