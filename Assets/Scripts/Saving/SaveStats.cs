using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveStats : MonoBehaviour
{
    public SaveMon mon;
    public StatsGenerator stats;
    public MonPicture hex;
    [SerializeField]private SpriteRenderer picture;
    [SerializeField]private float pictureSize;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)){
            StartCoroutine(SetStats());
        }
        if(Input.GetKeyDown(KeyCode.L)){
            mon = SaveManager.Load(SaveManager.fileName);
        }
    }

    IEnumerator SetStats(){
        yield return new WaitForSeconds(0.4f);
        mon.maxHealth = stats.Health;
        mon.currentHealth = mon.maxHealth;
        mon.attack = stats.Attack;
        mon.defense = stats.Defense;
        mon.monName = stats.monName;
        mon.speed = stats.Speed;
        mon.intelligence = stats.Intelligence;
        mon.type1 = stats.finalType[0];
        if(stats.finalType.Count == 2){
            mon.type2 = stats.finalType[1];
        }else{
            mon.type2 = "";
        }
        mon.moves = stats.moves;
        yield return new WaitForSeconds(0.3f);
        mon.picturePath = hex.filename;
        SaveManager.Save(mon);
    }
}
