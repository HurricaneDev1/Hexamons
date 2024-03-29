using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class GetSavedHexa : MonoBehaviour
{
    private static string directory = "/MonSaveData/";
    public List<SaveMon> mons = new List<SaveMon>();
    [SerializeField]private BattleMon battleMon;
    [SerializeField]private Hexamon hex;
    [SerializeField]private Transform textSpawn;
    [SerializeField]private TextMeshProUGUI monName;
    [SerializeField]private BattleSystem bat;
    [SerializeField]private Transform monSelection;
    public List<TextMeshProUGUI> info = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> nameTexts = new List<TextMeshProUGUI>();
    void Start()
    {
        mons = GetMons(true);
        int currentMon = PlayerPrefs.GetInt("CurrentMon");
        if(hex != null){
            hex.monData = mons[currentMon];
            battleMon.mon = mons[currentMon];
        }
    }

    void Update(){
        //Does stuff if the swap action is being preformed
        if(bat != null){
            if(bat.state == BattleState.SwapMon){
            for(int t = 0; t < nameTexts.Count; t++){
                if(t != bat.currentMon){
                    nameTexts[t].color = Color.black;
                }else{
                    nameTexts[bat.currentMon].color = bat.highlight;
                }
            }
            UpdateInfo();
        }
        }
    }
    //Takes all mons from the mon info area and gets their data
    public List<SaveMon> GetMons(bool yours){
        string dir = Application.persistentDataPath + directory;
        string[] monFiles = Directory.GetFiles(dir);
        List<SaveMon> mo = new List<SaveMon>();
        for(int i = 0; i < monFiles.Length; i++){
            SaveMon loadMon = SaveManager.Load(monFiles[i]);
            if(loadMon.isMine == yours){
                mo.Add(loadMon);
            } 
        }
        return mo;
    }

    //Sets up the text for each hexamon when switching
    public void SpawnMons(){
        for(int i = 0; i < mons.Count; i++){
            TextMeshProUGUI text = Instantiate(monName,new Vector3(textSpawn.position.x,textSpawn.position.y - i),Quaternion.identity);
            mons = GetMons(true);
            text.text = mons[i].monName;
            text.transform.SetParent(monSelection,true);
            text.rectTransform.localScale = new Vector3(2,2,2);
            nameTexts.Add(text);
        }
    }

    //Makes the info text fit for the current hexamon
    void UpdateInfo(){
        hex.monData = mons[bat.currentMon];
        StartCoroutine(hex.SetUpPicture());
        info[0].text = "Attack:" + hex.monData.attack.ToString();
        info[1].text = "Defense:" + hex.monData.defense.ToString();
        info[2].text = "Speed:" + hex.monData.speed.ToString();
        info[3].text = "Intelligence:" + hex.monData.intelligence.ToString();
        info[4].text = "Health:" + hex.monData.currentHealth.ToString() + "/" + hex.monData.maxHealth.ToString();
        if(hex.monData.type2 != ""){
            info[5].text = hex.monData.type1 + "/" + hex.monData.type2;
        }else{
            info[5].text = hex.monData.type1;
        }
    }   
}

public static class GrabMon{
    public static List<SaveMon> GetMons(bool yours){
        string dir = Application.persistentDataPath + "/MonSaveData/";
        string[] monFiles = Directory.GetFiles(dir);
        List<SaveMon> mo = new List<SaveMon>();
        for(int i = 0; i < monFiles.Length; i++){
            SaveMon loadMon = SaveManager.Load(monFiles[i]);
            if(loadMon.isMine == yours){
                mo.Add(loadMon);
            } 
        }
        return mo;
    }
}