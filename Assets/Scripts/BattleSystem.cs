using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum BattleState{
    Start,
    Select,
    Player,
    Enemy,
    End
}

public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    public BattleMon enemy;
    public BattleMon player;
    public TextMeshProUGUI battleText;
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.Start;
        SelectMoves();
    }

    void SelectMoves(){
        state = BattleState.Select;
    }

    public void AttackClicked(){
        if(state != BattleState.Select)
            return;
        state = BattleState.Player;
        StartCoroutine(UseMove(player.mon.moves[0]));
    }
    IEnumerator UseMove(Move mo){
        for(int i = 0; i < mo.NumHits; i++){
            int hitChance = Random.Range(0,101);
            if(mo.Accuracy < hitChance){
                battleText.text = mo.MoveName + " Missed";
            }else{
                enemy.TakeDamage(mo.Damage);
                battleText.text = "Used " + mo.MoveName;
            }
            yield return new WaitForSeconds(0.3f);
        }
        if(enemy.mon.currentHealth <= 0)Debug.Log("Player Wins");
        state = BattleState.Enemy;
        StartCoroutine(EnemyAttack(enemy.mon.moves[0]));
    }

    IEnumerator EnemyAttack(Move mo){
        for(int i = 0; i < mo.NumHits; i++){
            int hitChance = Random.Range(0,101);
            if(mo.Accuracy < hitChance){
                battleText.text = mo.MoveName + " Missed";
            }else{
                player.TakeDamage(mo.Damage);
                battleText.text = "Used " + mo.MoveName;
            }
            yield return new WaitForSeconds(0.3f);
        }
        if(player.mon.currentHealth <= 0)Debug.Log("Enemy Wins");
        SelectMoves();
    }
    
}
