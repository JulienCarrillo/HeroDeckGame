using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class LoginController : MonoBehaviour
{
    public class User
    {
        public int id;
        public string UserName;
        public string email;
        public string AccessToken;
    }

    [SerializeField]
    private GameObject EmailField;
    [SerializeField]
    private GameObject PasswordField;
   
    public GameObject loginButton; //public peut etre serializer

    //public TextMeshProUGUI result;

    //public GameObject ErrorTextUI;

    private readonly string baseLoginURL = "http://127.0.0.1:8000/api/v1/login";

    //Instance du scriptableobject UserData
    public UserData UserData;


    void Start()
    {
        Button btn = loginButton.GetComponent<Button>();
        btn.onClick.AddListener(Login);

        //hide error UI 
        //ErrorTextUI.SetActive(false);
    }
    // Update is called once per frame
    void Login()
    {
        string Email = EmailField.GetComponent<TMP_InputField>().text;
        string Password = PasswordField.GetComponent<TMP_InputField>().text;

        StartCoroutine(CallingAPIToLogin(Email, Password));
    }

    public User user = new User();

    IEnumerator CallingAPIToLogin(string Email,string Password)
    {
      
        Dictionary<string, string> wwwForm = new Dictionary<string, string>();
        wwwForm.Add("email", Email);
        wwwForm.Add("password", Password);

        UnityWebRequest uwr = UnityWebRequest.Post(baseLoginURL, wwwForm);

        yield return uwr.SendWebRequest();


        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
            //show error UI 
            //ErrorTextUI.SetActive(true);
        }
        else
        {
            //Parse du token (via Newtonsoft.Json.Linq https://github.com/jilleJr/Newtonsoft.Json-for-Unity)

            //Parse json 
            JObject json = JObject.Parse(uwr.downloadHandler.text);
           //Stocker le resultat dans le scriptable object UserData
            user.id = (int)json["user"]["id"];
            user.UserName = (string)json["user"]["name"];
            user.email = (string)json["user"]["email"];
            user.AccessToken = (string)json.GetValue("AccessToken");
            createUserGameObject(user, UserData);


            //Changer de scene 
            SceneManager.LoadScene("Home");
        }
        
    }

    
    void createUserGameObject(User user, UserData userInfo)
    {
        userInfo.id = user.id;
        userInfo.Username = user.UserName;
        userInfo.email = user.email;
        userInfo.AccessToken = user.AccessToken;
    }
}
