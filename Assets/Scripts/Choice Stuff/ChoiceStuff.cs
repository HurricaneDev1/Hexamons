using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum ChoiceState{
    Battle,
    Gift,
    Shop,
    Event,
    Wait,
    SetChoices,
    Boss
}
public class ChoiceStuff : MonoBehaviour
{
    public int floorNum;
    public ChoiceState state;
    [SerializeField]private BattleSystem bat;
    [SerializeField]private StatsGenerator stats;
    [SerializeField]private TextMeshProUGUI choice1;
    [SerializeField]private TextMeshProUGUI choice2;
    public TextMeshProUGUI explanation;
    public GameObject shop;
    public GameObject choices;

    void Start(){
        state = ChoiceState.Gift;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state){
            case ChoiceState.SetChoices:
                GetChoices();
                floorNum ++;
                state = ChoiceState.Wait;
                break;
            case ChoiceState.Battle:
                StartCoroutine(BattleSetup(floorNum * 5, floorNum * 15));
                break;
            case ChoiceState.Gift:
                List<SaveMon> mons = GrabMon.GetMons(true);
                if(mons.Count == 0){
                    StartCoroutine(GiftMon(40,80));
                }else{
                    GenerateGift();
                }
                state = ChoiceState.SetChoices;
                break;
            case ChoiceState.Shop:
                shop.SetActive(true);
                choices.SetActive(false);
                state = ChoiceState.Wait;
                break;
            // case ChoiceState.Event:
            //     break;
            case ChoiceState.Boss:
                Debug.Log("Boss");
                StartCoroutine(BattleSetup(floorNum * 15, floorNum * 25));
                break;
            case ChoiceState.Wait:
                break;
        }

        if(Input.GetKeyDown(KeyCode.K)){
            state = ChoiceState.SetChoices;
        }
    }

    IEnumerator BattleSetup(int minlines, int maxLines){
        state = ChoiceState.Wait;
        stats.MakeMon(minlines, maxLines);
        yield return new WaitForSeconds(0.5f);
        stats.save.mon.isMine = false;
        SaveManager.Save(stats.save.mon);
        yield return new WaitForSeconds(0.7f);
        bat.state = BattleState.SetUp;
    }

    public IEnumerator GiftMon(int minLines, int maxLines){
        //* Make numLines adjustable from here
        stats.MakeMon(minLines,maxLines);
        yield return new WaitForSeconds(0.2f);
        stats.save.mon.isMine = true;
        SaveManager.Save(stats.save.mon);
    }

    void GetChoices(){
        if(floorNum != 5 && floorNum != 11 && floorNum != 17){
            int roomChoice1 = Random.Range(0,7);
            if(roomChoice1 <= 2){
                choice1.text = "Battle";
            }else if(roomChoice1 == 3){
                choice1.text = "Gift";
            }else if(roomChoice1 == 4){
                choice1.text = "Shop";
            }
            //else{
            //     choice1.text = "Event";
            // }
            choice1.color = stats.line.MakeColor();

            int roomChoice2 = Random.Range(0,7);
            if(roomChoice2 <= 2){
                choice2.text = "Battle";
            }else if(roomChoice2 == 3){
                choice2.text = "Gift";
            }else if(roomChoice2 == 4){
                choice2.text = "Shop";
            }
            // else{
            //     choice2.text = "Event";
            // }
            choice2.color = stats.line.MakeColor();
        }else{
            choice1.text = "Boss";
            choice2.text = "Boss";
            choice1.color = Color.red;
            choice2.color = Color.red;
        }
    }

    public void CheckChoice(bool isChoice1){
        string chosen = "";
        if(isChoice1 == true){
            chosen = choice1.text;
        }else{
            chosen = choice2.text;
        }
        switch(chosen){
            case "Battle":
                state = ChoiceState.Battle;
                break;
            case "Gift":
                state = ChoiceState.Gift;
                break;
            case "Shop":
                state = ChoiceState.Shop;
                break;
            case "Event":
                state = ChoiceState.Event;
                break;
            case "Boss":
                state = ChoiceState.Boss;
                break;
        }
    }

    void GenerateGift(){
        List<SaveMon> myMons = GrabMon.GetMons(true);
        int giftChoice = Random.Range(0,5);
        if(giftChoice == 0){
            StartCoroutine(GiftMon(floorNum * 10, floorNum * 20));
            explanation.text = "You received a new Mon";
            //* Show Picture of new mon
        }else if(giftChoice == 1){
            foreach(SaveMon mon in myMons){
                mon.currentHealth = mon.maxHealth;
                SaveManager.Save(mon);
            }
            explanation.text = "All your Hexamons recovered their lost Health";
        }else if(giftChoice == 2){
            int drainedMon = Random.Range(0,myMons.Count);
            myMons[0].currentHealth = 1;
            SaveManager.Save(myMons[drainedMon]);
            explanation.text = "One of your Hexamons was drained of its life force";
        }else{
            foreach(SaveMon mon in myMons){
                mon.attack = (int)(mon.attack * 1.5);
                mon.defense = (int)(mon.defense * 1.5);
                mon.speed = (int)(mon.speed * 1.5);
                mon.intelligence = (int)(mon.intelligence* 1.5);
                mon.maxHealth = (int)(mon.maxHealth * 1.5);
                SaveManager.Save(mon);
            }
            explanation.text = "All of your Hexamons grew stronger";
        }
    }
}
