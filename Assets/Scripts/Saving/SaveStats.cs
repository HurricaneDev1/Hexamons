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
        yield return new WaitForSeconds(0.1f);
        mon.maxHealth = stats.Health;
        mon.currentHealth = mon.maxHealth;
        mon.attack = stats.Attack;
        mon.defense = stats.Defense;
        mon.monName = stats.monName;
        mon.speed = stats.Speed;
        mon.intelligence = stats.Intelligence;
        mon.type1 = stats.finalType[0];
        mon.type2 = stats.finalType[1];
        mon.picturePath = hex.filename;
        Texture2D tex = LoadPNG(hex.filename);
        Sprite monPhoto = Sprite.Create(tex,new Rect(0,0,tex.width,tex.height),new Vector2(0.5f,0.5f));
        picture.sprite = monPhoto;
        transform.localScale = new Vector2(transform.localScale.x * pictureSize,transform.localScale.y * pictureSize);
        SaveManager.Save(mon);
    }

    public static Texture2D LoadPNG(string filePath) {
 
     Texture2D tex = null;
     byte[] fileData;
 
     if (File.Exists(filePath))     {
         fileData = File.ReadAllBytes(filePath);
         tex = new Texture2D(2, 2);
         tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
     }
     return tex;
 }
}
