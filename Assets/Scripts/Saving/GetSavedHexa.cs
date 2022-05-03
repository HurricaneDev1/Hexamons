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
        hex.monData = mons[currentMon];
        battleMon.mon = mons[currentMon];
    }

    void Update(){
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
    public List<SaveMon> GetMons(bool yours){
        string dir = Application.persistentDataPath + directory;
        string[] monFiles = Directory.GetFiles(dir);
        Debug.Log(monFiles[0]);
        List<SaveMon> mo = new List<SaveMon>();
        for(int i = 0; i < monFiles.Length; i++){
            SaveMon loadMon = SaveManager.Load(monFiles[i]);
            if(loadMon.isMine == yours){
                mo.Add(loadMon);
            } 
        }
        return mo;
    }

    public void SpawnMons(){
        for(int i = 0; i < mons.Count; i++){
            TextMeshProUGUI text = Instantiate(monName,new Vector3(textSpawn.position.x,textSpawn.position.y - i),Quaternion.identity);
            text.text = mons[i].monName;
            text.transform.SetParent(monSelection,true);
            text.rectTransform.localScale = new Vector3(2,2,2);
            nameTexts.Add(text);
        }
    }

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