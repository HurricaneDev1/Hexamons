using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRooms : MonoBehaviour
{
    [SerializeField]private List<Transform> points = new List<Transform>();
    [SerializeField]private List<Transform> activePoints = new List<Transform>();
    [SerializeField]private List<GameObject> rooms = new List<GameObject>();
    [SerializeField]private Transform startRoom;
    [SerializeField]private Transform nextRoom;
    [SerializeField]private int roomHeight;
    [SerializeField]private int roomWidth;
    [SerializeField]private int numChecks;
    // Start is called before the first frame update
    void Start()
    {
        startRoom = points[Random.Range(0,points.Count)];
        activePoints.Add(startRoom);
        // for(int i = 0; i < numChecks; i++){
        //     SummonRooms();
        // }
    }

    void Update(){
        if(Input.GetKey(KeyCode.K)){
            SummonRooms();
        }
    }
    void SummonRooms(){
        if(CheckAdjacent()){
            activePoints.Add(nextRoom);
            points.Remove(startRoom);
            startRoom = nextRoom;
        }

        int pointCount = activePoints.Count;
        for(int i = 0; i < pointCount; i++){
            if(i == activePoints.Count)break;
            int randomRoom = Random.Range(0,rooms.Count);
            Instantiate(rooms[randomRoom],activePoints[i].position,Quaternion.identity);
            activePoints.Remove(activePoints[i]);
        }
    }

    bool CheckAdjacent(){
        nextRoom = points[Random.Range(0,points.Count)];
        if(nextRoom.position.x == startRoom.position.x && (nextRoom.position.y == (startRoom.position.y - roomHeight) || nextRoom.position.y == (startRoom.position.y + roomHeight))){
            Debug.Log("Above/Below");
            return true;
        }else if(nextRoom.position.y == startRoom.position.y && (nextRoom.position.x == (startRoom.position.x - roomWidth) || nextRoom.position.x == (startRoom.position.x + roomWidth))){
            Debug.Log("On Side");
            return true;
        }else{
            return false;
        }
    }
}
