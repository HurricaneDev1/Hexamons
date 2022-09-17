using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public void NextScene(){
        foreach(SaveMon savingMon in GrabMon.GetMons(false)){
            SaveManager.DestroyMon(savingMon.monName);
        }
        foreach(SaveMon savingMon in GrabMon.GetMons(true)){
            SaveManager.DestroyMon(savingMon.monName);
        }
        SceneManager.LoadScene(1);
    }
}
