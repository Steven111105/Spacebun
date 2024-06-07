using System;
using System.Collections;
using System.Data;
using System.Threading;


//using System.Numerics;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public UIManager uiManager;
    public bool[] blockedPath = new bool [4];
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
    int level;
    int randomSpawn;
    
    private void Awake()
    {   
        StartCoroutine(SpawnCar());
        level = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
    }

    IEnumerator SpawnCar(){
        while(true){
            //4*e^(-0.005t) + 1
            yield return new WaitForSeconds(2*MathF.Exp(-0.005f*Time.timeSinceLevelLoad) + 4f);
            if(level == 1){
                randomSpawn = UnityEngine.Random.Range(0, 2);
                if(randomSpawn == 1){
                    randomSpawn = 2;
                }
            }else if(level == 2){
                randomSpawn = UnityEngine.Random.Range(0, 3);
            }else{
                randomSpawn = UnityEngine.Random.Range(0, 4);
            }
                
            // Debug.Log("Random Spawn: " + randomSpawn);
            if(!blockedPath[randomSpawn]){
                GameObject spawnedCar = Instantiate(carPrefab, spawns[randomSpawn].spawnPoint.transform.position, Quaternion.identity);
                // Debug.Log("Spawning Car");
                
                int randomType = UnityEngine.Random.Range(0, 3);
                spawnedCar.GetComponent<Car>().carType = randomType;
                
                if(upgradesManager.unlockedUpgrades[1,0]){
                    //Blink Warning Light
                    upgradesManager.upgradesGO[1].transform.GetChild(0).GetComponent<LightManager>().Blink(randomSpawn, randomType);
                }
                if(upgradesManager.unlockedUpgrades[2,randomSpawn]){
                    spawnedCar.GetComponent<Car>().hasSpeedBump = true;
                }
                spawnedCar.GetComponent<Car>().turnDelay = turnDelay;
                spawnedCar.GetComponent<Car>().turnDelayWithSpeedBump = turnDelayWithSpeedBump;
                spawnedCar.GetComponent<Car>().direction = spawns[randomSpawn].direction.normalized;
                uiManager.pause.AddListener(spawnedCar.GetComponent<Car>().Pause);
                uiManager.unpause.AddListener(spawnedCar.GetComponent<Car>().Unpause);
                uiManager.gameOver.AddListener(spawnedCar.GetComponent<Car>().GameOver);
                spawnedCar.GetComponent<Car>().SetCar();
            }
        }
    }
}
