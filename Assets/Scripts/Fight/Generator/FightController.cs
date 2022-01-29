using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;
public class FightController : MonoBehaviour
{
    public GameObject CardsContainer;
   


    // Methode qui sera appeler dans la coroutine, elle prendra en paramettre le scriptable object
    //CardData ansi que le prefab a instancier 
    public void CreateCardPrefab(CardData CardData)
    {
        //Importer un nouveau game object card 
        GameObject CardPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/Fight/Fight"+CardData.element.name+".prefab", typeof(GameObject)) as GameObject;

        //Instance d'un nouveau prefab, enfant de CardContainer ces values 
        var NewCard = Instantiate(CardPrefab);
        NewCard.transform.SetParent(CardsContainer.gameObject.transform);
        NewCard.transform.localScale = new Vector3(1f, 1f, 1f);
   
        //get inputfields 
        RawImage HeroImg = NewCard.transform.GetChild(0).gameObject.GetComponent<RawImage>();

        TextMeshProUGUI Range = NewCard.transform.GetChild(5).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        
        TextMeshProUGUI Move1 = NewCard.transform.GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        Image Move1Image = NewCard.transform.GetChild(1).gameObject.GetComponent<Image>();

        TextMeshProUGUI Move2 = NewCard.transform.GetChild(2).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        Image Move2Image = NewCard.transform.GetChild(2).gameObject.GetComponent<Image>();

        TextMeshProUGUI Move3 = NewCard.transform.GetChild(3).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        Image Move3Image = NewCard.transform.GetChild(3).gameObject.GetComponent<Image>();

        TextMeshProUGUI Move4 = NewCard.transform.GetChild(4).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        Image Move4Image = NewCard.transform.GetChild(4).gameObject.GetComponent<Image>();

        // Name of card
        TextMeshProUGUI CardName = NewCard.transform.GetChild(6).GetChild(5).gameObject.GetComponent<TextMeshProUGUI>();
        //Moves button
        Text Move1ButtonText = NewCard.transform.GetChild(6).GetChild(1).GetChild(0).gameObject.GetComponent<Text>();
        Text Move2ButtonText = NewCard.transform.GetChild(6).GetChild(2).GetChild(0).gameObject.GetComponent<Text>();
        Text Move3ButtonText = NewCard.transform.GetChild(6).GetChild(3).GetChild(0).gameObject.GetComponent<Text>();
        Text Move4ButtonText = NewCard.transform.GetChild(6).GetChild(4).GetChild(0).gameObject.GetComponent<Text>();


        //Set Name 
        CardName.text = CardData.heroName;

        //Set HealthBar 
        HealthBar healthBar = NewCard.transform.GetChild(7).gameObject.GetComponent<HealthBar>();
        healthBar.SetHealth((int)((CardData.current_life_point*100)/CardData.max_life_point));

        // Set Range 
        Range.text = CardData.speed.ToString();
        //TextMeshProUGUI Speed = NewCard.transform.GetChild(7).gameObject.GetComponent<TextMeshProUGUI>();
        //TextMeshProUGUI Amount = NewCard.transform.GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

        //Asign Values on inputfields
        if (CardData.move1.attack_point != 0) { 
            Move1.text = CardData.move1.attack_point.ToString();
            Move1Image.sprite = Resources.Load<Sprite>("RuneFire");
        }
        else { 
            Move1.text = CardData.move1.defence_point.ToString();
            Move1Image.sprite = Resources.Load<Sprite>("RuneHeal");
        }

        if (CardData.move2.attack_point == 0) {
            Move2.text = CardData.move2.defence_point.ToString();
            Move2Image.sprite = Resources.Load<Sprite>("RuneHeal");
        }
        else{
            Move2.text = CardData.move2.attack_point.ToString();
            Move2Image.sprite = Resources.Load<Sprite>("RuneFire");
            
        }

        if (CardData.move3.attack_point != 0) { 
            Move3.text = CardData.move3.attack_point.ToString();
            Move3Image.sprite = Resources.Load<Sprite>("RuneFire");

        }else { 
            Move3.text = CardData.move3.defence_point.ToString();
            Move3Image.sprite = Resources.Load<Sprite>("RuneHeal");
        }

        if (CardData.move4.attack_point != 0) { 
            Move4.text = CardData.move4.attack_point.ToString();
            Move4Image.sprite = Resources.Load<Sprite>("RuneFire");

        }else { 
            Move4.text = CardData.move4.defence_point.ToString();
            Move4Image.sprite = Resources.Load<Sprite>("RuneHeal");
        }

        Move1ButtonText.text = CardData.move1.name;
        Move2ButtonText.text = CardData.move2.name;
        Move3ButtonText.text = CardData.move3.name;
        Move4ButtonText.text = CardData.move4.name;

        //Move1Button.onClick = Move1Action(CardData.move1.name);

        StartCoroutine(GetImage(CardData.img, HeroImg));

        HeroController Hero = NewCard.GetComponent<HeroController>();

        //Ajout de cardData dans la carte via HerooController
        Hero.AddCardData(CardData);

    }
    IEnumerator GetImage(string url, RawImage HeroImg)
    {
        //gestion image 
        string heroSpriteUrl = url;
        UnityWebRequest heroimg = UnityWebRequestTexture.GetTexture(heroSpriteUrl);
        if (heroimg.isNetworkError || heroimg.isHttpError)
        {
            Debug.Log(heroimg.error);
        }
        yield return heroimg.SendWebRequest();
        HeroImg.texture = DownloadHandlerTexture.GetContent(heroimg);
    }

   //public void Move1Action(string Actionname)
   // {

   // }
}
