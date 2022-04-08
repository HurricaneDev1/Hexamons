using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GetSavedHexa : MonoBehaviour
{
    private static string directory = "/MonSaveData/";
    public List<SaveMon> mons = new List<SaveMon>();
    [SerializeField]private GameObject monPrefab;
    // Update is called once per frame
    void Start()
    {
        mons = GetMons();
        SpawnMons();
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

    void SpawnMons(){
        foreach(SaveMon mo in mons){
            GameObject spawnedMon = Instantiate(monPrefab,transform.position,Quaternion.identity);
            spawnedMon.GetComponent<Hexamon>().monData = mo;
            GetSavedHexa get = FindObjectOfType<GetSavedHexa>();
            spawnedMon.transform.SetParent(get.transform);
        }
    }

    void SortMons(){

    }
}