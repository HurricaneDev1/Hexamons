using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MoveGeneration : MonoBehaviour
{
    [SerializeField]private List<Move> moves = new List<Move>();
    private string[] changes = {"Attack","Defense","Intelligence","Speed",""};
    private LineCreation line;
    private StatsGenerator stat;
    private string[] prefixs = {"Hyper","Mega","Strong","Quick","Evil","Weak","Holy","Draco","Crazy","Sad","Happy","Angry","Meh","Old","Brave","Bloody","Rough","Huge","Sharp"};
    private string[] idealNouns = {"Mind","Naiveness","Thoughts"};
    private string[] realNouns = {"Truth","Authenticity","Common Sense"};
    private string[] utoNouns = {"Perfection","Happiness","Dream"};
    private string[] comNouns = {"Sharing","Corruption","Equality"};
    private string[] capNouns = {"Money","Freedom","Business","Market","Economy","Property"};
    private string[] socNouns = {"Community","Collectivism"};
    private string[] utilNouns = {"Efficiency","Sensibility"};
    private string[] egoNouns = {"Ego","Narcissism","Self-Esteem"};
    private string[] intNouns = {"Logic","Reason","Knowledge"};
    private string[] welNouns = {"Kindness","Progress","Health"};
    private string[] pacNouns = {"Protest","Peace"};
    private string[] nepNouns = {"Bias","Family","Preference"};
    public List<Move> MakeMoves(){
        moves.Clear();
        line = GetComponent<LineCreation>();
        stat = GetComponent<StatsGenerator>();
        for(int i = 0; i < 4 ; i++){
            Move mo = new Move();
            //Stats
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
            float numChange = Mathf.Round(Random.Range(0.6f,1.7f) * 10);
            mo.numChange = numChange/10;
            mo.effectChance = Random.Range(1,101);
            int me = Random.Range(0,2);
            if(me == 0){
                mo.effectMe = true;
            }else{
                mo.effectMe = false;
            }
            //MoveName
            mo.MoveName = MakeMoveName(mo);
            moves.Add(mo);
        }
        return moves;
    }

    string MakeMoveName(Move mo){
        string moveName = "";
        int randWord = Random.Range(0,prefixs.Length);
        moveName += prefixs[randWord];
        List<string> nounList = GetMoveNames(mo);
        int randNoun = Random.Range(0,nounList.Count - 1);
        moveName += nounList[randNoun];
        return moveName;
    }

    List<string> GetMoveNames(Move mo){
        List<string> names = new List<string>();
        switch(mo.Type){
            case "Idealism":
                names = idealNouns.ToList<string>();
                break;   
            case "Realism":
                names = realNouns.ToList<string>();
                break;
            case "Utopianism":
                names = utoNouns.ToList<string>();
                break;
            case "Communism":
                names = comNouns.ToList<string>();
                break;
            case "Capitalism":
                names = capNouns.ToList<string>();
                break;
            case "Socialism":
                names = socNouns.ToList<string>();
                break;
            case "Utilitarianism":
                names = utilNouns.ToList<string>();
                break;
            case "Egoism":
                names = egoNouns.ToList<string>();
                break;
            case "Intellectualism":
                names  = intNouns.ToList<string>();
                break;
            case "Welfarism":
                names = welNouns.ToList<string>();
                break;
            case "Pacifism":
                names = pacNouns.ToList<string>();
                break;
            case "Nepotism":
                names = nepNouns.ToList<string>();
                break;
        }
        return names;
    }
}
