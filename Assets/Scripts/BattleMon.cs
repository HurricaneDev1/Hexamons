using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMon: MonoBehaviour
{
    public SaveMon mon;

    public void TakeDamage(int damage){
        int finalDamage = (damage - mon.defense);
        if(finalDamage <= 0)finalDamage = 1;
        mon.currentHealth -= finalDamage;
    }
}
