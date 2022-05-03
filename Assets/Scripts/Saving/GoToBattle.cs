using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToBattle : MonoBehaviour
{
    public bool isTrainer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NextScene());
    }

    IEnumerator NextScene(){
        if(isTrainer == false){
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene("BattleScene");
        }
        //else{
        //     int numHexamon = Random.Range(1,5);
        //     for(int i = 0; i < numHexamon; i++){
                
        //     }
        // }
    }
}
