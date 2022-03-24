using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsGenerator : MonoBehaviour
{
    private string[] types = {"Fire","Water","Grass","Air"};
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
        int numTypes = Random.Range(1,maxTypes + 1);
        for(int i = 0; i < numTypes; i++){
            int typeSelection = Random.Range(0,types.Length);
            actualType.Add(types[typeSelection]);
        }
        return actualType;
    }
}
