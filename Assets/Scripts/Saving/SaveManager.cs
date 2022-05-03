using UnityEngine;
using System.IO;

public static class SaveManager
{
    public static string directory = "/MonSaveData/";
    public static string fileName = "SavedMon.txt";
    public static void Save(SaveMon mon){
        fileName = mon.monName + ".txt";
        string dir = Application.persistentDataPath + directory;
        Debug.Log(dir);

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

    public static void DestroyMon(string monName){
        fileName = monName + ".txt";
        string picFileName = monName + ".png";
        string dir = Application.persistentDataPath + directory;
        string picDir = Application.persistentDataPath + "/HexamonPictures/";
        File.Delete(dir + fileName);
        File.Delete(picDir + picFileName);
    }
}
