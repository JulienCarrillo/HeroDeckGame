using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationController : MonoBehaviour
{
   public void NavigateToShop()
    {
        SceneManager.LoadScene("Shop");
    }
    public void NavigateToHome()
    {
        SceneManager.LoadScene("Home");
    }
    public void NavigateToLogin()
    {
        SceneManager.LoadScene("Login");
    }
    public void NavigateToMyDeck()
    {
        SceneManager.LoadScene("MyDeck");
    }
    public void NavigateToFight()
    {
        SceneManager.LoadScene("Fight");
    }
}

