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

public class HomeControllerProd : MonoBehaviour
{

    public GameObject CardsContainer;
    public UserData UserData;

    // Start is called before the first frame update
    void Start()
    {
        //Call Api To Get UserId 
        string url = "http://127.0.0.1:8000/api/v1/user/" + UserData.id + "/heroes";
        StartCoroutine(CallingApiToGetCardDataOfUserId(url));
    }


    IEnumerator CallingApiToGetCardDataOfUserId(string url)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url);

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            JArray json = JArray.Parse(uwr.downloadHandler.text);

            for (int i = 0; i <= json.Count - 1; i++)
            {
                
                CardData CardDataTemp = Resources.Load("ScriptableObjects/Card/CardData/" + (string)json[i]["name"], typeof(CardData)) as CardData;
                MoveData MoveData1 = Resources.Load("ScriptableObjects/Card/MoveData/" + (string)json[i]["Move1"]["name"], typeof(MoveData)) as MoveData;
                MoveData MoveData2 = Resources.Load("ScriptableObjects/Card/MoveData/" + (string)json[i]["Move2"]["name"], typeof(MoveData)) as MoveData;
                MoveData MoveData3 = Resources.Load("ScriptableObjects/Card/MoveData/" + (string)json[i]["Move3"]["name"], typeof(MoveData)) as MoveData;
                MoveData MoveData4 = Resources.Load("ScriptableObjects/Card/MoveData/" + (string)json[i]["Move4"]["name"], typeof(MoveData)) as MoveData;
                ElementData elementData = Resources.Load("ScriptableObjects/Card/ElementData/" + (string)json[i]["element"]["name"], typeof(ElementData)) as ElementData;
                CardData CardData = new CardData();
                CardData = CardDataTemp;
                CardData.move1 = MoveData1;
                CardData.move2 = MoveData2;
                CardData.move3 = MoveData3;
                CardData.move4 = MoveData4;
                CardData.element = elementData;
                Debug.Log( CardData.move1.moveName);
                Debug.Log( CardData.move2.moveName);
                Debug.Log( CardData.move3.moveName);
                Debug.Log(CardData.move4.moveName);
                Debug.Log(CardData.heroName);
                CreateCardPrefab(CardData);
            }
        }
    }

    public void CreateCardPrefab(CardData CardData)
    {
        //Importer un nouveau game object card 
        GameObject CardPrefab = Resources.Load("Prefabs/Cards/HomeCards/Card" + CardData.element.elementName, typeof(GameObject)) as GameObject;
        //Local : GameObject CardPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/Cards/Card"+CardData.element.elementName+".prefab", typeof(GameObject)) as GameObject;

        //Instance d'un nouveau prefab, enfant de CardContainer ces values 
        var NewCard = Instantiate(CardPrefab);
        NewCard.transform.SetParent(CardsContainer.gameObject.transform);
        NewCard.transform.localScale = new Vector3(1f, 1f, 1f);
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
        TextMeshProUGUI Amount = NewCard.transform.GetChild(9).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

        //Asign Values on inputfields
        Move1.text = CardData.move1.moveName;
        Move2.text = CardData.move2.moveName;
        Move3.text = CardData.move3.moveName;
        Move4.text = CardData.move4.moveName;
        Name.text = CardData.heroName;
        HpField.text = CardData.max_life_point.ToString();
        Speed.text = CardData.speed.ToString();
        Element.text = CardData.element.elementName;
        Amount.text = CardData.price.ToString();

        StartCoroutine(GetImage(CardData.img, HeroImg));

        if (SceneManager.GetActiveScene().name == "Home")
        {
            NewCard.transform.GetChild(9).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
            NewCard.transform.GetChild(9).GetChild(1).gameObject.GetComponent<Image>().enabled = false;

        }

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

