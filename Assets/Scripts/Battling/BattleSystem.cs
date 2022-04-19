using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum BattleState{
    Start,
    SelectAction,
    SelectMove,
    SwapMon,
    Player,
    Enemy,
    Wait
}

public class BattleSystem : MonoBehaviour
{
    [Header("Objects")]
    public BattleState state;
    public BattleMon enemy;
    public BattleMon player;
    [Header("Text")]
    [SerializeField]private TextMeshProUGUI battleText;
    [SerializeField]private List<TextMeshProUGUI> info = new List<TextMeshProUGUI>();
    [SerializeField]private List<TextMeshProUGUI> actionText = new List<TextMeshProUGUI>();
    [SerializeField]private List<TextMeshProUGUI> moveTexts = new List<TextMeshProUGUI>();
    [Header("Action/Move Stuff")]
    [SerializeField]private int actionNum = 0;
    [SerializeField]private int currentMove = 0;
    [SerializeField]private Move selectedMove;
    [SerializeField]private Color highlight;
    [SerializeField]private bool playerFirst;
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.Start;
    }
    void Update(){
        switch(state){
            case BattleState.Start:
                SetUpActions();
                break;
            case BattleState.SelectAction:
                SelectAction();
                if(Input.GetKeyDown(KeyCode.Z)){
                    if(actionNum == 0)
                        SetUpMoves(player.mon.moves);
                    if(actionNum == 1)
                        state = BattleState.SwapMon;
                }
                break;
            case BattleState.SelectMove:    
                MoveSelection(player.mon.moves);
                if(Input.GetKeyDown(KeyCode.Z)){
                    SpeedCheck();
                    ClearMoveText();
                }
                break;
            case BattleState.Player:
                StartCoroutine(PlayerAttack());
                break;
            case BattleState.Enemy:
                state = BattleState.SelectAction;
                break;
        }
    }
    void SetUpActions(){
        battleText.enabled = false;
        foreach(TextMeshProUGUI g in actionText){
            g.enabled = true;
        }
        state = BattleState.SelectAction;
    }

    void SelectAction(){
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            if(actionNum > 0)
                actionNum --;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            if(actionNum < actionText.Count - 1)
                actionNum ++;
        }
        foreach(TextMeshProUGUI g in actionText){
            g.color = Color.black;   
        }
        actionText[actionNum].color = highlight;
    }
    void SetUpMoves(List<Move> mo){
        state = BattleState.SelectMove;
        for(int i = 0; i < moveTexts.Count; i++){
            moveTexts[i].enabled = true;
            moveTexts[i].text = mo[i].MoveName;
        }foreach(TextMeshProUGUI g in actionText){
            g.enabled = false;
        }
    }
    void MoveSelection(List<Move> moves){
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            if(currentMove != 0)
                currentMove --;
        }else if(Input.GetKeyDown(KeyCode.DownArrow)){
            if(currentMove != 3)
                currentMove ++;
        }else if(Input.GetKeyDown(KeyCode.RightArrow)){
            if(currentMove != 2 && currentMove != 3)
                currentMove += 2;
        }else if(Input.GetKeyDown(KeyCode.LeftArrow)){
            if(currentMove != 0 && currentMove != 1)
                currentMove -= 2;
        }
        foreach(TextMeshProUGUI g in moveTexts){
            g.color = Color.black;   
        }
        moveTexts[currentMove].color = highlight;
        selectedMove = moves[currentMove];
        info[0].text = selectedMove.Type;
        info[1].text = selectedMove.Accuracy + "%";
        info[2].text = "Pow: " + selectedMove.Damage;
    }
    void SpeedCheck(){
        if(player.mon.speed > enemy.mon.speed){
            state = BattleState.Player;
            playerFirst = true;
        }else if(player.mon.speed == enemy.mon.speed){
            int speedTie = Random.Range(0,2);
            if(speedTie == 0){
                playerFirst = true;
                state = BattleState.Player;
            }else{
                playerFirst = false;
                state = BattleState.Enemy;
            }
        }else{
            state = BattleState.Enemy;
            playerFirst = false;
        }
        battleText.enabled = true;
    }
    void ClearMoveText(){
        foreach(TextMeshProUGUI g in moveTexts){
            g.enabled = false;
        }
        info[0].text = "";
        info[1].text = "";
        info[2].text = "";
    }
    IEnumerator PlayerAttack(){
        for(int i = 0; i < selectedMove.NumHits; i++){
            state = BattleState.Wait;
            int hitChance = Random.Range(0,101);
            battleText.text = player.mon.monName + " used " + selectedMove.MoveName;
            yield return new WaitForSeconds(0.3f);
            if(selectedMove.Accuracy < hitChance){
                battleText.text = player.mon.monName + " attack missed";
            }else{
                enemy.TakeDamage(CalcDamage());
            }
            yield return new WaitForSeconds(0.2f);
        }
        if(playerFirst == false){
            state = BattleState.Enemy;
        }else{
            state = BattleState.Start;
        }
    }

    int CalcDamage(){
        int newDamage = 0;
        if(selectedMove.Type == player.mon.type1 || selectedMove.Type == player.mon.type2){
            newDamage = (int)(selectedMove.Damage * 1.5);
        }else{
            newDamage = selectedMove.Damage;
        }

        if(selectedMove.isPhysical == true){
            newDamage += player.mon.attack;
        }else{
            newDamage += player.mon.intelligence;
        }
        return newDamage;
    }
}
