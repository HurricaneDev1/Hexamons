using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TransferStats : MonoBehaviour
{
    private StatsGenerator stats;
    public HexamonBased character;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)){
            Debug.Log("Print");
            // HexamonBased character = ScriptableObject.CreateInstance<HexamonBased>();
            character.name = stats.monName;
            character.attack = stats.Attack;
            character.defense = stats.Defense;
            character.intelligence = stats.Intelligence;
            character.speed = stats.Speed;
            character.health = stats.Health;
        }
    }
}
