using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChoiceState{
    Battle,
    Gift,
    Heal,
    Shop,
    Event,
    Wait
}
public class ChoiceStuff : MonoBehaviour
{
    [SerializeField]private int floorNum;
    [SerializeField]private ChoiceState state;
    [SerializeField]private BattleSystem bat;

    void Start(){
        state = ChoiceState.Event;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state){
            case ChoiceState.Battle:
                bat.state = BattleState.SetUp;
                state = ChoiceState.Wait;
                break;
            case ChoiceState.Gift:
                break;
            case ChoiceState.Heal:
                break;
            case ChoiceState.Shop:
                break;
            case ChoiceState.Event:
                break;
        }
    }

    public void UpdateChoices(){
        state = ChoiceState.Battle;
    }
}
