using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGeneration : MonoBehaviour
{
    [SerializeField]private List<Move> moves = new List<Move>();
    private string[] changes = {"Attack","Defense","Intelligence","Speed",""};
    private LineCreation line;
    private StatsGenerator stat;
    public List<Move> MakeMoves(){
        moves.Clear();
        line = GetComponent<LineCreation>();
        stat = GetComponent<StatsGenerator>();
        for(int i = 0; i < 4 ; i++){
            Move mo = new Move();
            //MoveName
            int damage = Random.Range(10,line.lRend.positionCount * 10);
            mo.Accuracy = Random.Range(10,101);
            mo.NumHits = Random.Range(1,10);
            damage -= mo.NumHits * 10;
            if(damage < 0)damage = 0;
            damage += 10;
            mo.Damage = damage;
            if(i < stat.finalType.Count){
                mo.Type = stat.finalType[i];
            }else{
                List<string> typs = stat.ChooseType();
                mo.Type = typs[0];
            }
            int choice = Random.Range(0,2);
            if(choice == 0){
                mo.isPhysical = true;
            }else{
                mo.isPhysical = false;
            }
            int ch = Random.Range(0,changes.Length);
            mo.typeOfChange = changes[ch];
            mo.numChange = Random.Range(0.5f,2f);
            mo.effectChance = Random.Range(1,101);
            int me = Random.Range(0,2);
            if(me == 0){
                mo.effectMe = true;
            }else{
                mo.effectMe = false;
            }
            moves.Add(mo);
        }
        return moves;
    }
}
