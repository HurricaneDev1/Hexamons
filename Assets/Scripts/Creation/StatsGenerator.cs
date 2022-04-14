using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsGenerator : MonoBehaviour
{
    private string[] types = {"Contracts","Wealth","Military","Trade","Land","Resources"};
    private string[] adjectives = {"Idealism","Realism","Utopianism","Communism","Captilism","Socialism"};
    public List<string> finalType = new List<string>();
    public int Attack;
    
    public int Defense;
    public int Intelligence;
    public int Speed;
    public int Health;
    public string monName;
    private char[] letters = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};
    [SerializeField]private int maxLetters;
    private LineCreation line;
    // Start is called before the first frame update
    void Start()
    {
        finalType = ChooseType();
        line = this.GetComponent<LineCreation>();
        MakeStats();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J)){
            finalType = ChooseType();
            MakeStats();
        }
    }

    List<string> ChooseType(){
        List<string> actualType = new List<string>();
        int adjectiveSelection = Random.Range(0,adjectives.Length);
        actualType.Add(adjectives[adjectiveSelection]);
        int typeSelection = Random.Range(0,types.Length);
        actualType.Add(types[typeSelection]);
        return actualType;
    }

    void MakeStats(){
        int posCount = line.lRend.positionCount;
        Attack = Random.Range(posCount, posCount * 5);
        Defense = Random.Range(posCount,posCount * 5);
        Intelligence = Random.Range(posCount,posCount * 5);
        Speed = Random.Range(posCount, posCount * 5);
        Health = Random.Range(posCount * 2, posCount * 10);
        MakeName();
    }

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
