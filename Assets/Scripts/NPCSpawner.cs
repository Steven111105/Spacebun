using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;
    [Serializable]
    class SpawnDatas{
        public GameObject spawnPoint;
        public GameObject[] zebraStops;
        public Vector2[] direction;
    }
    [SerializeField]
    SpawnDatas[] spawns;
    public float movespeed = 5;

    void OnEnable()
    {
        for (int i = 0; i < spawns.Length; i++)
        {
            for (int j = 0; j < spawns[i].zebraStops.Length; j++)
            {
                spawns[i].direction[j] = new Vector2(spawns[i].zebraStops[j].transform.position.x - spawns[i].spawnPoint.transform.position.x, spawns[i].zebraStops[j].transform.position.y - spawns[i].spawnPoint.transform.position.y).normalized;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            //random location
            int randomSpawn = UnityEngine.Random.Range(0, spawns.Length);
            //random direction from that location (can have multiple per location)
            int randomDirection = UnityEngine.Random.Range(0, spawns[randomSpawn].direction.Length);
            
            GameObject spawnedNPC = Instantiate(npcPrefab, spawns[randomSpawn].spawnPoint.transform.position, Quaternion.identity);
            spawnedNPC.GetComponent<NPC>().npcType = UnityEngine.Random.Range(0, 2);
            spawnedNPC.GetComponent<NPC>().direction = spawns[randomSpawn].direction[randomDirection];
        }
    }
}
