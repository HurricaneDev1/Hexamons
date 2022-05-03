using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMon: Typings
{
    public SaveMon mon;
    public BattleSystem bat;
    [SerializeField]private Transform healthBar;
    [SerializeField]private bool isEnemy;

    public float attackMod = 1;
    public float defenseMod = 1;
    public float intelligenceMod = 1;
    public float speedMod = 1;

    //Calculates typing and defense then deals damage accodingly
    public void TakeDamage(int damage){
        int finalDamage = TypeCheck(mon,damage,bat.selectedMove);
        if(bat.selectedMove.isPhysical == true){
            finalDamage = finalDamage - (int)(mon.defense * defenseMod);
        }else{
            finalDamage = finalDamage - (int)(mon.intelligence * intelligenceMod);
        }
        if(finalDamage < 0)finalDamage = 1;
        mon.currentHealth -= finalDamage;
        CheckDeath();
        if(isEnemy == false){
            bat.healthPercentageText.text = mon.currentHealth.ToString() + "/" + mon.maxHealth;
        }
        SetSize();
    }

    //Adjusts the size of the health bar
    public void SetSize(){
        float monMax = mon.maxHealth;
        float monCur = mon.currentHealth;
        float finalSize = monCur/monMax * 0.85f;
        healthBar.localScale = new Vector3(finalSize,healthBar.localScale.y);
    }

    //Sees if the mon is dead
    void CheckDeath(){
        if(mon.currentHealth <= 0){
            if(isEnemy){
                bat.state = BattleState.EnemyDead;
            }else{
                bat.state = BattleState.PlayerDead;
            }
            mon.currentHealth = 0;
        }
    }
}
