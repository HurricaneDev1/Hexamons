using UnityEngine;
using System.IO;

public static class SaveManager
{
    public static string directory = "/MonSaveData/";
    public static string fileName = "SavedMon.txt";
    public static void Save(SaveMon mon){
        fileName = mon.monName + ".txt";
        string dir = Application.persistentDataPath + directory;

        if(!Directory.Exists(dir)){
            Directory.CreateDirectory(dir);
        }
        string json = JsonUtility.ToJson(mon);
        File.WriteAllText(dir + fileName, json);
    }

    public static SaveMon Load(string monName){
        SaveMon mon = new SaveMon();

        if(File.Exists(monName)){
            string json = File.ReadAllText(monName);
            mon = JsonUtility.FromJson<SaveMon>(json);
        }else{
            Debug.Log("Save file does not exist");
        }
        return mon;
    }
}
