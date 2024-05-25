using System;
//using System.Numerics;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    public UpgradesManager upgradesManager;
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
            GameObject spawnedCar = Instantiate(carPrefab, spawns[randomSpawn].spawnPoint.transform.position, Quaternion.Euler(0,0,Mathf.Atan2(spawns[randomSpawn].direction.y, spawns[randomSpawn].direction.x)*Mathf.Rad2Deg - 90f));
            int randomType = UnityEngine.Random.Range(0, 3);
            spawnedCar.GetComponent<Car>().carType = randomType;
            if(upgradesManager.unlockedUpgrades[1]){
                //Blink Warning Light
                upgradesManager.upgradesGO[1].transform.GetChild(randomSpawn).GetComponent<WarningLight>().Blink(randomType);
            }
            if(upgradesManager.unlockedUpgrades[2]){
                Debug.Log("Has Speedbump");
                spawnedCar.GetComponent<Car>().hasSpeedBump = true;
            }
            spawnedCar.GetComponent<Car>().SetCar();
            spawnedCar.GetComponent<Car>().direction = spawns[randomSpawn].direction.normalized;

        }
    }
}
