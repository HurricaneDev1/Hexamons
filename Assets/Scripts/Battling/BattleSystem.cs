using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum BattleState{
    Start,
    SelectAction,
    SelectMove,
    SwapMon,
    Player,
    Enemy,
    PlayerDead,
    EnemyDead,
    Wait
}

public class BattleSystem : MonoBehaviour
{
    [Header("Objects")]
    public BattleState state;
    public BattleMon enemy;
    public BattleMon player;
    public GetSavedHexa get;
    public Enemy en;
    public List<SaveMon> enemies = new List<SaveMon>();
    [SerializeField]private GameObject battleUI;
    [SerializeField]private GameObject monSelection;
    [Header("Text")]
    [SerializeField]private TextMeshProUGUI nameText;
    [SerializeField]private TextMeshProUGUI enemyNameText;
    [SerializeField]private TextMeshProUGUI battleText;
    public TextMeshProUGUI healthPercentageText;
    [SerializeField]private List<TextMeshProUGUI> info = new List<TextMeshProUGUI>();
    [SerializeField]private List<TextMeshProUGUI> actionText = new List<TextMeshProUGUI>();
    [SerializeField]private List<TextMeshProUGUI> moveTexts = new List<TextMeshProUGUI>();
    [Header("Action/Move Stuff")]
    [SerializeField]private int actionNum = 0;
    public int currentMon = 0;
    [SerializeField]private int currentMove = 0;
    public Move selectedMove;
    public Color highlight;
    [SerializeField]private bool playerFirst;
    [SerializeField]private bool playerDied;
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.Start;
        currentMon = PlayerPrefs.GetInt("CurrentMon");
        player.SetSize();
    }
    void Update(){
        //State machine to see what to do next
        switch(state){
            case BattleState.Start:
                UpdateName();
                healthPercentageText.text = player.mon.currentHealth.ToString() + "/" + player.mon.maxHealth;
                SetUpActions();
                break;
            case BattleState.SelectAction:
                SelectAction();
                if(Input.GetKeyDown(KeyCode.Z)){
                    if(actionNum == 0){
                        SetUpMoves(player.mon.moves);
                    }else if(actionNum == 1){
                        StartCoroutine(Catch());
                    }else if(actionNum == 2){
                        SetUpSwap();
                    }
                }
                break;
            case BattleState.SelectMove:    
                MoveSelection(player.mon.moves);
                if(Input.GetKeyDown(KeyCode.Z)){
                    SpeedCheck();
                    ClearMoveText();
                }
                if(Input.GetKeyDown(KeyCode.X)){
                    state = BattleState.SelectAction;
                    
                    ClearMoveText();
                    SetUpActions();
                }
                break;
            case BattleState.SwapMon:
                ChooseMon();
                if(Input.GetKeyDown(KeyCode.Z)){
                    SwapMon();
                    StopSwap();
                }
                if(Input.GetKeyDown(KeyCode.X) && playerDied == false){
                    state = BattleState.SelectAction;
                    StopSwap();
                    SetUpActions();
                }
                break;
            case BattleState.Player:
                StartCoroutine(PlayerAttack());
                break;
            case BattleState.Enemy:
                StartCoroutine(EnemyAttack());
                break;
            case BattleState.EnemyDead:
                state = BattleState.Start;
                StartCoroutine(EnemyDie());
                break;
            case BattleState.PlayerDead:
                if(playerDied != true){
                    StartCoroutine(PlayerDie());
                }
                break;
        }
    }
    //Updates the name text of enemy and player
    void UpdateName(){
        nameText.text = player.mon.monName;
        enemyNameText.text = enemy.mon.monName;
    }
    //Enables text and changes the battlestate
    void SetUpActions(){
        battleText.enabled = false;
        foreach(TextMeshProUGUI g in actionText){
            g.enabled = true;
        }
        state = BattleState.SelectAction;
    }
    //Lets you choose which action you would like to do
    void SelectAction(){
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            if(actionNum > 0)
                actionNum --;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            if(actionNum < actionText.Count - 1)
                actionNum ++;
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            if(actionNum == 0)
                actionNum += 2;
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            if(actionNum == 2)
                actionNum -= 2;
        }
        foreach(TextMeshProUGUI g in actionText){
            g.color = Color.black;   
        }
        actionText[actionNum].color = highlight;
    }
    //Disables stuff and changes battlestate for swapping
    void SetUpSwap(){
        state = BattleState.SwapMon;
        battleUI.SetActive(false);
        monSelection.SetActive(true);
        get.SpawnMons();
    }

    //Gets rid of everything set up for swapping
    void StopSwap(){
        battleUI.SetActive(true);
        monSelection.SetActive(false);
        Hexamon hex = player.GetComponent<Hexamon>();
        hex.monData = player.mon;
        for(int i = 0; i < get.mons.Count; i++){
            if(get.mons[i] == hex.monData){
                currentMon = i;
            }
        }
        int amount = get.nameTexts.Count;
        for(int t  = amount; t > 0; t--){
            Destroy(get.nameTexts[t-1].gameObject);
            get.nameTexts.Remove(get.nameTexts[t-1]);
        }
    }
    //Select which mon you want to switch into
    void ChooseMon(){
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            if(currentMon > 0)
                currentMon --;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            if(currentMon < get.mons.Count - 1)
                currentMon ++;
        }
    }
    //Switches the hexamon in battle and changes stuff to fit it
    void SwapMon(){
        player.mon = get.mons[currentMon];
        Hexamon hex = player.GetComponent<Hexamon>();
        hex.monData = get.mons[currentMon];
        StartCoroutine(hex.SetUpPicture());
        nameText.text = player.mon.monName;
        healthPercentageText.text = player.mon.currentHealth.ToString() + "/" + player.mon.maxHealth;
        player.SetSize();
        player.attackMod = 1;
        player.speedMod = 1;
        player.intelligenceMod = 1;
        player.defenseMod = 1;
        foreach(TextMeshProUGUI g in actionText){
            g.enabled = false;
        }
        battleText.enabled = true;
        if(playerDied == true){
            playerDied = false; 
            playerFirst = false;
            state = BattleState.Start;
        }else{
            playerFirst = true;
            state = BattleState.Enemy;
        }
    }
    //Enables moves and disables action text
    void SetUpMoves(List<Move> mo){
        state = BattleState.SelectMove;
        for(int i = 0; i < moveTexts.Count; i++){
            moveTexts[i].enabled = true;
            moveTexts[i].text = mo[i].MoveName;
        }
        foreach(TextMeshProUGUI g in actionText){
            g.enabled = false;
        }
    }
    //Selects which move you want to use
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

    //Turns the enemy pokemon to yours
    IEnumerator Catch(){
        foreach(TextMeshProUGUI g in actionText){
            g.enabled = false;
        }
        battleText.enabled = true;
        battleText.text = "You tried to catch " + enemy.mon.monName;
        yield return new WaitForSeconds(1);
        if(Random.Range(1,101) <= 40){
            battleText.text = "You caught " + enemy.mon.monName;

            yield return new WaitForSeconds(1);
            en.mons[0].isMine = true;
            get.mons.Add(en.mons[0]);
            en.mons.Remove(en.mons[0]);
            foreach(SaveMon savingMon in get.mons){
                SaveManager.Save(savingMon);
            }
            PlayerPrefs.SetInt("CurrentMon",currentMon + 1);
            if(en.mons.Count > 0){
                en.MonChange();
                enemy.SetSize();
                UpdateName();
                state = BattleState.Start;
            }else{
                SceneManager.LoadScene("WalkingArea");
            }
        }else{
            battleText.text = "You failed to catch " + enemy.mon.monName;
            yield return new WaitForSeconds(1);
            playerFirst = true;
            state = BattleState.Enemy;
        }
    }
    //Checks to see who goes first based on speed
    void SpeedCheck(){
        if(player.mon.speed * player.speedMod > enemy.mon.speed * player.speedMod){
            state = BattleState.Player;
            playerFirst = true;
        }else if(player.mon.speed * player.speedMod == enemy.mon.speed * enemy.speedMod){
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
    //Gets rid of move text and info text
    void ClearMoveText(){
        foreach(TextMeshProUGUI g in moveTexts){
            g.enabled = false;
        }
        info[0].text = "";
        info[1].text = "";
        info[2].text = "";
    }
    //Attacks the enemy 
    IEnumerator PlayerAttack(){
        for(int i = 0; i < selectedMove.NumHits; i++){
            state = BattleState.Wait;
            int hitChance = Random.Range(0,101);
            battleText.text = player.mon.monName + " used " + selectedMove.MoveName;
            yield return new WaitForSeconds(0.3f);
            if(selectedMove.Accuracy < hitChance){
                battleText.text = player.mon.monName + " attack missed";
            }else{
                StatChange(selectedMove,player,enemy);
                enemy.TakeDamage(CalcDamage(selectedMove,player));
                if(state == BattleState.EnemyDead)break;
            }
            yield return new WaitForSeconds(0.2f);
        }
        if(playerFirst == true && state == BattleState.Wait){
            state = BattleState.Enemy;
        }else if(state == BattleState.Wait){
            state = BattleState.Start;
        }
    }

    //Calculates stab and stats afffect on attack
    int CalcDamage(Move mo, BattleMon mon){
        int newDamage = mo.Damage;
        if(mo.isPhysical == true){
            newDamage += (mon.mon.attack * (int)mon.attackMod);
            // newDamage *= (int)mon.attackMod;
        }else{
            newDamage += mon.mon.intelligence * (int)mon.intelligenceMod;
            // newDamage *= (int)mon.intelligenceMod;
        }

        if(mo.Type == mon.mon.type1 || mo.Type == mon.mon.type2){
            newDamage = (int)(newDamage * 1.5);
        }
        return newDamage;
    }

    //Changes a hexamons stat modifiers based on a move
    void StatChange(Move mo, BattleMon good, BattleMon bad){
        int changeChance = Random.Range(0,101);
        if(mo.effectChance > changeChance){
            switch(mo.typeOfChange){
            case "Attack":
                if(mo.effectMe == true){
                    good.attackMod *= mo.numChange;
                }else{
                    bad.attackMod *= mo.numChange;
                }
                break;
            case "Defense":
                if(mo.effectMe == true){
                    good.defenseMod *= mo.numChange;
                }else{
                    bad.defenseMod *= mo.numChange;
                }
                break;
            case "Intelligence":
                if(mo.effectMe == true){
                    good.intelligenceMod *= mo.numChange;
                }else{
                    bad.intelligenceMod *= mo.numChange;
                }
                break;
            case "Speed":
                if(mo.effectMe == true){
                    good.speedMod *= mo.numChange;
                }else{
                    bad.speedMod *= mo.numChange;
                }
                break;
            }
        }
    }

    //Enemy randomly chooses move and attacks
    IEnumerator EnemyAttack(){
        List<Move> moves = enemy.mon.moves;
        int moveSelect = Random.Range(0,4);
        Move curMove = moves[moveSelect];
        for(int i = 0; i < curMove.NumHits; i++){
            state = BattleState.Wait;
            int hitChance = Random.Range(0,101);
            battleText.text = enemy.mon.monName + " used " + curMove.MoveName;
            yield return new WaitForSeconds(0.3f);
            if(curMove.Accuracy < hitChance){
                battleText.text = enemy.mon.monName + " attack missed";
            }else{
                StatChange(curMove,enemy,player);
                player.TakeDamage(CalcDamage(curMove,enemy));
                if(state == BattleState.PlayerDead)break;
            }
            yield return new WaitForSeconds(0.2f);
        }
        if(playerFirst == false && state == BattleState.Wait){
            state = BattleState.Player;
        }else if(state == BattleState.Wait){
            state = BattleState.Start;
        }
        yield return new WaitForSeconds(1);
    }

    IEnumerator EnemyDie(){
        yield return new WaitForSeconds(1f);
        en.mons.Remove(en.mons[0]);
        SaveManager.DestroyMon(enemy.mon.monName);
        if(en.mons.Count > 0){
            en.MonChange();
            enemy.SetSize();
            UpdateName();
        }else{
            foreach(SaveMon savingMon in get.mons){
                SaveManager.Save(savingMon);
            }
            PlayerPrefs.SetInt("CurrentMon",currentMon);
            SceneManager.LoadScene("WalkingArea");
        }
    }

    IEnumerator PlayerDie(){
        playerDied = true;
        yield return new WaitForSeconds(1f);
        get.mons.Remove(get.mons[currentMon]);
        currentMon = 0;
        SaveManager.DestroyMon(player.mon.monName);
        if(get.mons.Count > 0){
            player.mon = get.mons[0];
            Hexamon hex = player.GetComponent<Hexamon>();
            hex.monData = get.mons[0];
            StartCoroutine(hex.SetUpPicture());
            SetUpSwap();
        }else{
            PlayerPrefs.SetInt("CurrentMon",0);
           SceneManager.LoadScene("WalkingArea"); 
        }
    }
}
