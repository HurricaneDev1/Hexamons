using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hexamon", menuName = "Hexamon/Create a Hexamon")]
public class HexamonBased : ScriptableObject
{
    public Sprite looks;
    public string monName;
    public int attack;
    public int defense;
    public int intelligence;
    public int speed;
    public int health;

    public string type1;
    public string type2;

}
