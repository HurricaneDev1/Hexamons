using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMon: Typings
{
    public SaveMon mon;
    public BattleSystem bat;
    [SerializeField]private Transform healthBar;

    public float attackMod = 1;
    public float defenseMod = 1;
    public float intelligenceMod = 1;
    public float speedMod = 1;

    public void TakeDamage(int damage){
        int finalDamage = TypeCheck(mon,damage,bat.selectedMove);
        if(bat.selectedMove.isPhysical == true){
            finalDamage = finalDamage - (int)(mon.defense * defenseMod);
        }else{
            finalDamage = finalDamage - (int)(mon.intelligence * intelligenceMod);
        }
        if(finalDamage < 0)finalDamage = 0;
        mon.currentHealth -= finalDamage;
        CheckDeath();
        SetSize();
    }

    void SetSize(){
        float monMax = mon.maxHealth;
        float monCur = mon.currentHealth;
        float finalSize = monCur/monMax * 0.85f;
        healthBar.localScale = new Vector3(finalSize,healthBar.localScale.y);
    }

    void CheckDeath(){
        if(mon.currentHealth <= 0){
            bat.state = BattleState.EnemyDead;
            mon.currentHealth = 0;
        }
    }
}