using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMon: Typings
{
    public SaveMon mon;
    public BattleSystem bat;
    [SerializeField]private Transform healthBar;
    [SerializeField]private bool isEnemy;
    public Hexamon hex;

    public float attackMod = 1;
    public float defenseMod = 1;
    public float intelligenceMod = 1;
    public float speedMod = 1;
    private int finalDamage;

    //Calculates typing and defense then deals damage accodingly
    public void TakeDamage(int damage, Move curMove){
        finalDamage = damage;
        if(curMove.isPhysical == true){
            finalDamage = finalDamage - (int)(mon.defense * defenseMod);
        }else{
            finalDamage = finalDamage - (int)(mon.intelligence * intelligenceMod);
        }
        StartCoroutine(TypeSetUp(curMove));
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

    IEnumerator TypeSetUp(Move curMove){
        int ogDamage = finalDamage;
        finalDamage = TypeCheck(mon,finalDamage,curMove);
        if(finalDamage == ogDamage/2 || finalDamage == ogDamage/4){
            bat.battleText.text = "The attack wasn't Very Effective";
        }else if(finalDamage == ogDamage * 2 || finalDamage == ogDamage * 4){
            bat.battleText.text = "The attack was Super Effective";
        }else if(finalDamage == 0){
            bat.battleText.text = "The attack did nothing";
        }
        yield return new WaitForSeconds(0.8f);
    }

    public void ClearStats(){
        attackMod = 1;
        speedMod = 1;
        intelligenceMod = 1;
        defenseMod = 1;
    }
}
