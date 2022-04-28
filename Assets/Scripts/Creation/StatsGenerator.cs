using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsGenerator : MonoBehaviour
{
    private string[] types = {"Idealism","Realism","Utopianism","Communism","Capitalism","Socialism","Utilitarianism","Egoism","Intellectualism","Welfarism","Pacifism","Nepotism"};
    public List<string> finalType = new List<string>();
    public int Attack;
    
    public int Defense;
    public int Intelligence;
    public int Speed;
    public int Health;
    public string monName;
    public List<Move> moves = new List<Move>();
    private char[] letters = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};
    [SerializeField]private int maxLetters;
    private LineCreation line;
    private MoveGeneration moveGen;
    // Start is called before the first frame update
    void Start()
    {
        finalType = ChooseType();
        line = GetComponent<LineCreation>();
        moveGen = GetComponent<MoveGeneration>();
        finalType = ChooseType();
        MakeStats();
        moves = moveGen.MakeMoves();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J)){
            finalType = ChooseType();
            MakeStats();
            moves = moveGen.MakeMoves();
        }
    }

    //Randomly sets the type of the hexamon
    public List<string> ChooseType(){
        List<string> actualType = new List<string>();
        int typeChoice = Random.Range(0,types.Length);
        int twoTypes = Random.Range(0,2);
        if(twoTypes == 1){
            actualType.Add(types[typeChoice]);
            typeChoice = Random.Range(0,types.Length);
            if(actualType[0] != types[typeChoice]){
                actualType.Add(types[typeChoice]);
            }
        }else{
            actualType.Add(types[typeChoice]);
        }
        return actualType;
    }

    //Sets attack and other stats
    void MakeStats(){
        int posCount = line.lRend.positionCount;
        Attack = Random.Range(posCount, posCount * 5);
        Defense = Random.Range(posCount,posCount * 5);
        Intelligence = Random.Range(posCount,posCount * 5);
        Speed = Random.Range(posCount, posCount * 5);
        Health = Random.Range(posCount * 2, posCount * 10);
        MakeName();
    }

    //Randomly puts leters together to create a name
    void MakeName(){
        monName = "";
        int numLet = Random.Range(4,maxLetters);
        for(int i = 0; i < numLet; i++){
            int ranNum = Random.Range(0,26);
            monName += letters[ranNum];
        }
        Debug.Log(monName);
    }
}
