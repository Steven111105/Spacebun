using System;
using System.Collections;
using System.Threading;


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
    public float turnDelay = 0.1f;
    public float turnDelayWithSpeedBump = 0.1f;
    
    private void OnEnable()
    {   
        StartCoroutine(SpawnCar());
    }

    IEnumerator SpawnCar(){
        while(true){
            //4*e^(-0.005t) + 1
            yield return new WaitForSeconds(4*MathF.Exp(-0.005f*Time.timeSinceLevelLoad) + 1f);
            Debug.Log("Spawning Car");
            int randomSpawn = UnityEngine.Random.Range(0, spawns.Length);
            GameObject spawnedCar = Instantiate(carPrefab, spawns[randomSpawn].spawnPoint.transform.position, Quaternion.identity);
            
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
            spawnedCar.GetComponent<Car>().turnDelay = turnDelay;
            spawnedCar.GetComponent<Car>().turnDelayWithSpeedBump = turnDelayWithSpeedBump;
            spawnedCar.GetComponent<Car>().direction = spawns[randomSpawn].direction.normalized;
            spawnedCar.GetComponent<Car>().SetCar();
        }
    }
}
