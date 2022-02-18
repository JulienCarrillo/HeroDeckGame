using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CombatController : MonoBehaviour
{

    public GameObject CardContainer;
    public GameObject EnemyCardContainer;
    public GameObject SlotCanvas; 
    public  List<GameObject> Playercards;
    public  List<GameObject> Enemycards;
    private bool IsPlayerTurn;
    public GameObject PopUpIntro;
    public Button PlayerFinishPlacement;
    public Button EnemyFinishPlacement;
    public GameObject PopUpError;
    public GameObject PopUpEnemyIntro;
    public static bool PlayerFinishMove;
    public GameObject PopUpEndGame;


    public void GeneratePlayerCardList(GameObject card)
    {
        Playercards.Add(card);
    }

    public void GenerateEnemyCardList(GameObject card)
    {
        Enemycards.Add(card);
    }

    // Start is called before the first frame update
    void Start()
    {
       
        PopUpIntro.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(PlayerOnePlacement);
        PlayerFinishPlacement.onClick.AddListener(CheckPalyerPlacement);
        PopUpEnemyIntro.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(PlayerTwoPlacement);
        EnemyFinishPlacement.onClick.AddListener(CheckEnemeyPlacement);
        IsPlayerTurn = true;
        PlayerFinishMove = false;
    }



    public void DisableCards(List<GameObject> cards)
    {
        foreach (GameObject card in cards)
        {
            card.GetComponent<CanvasGroup>().interactable = false;
            card.transform.GetChild(0).GetComponent<RawImage>().color = new Color32(255, 255, 225, 90);
        }
    }
    public void EnableCards(List<GameObject> cards)
    {
        foreach (GameObject card in cards)
        {
            card.GetComponent<CanvasGroup>().interactable = true;
            card.transform.GetChild(0).GetComponent<RawImage>().color = new Color32(255, 255, 225, 255);
        }
    }

    public void EnableMove(List<GameObject> cards)
    {
        foreach (GameObject card in cards)
        {
            card.transform.GetChild(0).GetComponent<Button>().interactable = true;
        }
    }
    public void DisableMove(List<GameObject> cards)
    {
        foreach (GameObject card in cards)
        {
            card.transform.GetChild(0).GetComponent<Button>().interactable = false;
        }
    }

    public void DisableRayCastOnCards(GameObject ExeptCard)
    {
        Debug.Log("DissablingRayCast");
        foreach (GameObject card in Playercards)
        {
            if(card != ExeptCard) { 
                card.GetComponent<CanvasGroup>().blocksRaycasts = false;
                card.transform.GetChild(0).GetComponent<RawImage>().color = new Color32(255, 255, 225, 90);
            }
        }
        foreach (GameObject card in Enemycards)
        {
            if (card != ExeptCard) { 
                card.GetComponent<CanvasGroup>().blocksRaycasts = false;
                card.transform.GetChild(0).GetComponent<RawImage>().color = new Color32(255, 255, 225, 90);
            }
        }
    }
    public void EnableRayCastOnCards(List<GameObject> cards)
    {
        Debug.Log("EnablingRayCast");
        foreach (GameObject card in cards)
        {
            
                card.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }

    public void PlayerOnePlacement()
    {
        DisableMove(Playercards);
        DisableCards(Enemycards);
        PopUpIntro.SetActive(false);
        PlayerFinishPlacement.gameObject.SetActive(true);
    }

    public void CheckPalyerPlacement()
    {
        Debug.Log("Ok");
        if (CardContainer.transform.childCount == 0)
        {
            PlayerFinishPlacement.gameObject.SetActive(false);

            DisableCards(Playercards);

            PopUpEnemyIntro.SetActive(true);
        }
        else
        {
            PopUpError.gameObject.SetActive(true);
        }
    }
    public void PlayerTwoPlacement()
    {
        DisableMove(Enemycards);
        EnableCards(Enemycards);
        PopUpEnemyIntro.SetActive(false);
        EnemyFinishPlacement.gameObject.SetActive(true);
    }
   
    public void CheckEnemeyPlacement()
    {
        if (EnemyCardContainer.transform.childCount == 0)
        {
            EnemyFinishPlacement.gameObject.SetActive(false);

            DisableCards(Enemycards);
            //FightSystem();
            PlayerTurn();
        }
        else
        {
            PopUpError.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerFinishMove) {
            checkDeath(Playercards,"Player");
            checkDeath(Enemycards, "Enemy");
            for (int i = 0; i <= 41; i++)
            {
                SlotCanvas.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("Slot");
            }
            if (IsPlayerTurn)
                EnemyTurn();
            else
                PlayerTurn();
        }
    }

   
    public void checkDeath(List<GameObject> Listcards, string Player)
    {
        foreach (GameObject card in Listcards)
        {
            if (card.transform.GetChild(7).GetComponent<Slider>().value <= 0)
            {
                Listcards.Remove(card);
                Destroy(card);
            }
        }
        if (Listcards.Count <= 0)
        {
            EndGame(Player);
        }
    }

    public void EndGame(string PlayerName)
    {
        PopUpEndGame.transform.GetChild(0).GetComponent<Text>().text = "Congratulation, " + PlayerName + " Win the battle ! ";
        PopUpEndGame.transform.gameObject.SetActive(true);

    }
    public void PlayerTurn()
    {
        IsPlayerTurn = true;
        PlayerFinishMove = false;
        Debug.Log("PalyerTurn");
        DisableCards(Enemycards);
        EnableCards(Playercards);
        EnableMove(Playercards);
        EnableRayCastOnCards(Playercards);
    }

    
    public void EnemyTurn()
    {
        IsPlayerTurn = false;
        PlayerFinishMove = false;
        Debug.Log("EnemyTurn");
        DisableCards(Playercards);
        EnableCards(Enemycards);
        EnableMove(Enemycards);
        EnableRayCastOnCards(Enemycards);
    }

   
    
}
