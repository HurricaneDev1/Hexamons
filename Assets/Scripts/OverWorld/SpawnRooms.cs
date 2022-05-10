using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRooms : MonoBehaviour
{
    [SerializeField]private List<Transform> points = new List<Transform>();
    [SerializeField]private List<GameObject> rooms = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform spawnPoint in points){
            Instantiate(rooms[0],spawnPoint.position,Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
