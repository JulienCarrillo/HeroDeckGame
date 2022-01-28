using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : ScriptableObject
{
    public int id;
    public int userId;
    public string heroName;
    public string img;
    public float max_life_point;
    public float current_life_point;
    public float xp;
    public int level;
    public int speed;
    public ElementData element;
    public MoveData move1;
    public MoveData move2;
    public MoveData move3;
    public MoveData move4;
    public float price;
}
