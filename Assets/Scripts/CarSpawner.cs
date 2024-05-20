using System;
//using System.Numerics;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    [Serializable]
    public class SpawnDatas{
        public GameObject spawnPoint;
        public Vector2 direction;
    }
    [SerializeField]
    public SpawnDatas[] spawns;
    
    private void OnEnable()
    {   
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            int randomSpawn = UnityEngine.Random.Range(0, spawns.Length);
            Debug.Log("Spawned Car at spawn" + randomSpawn + " with direction " + spawns[randomSpawn].direction);
            GameObject spawnedCar = Instantiate(carPrefab, spawns[randomSpawn].spawnPoint.transform.position, Quaternion.Euler(0,0,Mathf.Atan2(spawns[randomSpawn].direction.y, spawns[randomSpawn].direction.x)*Mathf.Rad2Deg - 90f));
            spawnedCar.GetComponent<Car>().carType = UnityEngine.Random.Range(0, 2);
            Debug.Log("Car Type: " + spawnedCar.GetComponent<Car>().carType);
            spawnedCar.GetComponent<Car>().SetCar();
            spawnedCar.GetComponent<Car>().direction = spawns[randomSpawn].direction.normalized;

        }
    }
}
