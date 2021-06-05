using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    [SerializeField] GameObject[] chestSpawnPoints;
    [SerializeField] GameObject[] doorSpawnPoints;

    [SerializeField] GameObject chest;
    [SerializeField] GameObject[] doors;
    [SerializeField] GameObject wall;

    // Start is called before the first frame update
    void Start()
    {
        SpawnChest();
        SpawnDoor();
    }

    void SpawnChest() {
        // get chestSpawnPoints random index and instantiate there chest
        int randomIndex = Random.Range(0, chestSpawnPoints.Length);
        Instantiate(chest, chestSpawnPoints[randomIndex].transform.position, chestSpawnPoints[randomIndex].transform.rotation);
    }

    void SpawnDoor() {
        int randomWallIndex = Random.Range(0, doorSpawnPoints.Length);
        int randomDoorIndex = Random.Range(0, doors.Length);

        for(int i = 0; i < doorSpawnPoints.Length; i++) {
            if(i == randomWallIndex) { // instantiate door
                var door = Instantiate(doors[randomDoorIndex], doorSpawnPoints[randomWallIndex].transform.position, doors[randomDoorIndex].transform.rotation);
                door.transform.parent = doorSpawnPoints[randomWallIndex].transform;
                door.transform.localRotation = Quaternion.identity;
            } else { // instantiate walls
                var newWall = Instantiate(wall, doorSpawnPoints[i].transform.position, wall.transform.rotation);
                newWall.transform.parent = doorSpawnPoints[i].transform;
                newWall.transform.localRotation = Quaternion.identity;
            }
        }
    }
}
