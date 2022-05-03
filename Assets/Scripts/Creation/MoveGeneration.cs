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
            int damage = Random.Range(50,line.lRend.positionCount * 5);
            mo.Accuracy = Random.Range(40,101);
            mo.NumHits = Random.Range(1,6);
            damage -= mo.NumHits * 10;
            if(damage < 0)damage = 0;
            damage += 10;
            mo.Damage = damage;
            mo.Accuracy -= (int)damage/10;
            //Typing
            if(i < stat.finalType.Count){
                mo.Type = stat.finalType[i];
            }else{
                List<string> typs = stat.ChooseType();
                mo.Type = typs[0];
            }
            //Chooses between physical or intelligence based
            int choice = Random.Range(0,2);
            if(choice == 0){
                mo.isPhysical = true; 
            }else{
                mo.isPhysical = false;
            }
            //Effect Creation
            int ch = Random.Range(0,changes.Length);
            mo.typeOfChange = changes[ch];
            float numChange = Mathf.Round(Random.Range(0.7f,1.5f) * 10);
            mo.numChange = numChange/10;
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
