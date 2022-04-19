using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMon: Typings
{
    public SaveMon mon;
    public BattleSystem bat;

    public void TakeDamage(int damage){
        int finalDamage = TypeCheck(mon,damage,bat.selectedMove);
        if(bat.selectedMove.isPhysical == true){
            finalDamage = finalDamage - mon.defense;
        }else{
            finalDamage = finalDamage - mon.intelligence;
        }
        if(finalDamage < 0)finalDamage = 0;
        mon.currentHealth -= finalDamage;
    }
}
