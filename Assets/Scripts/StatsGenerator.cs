using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsGenerator : MonoBehaviour
{
    private string[] types = {"Stick","Gun","Ice","Garbage","Baseball","Proposal","Disaster","Writer","Analysis","Wedding","Night","Secretary","Actor","Contract","Platform","Guidance","Two","Introduction","Data","Arrival","Philosophy","Customer","Attention","Power","Historian","Situation","Teaching"};
    private string[] adjectives = {"SUS","Naive","Giant","Reflective","Orange","Dead","Yellow","Rhetorical","Extra-Small","Consistent","Puny","Wary","Marvelous","Ablaze","Idiotic","Special","Materialistic","Realistic","Earthy","Grateful","Interesting","Public","Somber","Cuddly","Additional","Depressed"};
    [SerializeField]private List<string> finalType = new List<string>();
    [SerializeField]private int maxTypes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J)){
            finalType = ChooseType();
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
}
