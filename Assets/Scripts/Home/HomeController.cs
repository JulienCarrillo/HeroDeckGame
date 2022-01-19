﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Linq;
using Newtonsoft.Json.Linq;


//Script de Gestion de Carte (Apparition de Prefab + Assignement des donnée aux prefabs 
public class HomeController : MonoBehaviour
{

    public GameObject CardsContainer;

    // Methode qui sera appeler dans la coroutine, elle prendra en paramettre le scriptable object
    //CardData ansi que le prefab a instancier 
    public void CreateCardPrefab(CardData CardData)
    {
        Debug.Log(CardData.element.elementName);
        //Importer un nouveau game object card 
        //Modifier la route en "Assets/Prefab/"+CardData.element.elementName + ".prefab"
        GameObject CardPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/CardPsy.prefab", typeof(GameObject)) as GameObject;
        //Assigner ces values 
        var NewCard = Instantiate(CardPrefab);
        NewCard.transform.SetParent(CardsContainer.gameObject.transform);
        NewCard.transform.localScale =new Vector3(1f,1f,1f);
        //get inputfields 
        RawImage HeroImg = NewCard.transform.GetChild(0).gameObject.GetComponent<RawImage>();
        TextMeshProUGUI Move1 = NewCard.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI Move2 = NewCard.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI Move3 = NewCard.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI Move4 = NewCard.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI Name = NewCard.transform.GetChild(5).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI HpField = NewCard.transform.GetChild(6).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI Speed = NewCard.transform.GetChild(7).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI Element = NewCard.transform.GetChild(8).gameObject.GetComponent<TextMeshProUGUI>();
        
        //Asign Values on inputfields
        Move1.text = CardData.move1.moveName;
        Move2.text = CardData.move2.moveName;
        Move3.text = CardData.move3.moveName;
        Move4.text = CardData.move4.moveName;
        Name.text = CardData.heroName;
        HpField.text = CardData.max_life_point.ToString();
        Speed.text = CardData.speed.ToString();
        Element.text = CardData.element.elementName;

        StartCoroutine(GetImage(CardData.img,HeroImg));

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
}
    
