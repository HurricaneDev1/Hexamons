using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float moveSpeed;
    private bool isMoving;
    private Vector2 input;
    public LayerMask solidObjects;
    public LayerMask grassLayer;
    public LayerMask healLayer;

    private void Update(){
        if(!isMoving){
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //Removes diagonals
            if(input.x != 0)input.y = 0;

            if(input != Vector2.zero){
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;
                if(IsWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }
        }
    }

    IEnumerator Move(Vector3 targetPos){
        isMoving = true;
        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon){
            transform.position = Vector3.MoveTowards(transform.position,targetPos,moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;

        CheckForEncounters();
        CheckForHeal();
    }

    private bool IsWalkable(Vector3 targetPos){
        if(Physics2D.OverlapCircle(targetPos, 0.2f, solidObjects) != null){
            return false;
        }
        return true;
    }

    private void CheckForEncounters(){
        if(Physics2D.OverlapCircle(transform.position, 0.2f, grassLayer) != null){
            if(Random.Range(1,101) <= 10){
                SceneManager.LoadScene("WildMonScene");
            }
        }
    }

    private void CheckForHeal(){
        if(Physics2D.OverlapCircle(transform.position, 0.2f, healLayer) != null){
            Debug.Log("IsHeal");
            List<SaveMon> myMons = GrabMon.GetMons(true);
            foreach(SaveMon mon in myMons){
                mon.currentHealth = mon.maxHealth;
                SaveManager.Save(mon);
            }
        }
    }
}
