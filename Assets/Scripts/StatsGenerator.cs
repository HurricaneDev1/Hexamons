using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsGenerator : MonoBehaviour
{
    private string[] types = {"Stick","Gun","Garbage","Baseball","Proposal","Disaster","Writer","Analysis","Wedding","Night","Secretary","Actor","Contract","Platform","Guidance","Data","Philosophy","Customer","Attention","Power","Historian"};
    private string[] adjectives = {"Suspicous","Naive","Giant","Reflective","Dead","Rhetorical","Extra-Small","Consistent","Puny","Wary","Marvelous","Idiotic","Special","Materialistic","Realistic","Earthy","Grateful","Interesting","Public","Somber","Cuddly","Depressed"};
    [SerializeField]private List<string> finalType = new List<string>();
    [SerializeField]private int Attack;
    
    [SerializeField]private int Defense;
    [SerializeField]private int Intelligence;
    [SerializeField]private int Speed;
    public LineCreation line;
    // Start is called before the first frame update
    void Start()
    {
        finalType = ChooseType();
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
    }
}
