using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]private GetSavedHexa get;
    [SerializeField]private BattleMon enemy;
    [SerializeField]private Hexamon hex;
    public List<SaveMon> mons = new List<SaveMon>();
    // Start is called before the first frame update
    void Start()
    {
        mons = get.GetMons(false);
        enemy.mon = mons[0];
        hex.monData = enemy.mon;
        StartCoroutine(hex.SetUpPicture());
    }

    public void MonChange(){
        hex.monData = mons[0];
        enemy.mon = mons[0];
        StartCoroutine(hex.SetUpPicture());
    }
}
