using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveData")]
public class MoveData : ScriptableObject
{
    public string moveName;
    public int attack_point;
    public int defence_point;
    public ElementData element;
    public int min_dpt;
    public int max_dpt;
    public int range;
}
