using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField]
    class SpawnDatas{
        public GameObject spawnPoint;
        public Vector2[] direction;
    }
    [SerializeField]
    SpawnDatas[] spawns;
    
    private void OnEnable()
    {   
        int randomSpawn = UnityEngine.Random.Range(0, spawns.Length);
        int randomDirection = UnityEngine.Random.Range(0, spawns[randomSpawn].direction.Length);
        
        GameObject spawnedCar = Instantiate(spawns[randomSpawn].spawnPoint, spawns[randomSpawn].spawnPoint.transform.position, Quaternion.identity);
        spawnedCar.GetComponent<Car>().carType = UnityEngine.Random.Range(0, 2);
        spawnedCar.GetComponent<Car>().direction = spawns[randomSpawn].direction[randomDirection];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
