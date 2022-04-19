using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMon: MonoBehaviour
{
    public SaveMon mon;
    public Move recievingMove;

    public void TakeDamage(int damage){
        int finalDamage = 0;
        if(recievingMove.isPhysical == true){
            finalDamage = damage - mon.defense;
        }else{
            finalDamage = damage - mon.intelligence;
        }
        if(finalDamage <= 0)finalDamage = 1;
        mon.currentHealth -= finalDamage;
    }
}
