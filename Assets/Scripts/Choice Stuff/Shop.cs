using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public int hexaBux;
    public ChoiceStuff choice;
    public TextMeshProUGUI buxCount;
    public List<TextMeshProUGUI> priceTexts = new List<TextMeshProUGUI>();

    void Update(){
        UpdatePrice();
    }
    
    public void BuyHeal(){
        if(hexaBux >= choice.floorNum * 5){
            List<SaveMon> myMons = GrabMon.GetMons(true);
            foreach(SaveMon mon in myMons){
                mon.currentHealth = mon.maxHealth;
                SaveManager.Save(mon);
            }
            choice.explanation.text = "All your Hexamons recovered their lost Health";
            hexaBux -= choice.floorNum * 5;
            buxCount.text = "Hexabux:" + hexaBux.ToString();
        }
    }

    public void BuyMon(){
        if(hexaBux >= choice.floorNum * 10){
            StartCoroutine(choice.GiftMon(choice.floorNum * 5, choice.floorNum * 10));
            hexaBux -= choice.floorNum * 10;
            buxCount.text = "Hexabux:" + hexaBux.ToString();
        }
    }

    public void BuyUpgrade(){
        if(hexaBux >= choice.floorNum * 2){
            List<SaveMon> myMons = GrabMon.GetMons(true);
            foreach(SaveMon mon in myMons){
                mon.attack = (int)(mon.attack * 1.2);
                mon.defense = (int)(mon.defense * 1.2);
                mon.speed = (int)(mon.speed * 1.2);
                mon.intelligence = (int)(mon.intelligence* 1.2);
                mon.maxHealth = (int)(mon.maxHealth * 1.2);
                SaveManager.Save(mon);
            }
            hexaBux -= choice.floorNum * 2;
            buxCount.text = "Hexabux:" + hexaBux.ToString();
        }
    }

    public void Leave(){
        choice.state = ChoiceState.SetChoices;
    }

    void UpdatePrice(){
        buxCount.text = "Hexabux:" + hexaBux.ToString();
        priceTexts[0].text = "Heal: " + (choice.floorNum * 5).ToString();
        priceTexts[1].text = "Buy Mon: " + (choice.floorNum * 10).ToString();
        priceTexts[2].text = "Buff Mons: " +(choice.floorNum * 2).ToString();
    }
}
