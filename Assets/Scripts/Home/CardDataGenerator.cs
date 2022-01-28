using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CardDataGenerator : MonoBehaviour
{
    public UserData UserData;
    private ElementData elementData;
    private ElementData weaknessData;
    private MoveData MoveData1;
    private MoveData MoveData2;
    private MoveData MoveData3;
    private MoveData MoveData4;
    private CardData CardData;
    public GameObject HomeControllerObject;


    // Start is called before the first frame update
    void Start()
    {

       
        string url = "http://127.0.0.1:8000/api/v1/user/" + UserData.id + "/heroes";
        StartCoroutine(CallingAPIToGetHeroes(url));

    }

    IEnumerator CallingAPIToGetHeroes(string url)
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
            string[] cardExistArray;
            string[] elementExistArray;
            string[] moveExistArray;
            string[] moveExist1Array;
            string[] moveExist2Array;
            string[] moveExist3Array;

            for (int i = 0; i <= json.Count - 1; i++)
            {
                
                CardData = ScriptableObject.CreateInstance<CardData>(); //ATTENTION NE FONCTIONNE PAS EN BUILD -> creer une class au lieu d'un scriptable object
                MoveData1 = ScriptableObject.CreateInstance<MoveData>();//ATTENTION NE FONCTIONNE PAS EN BUILD -> remplace by ressource.load 
                MoveData2 = ScriptableObject.CreateInstance<MoveData>();//ATTENTION NE FONCTIONNE PAS EN BUILD -> remplace by ressource.load 
                MoveData3 = ScriptableObject.CreateInstance<MoveData>();//ATTENTION NE FONCTIONNE PAS EN BUILD -> remplace by ressource.load 
                MoveData4 = ScriptableObject.CreateInstance<MoveData>();//ATTENTION NE FONCTIONNE PAS EN BUILD -> remplace by ressource.load 
                elementData = ScriptableObject.CreateInstance<ElementData>();//ATTENTION NE FONCTIONNE PAS EN BUILD -> remplace by ressource.load 
                weaknessData = ScriptableObject.CreateInstance<ElementData>();//ATTENTION NE FONCTIONNE PAS EN BUILD -> remplace by ressource.load 
                Debug.Log("loop" + i);
                //Check si la card n'existe pas déja
                cardExistArray = AssetDatabase.FindAssets("Card" + (string)json[i]["id"], new[] { "Assets/ScriptableObject/Card/CardData" }); //Ressource.load 
                List<string> cardExist = cardExistArray.ToList<string>();

                if (cardExist.Count() == 0)
                {
                    //Creer un nouveau SO CardData et lui assigner les valeurs reçu par l'API
                    CardData.name = (string)json[i]["name"];
                    //Stockage du nouveau card Data
                    CardData.id = (int)json[i]["id"];
                    CardData.userId = (int)json[i]["user_id"];
                    CardData.heroName = (string)json[i]["name"]; ;
                    CardData.img = (string)json[i]["img"]; ;
                    CardData.max_life_point = (float)json[i]["max_life_point"];
                    CardData.current_life_point = (float)json[i]["current_life_point"];
                    CardData.xp = (float)json[i]["xp"];
                    CardData.level = (int)json[i]["level"];
                    CardData.speed = (int)json[i]["speed"];

                    //Check si l'element n'existe pas 
                    elementExistArray = AssetDatabase.FindAssets((string)json[i]["element"]["name"], new[] { "Assets/ScriptableObject/Card/ElementData" }); //ressource.load
                    List<string> elementExist = elementExistArray.ToList<string>();
                    if (elementExist.Count() == 0)
                    {
                        elementData.name = (string)json[i]["element"]["name"];
                        elementData.elementName = (string)json[i]["element"]["name"];
                        elementData.weakness_id = (int)json[i]["element"]["weakness_id"];
                        AssetDatabase.CreateAsset(elementData, "Assets/ScriptableObject/Card/ElementData/" + elementData.name + ".asset");//ressource.load
                    }
                    else
                    {
                        var path = AssetDatabase.GUIDToAssetPath(elementExist[0]);
                        elementData = AssetDatabase.LoadAssetAtPath(path, typeof(ElementData)) as ElementData;

                    }
                    CardData.element = elementData;
                    elementExist.Clear();

                    //Check si le move1 n'existe pas déja
                    moveExistArray = AssetDatabase.FindAssets((string)json[i]["Move1"]["name"], new[] { "Assets/ScriptableObject/Card/MoveData" }); //ressource.load
                    List<string> moveExist = moveExistArray.ToList<string>();
                    if (moveExist.Count() == 0)
                    {
                        MoveData1.name = (string)json[i]["Move1"]["name"];
                        MoveData1.moveName = (string)json[i]["Move1"]["name"];
                        MoveData1.attack_point = (int)json[i]["Move1"]["attack_point"];
                        MoveData1.defence_point = (int)json[i]["Move1"]["defence_point"];
                        MoveData1.element = elementData;
                        if (!String.IsNullOrEmpty(((string)json[i]["Move1"]["min_dpt"])))
                        {
                            MoveData1.min_dpt = (int)json[i]["Move1"]["min_dpt"];
                            MoveData1.max_dpt = (int)json[i]["Move1"]["max_dpt"];
                        }
                        AssetDatabase.CreateAsset(MoveData1, "Assets/ScriptableObject/Card/MoveData/" + MoveData1.name + ".asset"); 
                    }
                    else
                    {
                        var path = AssetDatabase.GUIDToAssetPath(moveExist[0]);
                        MoveData1 = AssetDatabase.LoadAssetAtPath(path, typeof(MoveData)) as MoveData;
                    }
                    CardData.move1 = MoveData1;


                    //Check si le move2 n'existe pas déja
                    moveExist1Array = AssetDatabase.FindAssets((string)json[i]["Move1"]["name"], new[] { "Assets/ScriptableObject/Card/MoveData" });
                    List<string> moveExist1 = moveExist1Array.ToList<string>();
                    if (moveExist1.Count() == 0)
                    {
                        MoveData2.name = (string)json[i]["Move2"]["name"];
                        MoveData2.moveName = (string)json[i]["Move2"]["name"];
                        MoveData2.attack_point = (int)json[i]["Move2"]["attack_point"];
                        MoveData2.defence_point = (int)json[i]["Move2"]["defence_point"];
                        MoveData2.element = elementData;
                        if (!String.IsNullOrEmpty(((string)json[i]["Move2"]["min_dpt"])))
                        {
                            MoveData2.min_dpt = (int)json[i]["Move2"]["min_dpt"];
                            MoveData2.max_dpt = (int)json[i]["Move2"]["max_dpt"];
                        }
                        AssetDatabase.CreateAsset(MoveData2, "Assets/ScriptableObject/Card/MoveData/" + MoveData2.name + ".asset");
                    }
                    else
                    {
                        var path = AssetDatabase.GUIDToAssetPath(moveExist1[0]);
                        MoveData2 = AssetDatabase.LoadAssetAtPath(path, typeof(MoveData)) as MoveData;
                    }
                    CardData.move2 = MoveData2;


                    //Check si le Move3 n'existe pas déja
                    moveExist2Array = AssetDatabase.FindAssets((string)json[i]["Move3"]["name"], new[] { "Assets/ScriptableObject/Card/MoveData" });
                    List<string> moveExist2 = moveExist2Array.ToList<string>();
                    if (moveExist2.Count() == 0)
                    {
                        MoveData3.name = (string)json[i]["Move3"]["name"];
                        MoveData3.moveName = (string)json[i]["Move3"]["name"];
                        MoveData3.attack_point = (int)json[i]["Move3"]["attack_point"];
                        MoveData3.defence_point = (int)json[i]["Move3"]["defence_point"];
                        MoveData3.element = elementData;
                        if (!String.IsNullOrEmpty(((string)json[i]["Move3"]["min_dpt"])))
                        {
                            MoveData3.min_dpt = (int)json[i]["Move3"]["min_dpt"];
                            MoveData3.max_dpt = (int)json[i]["Move3"]["max_dpt"];
                        }
                        AssetDatabase.CreateAsset(MoveData3, "Assets/ScriptableObject/Card/MoveData/" + MoveData3.name + ".asset");
                    }
                    else
                    {
                        var path = AssetDatabase.GUIDToAssetPath(moveExist2[0]);
                        MoveData3 = AssetDatabase.LoadAssetAtPath(path, typeof(MoveData)) as MoveData;
                    }
                    CardData.move3 = MoveData3;

                    //Check si le Move4 n'existe pas déja
                    moveExist3Array = AssetDatabase.FindAssets((string)json[i]["Move4"]["name"], new[] { "Assets/ScriptableObject/Card/MoveData" });
                    List<string> moveExist3 = moveExist3Array.ToList<string>();
                    if (moveExist3.Count() == 0)
                    {
                        MoveData4.name = (string)json[i]["Move4"]["name"];
                        MoveData4.moveName = (string)json[i]["Move4"]["name"];
                        MoveData4.attack_point = (int)json[i]["Move4"]["attack_point"];
                        MoveData4.defence_point = (int)json[i]["Move4"]["defence_point"];
                        MoveData4.element = elementData;
                        if (!String.IsNullOrEmpty(((string)json[i]["Move4"]["min_dpt"])))
                        {
                            MoveData4.min_dpt = (int)json[i]["Move4"]["min_dpt"];
                            MoveData4.max_dpt = (int)json[i]["Move4"]["max_dpt"];
                        }
                        AssetDatabase.CreateAsset(MoveData4, "Assets/ScriptableObject/Card/MoveData/" + MoveData4.name + ".asset");
                    }
                    else
                    {
                        var path = AssetDatabase.GUIDToAssetPath(moveExist3[0]);
                        MoveData4 = AssetDatabase.LoadAssetAtPath(path, typeof(MoveData)) as MoveData;
                    }
                    CardData.move4 = MoveData4;

                    AssetDatabase.CreateAsset(CardData, "Assets/ScriptableObject/Card/CardData/" + CardData.name + ".asset");
                    
              
                }
                else
                {
                    var path = AssetDatabase.GUIDToAssetPath(cardExist[0]);
                    CardData = AssetDatabase.LoadAssetAtPath(path, typeof(CardData)) as CardData;
                }
             
                
                //Appel de la fonction CreateCard se trouvant dans la class CardController 
                HomeControllerObject.GetComponent<HomeController>().CreateCardPrefab(CardData);
            }
        }
    }
}
