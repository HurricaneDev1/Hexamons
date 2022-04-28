using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Hexamon : MonoBehaviour
{
    public SaveMon monData;
    public BattleSystem bat;
    [SerializeField]private Transform healthBar;
    [SerializeField]private SpriteRenderer picture;
    [SerializeField]private float pictureSize;

    [SerializeField]private float speed = 5f;
 
    [SerializeField]private float height = 0.5f;

    void Start(){
        StartCoroutine(SetUpPicture());
    }

    IEnumerator SetUpPicture(){
        yield return new WaitForSeconds(0.1f);
        Texture2D tex = LoadPNG(monData.picturePath);
        Sprite monPhoto = Sprite.Create(tex,new Rect(0,0,tex.width,tex.height),new Vector2(0.5f,0.5f));
        picture.sprite = monPhoto;
        transform.localScale = new Vector2(transform.localScale.x * pictureSize,transform.localScale.y * pictureSize);
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

    void Update(){
        if(bat.state == BattleState.SelectAction || bat.state == BattleState.SelectMove){
            Bob(transform);
            Bob(healthBar);
        }
    }

    void Bob(Transform trans){
        Vector2 pos = trans.position;

        float newY = Mathf.Sin(Time.time * speed);

        trans.position = new Vector3(pos.x, newY * height);
    }
}
