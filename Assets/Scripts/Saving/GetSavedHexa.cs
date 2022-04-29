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
    public List<TextMeshProUGUI> nameTexts = new List<TextMeshProUGUI>();
    void Start()
    {
        mons = GetMons();
        hex.monData = mons[0];
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
        }
    }
    List<SaveMon> GetMons(){
        string dir = Application.persistentDataPath + directory;
        string[] monFiles = Directory.GetFiles(dir);
        Debug.Log(monFiles[0]);
        List<SaveMon> mo = new List<SaveMon>();
        for(int i = 0; i < monFiles.Length; i++){
            SaveMon loadMon = SaveManager.Load(monFiles[i]);
            mo.Add(loadMon);
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
}