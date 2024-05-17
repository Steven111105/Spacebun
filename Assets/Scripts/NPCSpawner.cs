using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;

    [Serializable]
    class SpawnDatas{
        //Spawn Point
        public GameObject spawnPoint;
        //Zebra Cross that can be chosen
        public GameObject[] zebraStops;
        //Vector2 direction to the zebra cross
        public Vector2[] direction;
    }
    [SerializeField]
    SpawnDatas[] spawns;
    void OnEnable()
    {
        //For each spawn point
        for (int i = 0; i < spawns.Length; i++)
        {
            //For each zebra cross from each spawn point
            for (int j = 0; j < spawns[i].zebraStops.Length; j++)
            {
                //Set the direction from spawn point to zebra cross
                spawns[i].direction[j] = new Vector2(spawns[i].zebraStops[j].transform.position.x - spawns[i].spawnPoint.transform.position.x, spawns[i].zebraStops[j].transform.position.y - spawns[i].spawnPoint.transform.position.y).normalized;
            }
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            //random location
            int randomSpawn = UnityEngine.Random.Range(0, spawns.Length);
            //random direction from that location (can have multiple per location)
            //picking a zebra cross to move towards
            int randomDirection = UnityEngine.Random.Range(0, spawns[randomSpawn].direction.Length);
            
            GameObject spawnedNPC = Instantiate(npcPrefab, spawns[randomSpawn].spawnPoint.transform.position, Quaternion.identity);
            spawnedNPC.GetComponent<NPC>().npcType = UnityEngine.Random.Range(0, 2);
            spawnedNPC.GetComponent<NPC>().direction = spawns[randomSpawn].direction[randomDirection];
        }
    }
}
