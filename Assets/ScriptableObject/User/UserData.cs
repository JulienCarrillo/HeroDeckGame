using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UserData")]
public class UserData : ScriptableObject
{
    public int id;
    public string Username;
    public string email;
    public string AccessToken;
}
