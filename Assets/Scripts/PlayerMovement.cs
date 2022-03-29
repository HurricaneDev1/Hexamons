using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.position = new Vector2(transform.position.x + direction.x /20,transform.position.y + direction.y/20);
    }

    void OnTriggerStay2D(Collider2D col){
        Debug.Log("Grass");
        if(col.tag == "Grass"){
            int fightChance = Random.Range(0,10);
            if(fightChance == 1){
                SceneManager.LoadScene("MonScene");
            }
        }
    }
}
