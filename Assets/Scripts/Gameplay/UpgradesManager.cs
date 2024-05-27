using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    //This is the script where we will manage the upgrades while playing
    public GameObject[] upgradesGO = new GameObject[4];
    readonly int[] upgradesLength = new int[] { 8, 1, 4, 4 };
    public bool[,] unlockedUpgrades = new bool[4,8];
    
    int upgradeLevelIndex ;
    private void OnEnable()
    {
        upgradeLevelIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex-1;
        for(int i = 0; i < 4; i++){
            for(int j = 0; j < upgradesLength[i]; j++){
                if(i == 1){
                    upgradesGO[i].SetActive(false);
                }else if(i != 0){
                    upgradesGO[i].transform.GetChild(j).gameObject.SetActive(false);
                }
                if(PlayerPrefs.GetInt("Upgrade" + upgradeLevelIndex + i + j, 0) == 1){
                    unlockedUpgrades[i,j] = true;
                }
            }
        }
        
        for (int i = 0; i < 4; i++)
        {
            for(int j = 0; j < upgradesLength[i]; j++){
                if(i == 0){
                    upgradesGO[i].transform.GetChild(j).gameObject.SetActive(true);
                    if(unlockedUpgrades[i,j]){
                        upgradesGO[i].transform.GetChild(j).GetComponent<ZebraCross>().blindStop = true;
                    }
                }else if(PlayerPrefs.GetInt("Upgrade" + upgradeLevelIndex + i + j, 0) == 1){
                    if(i == 1){
                        upgradesGO[i].SetActive(true);
                    }
                    upgradesGO[i].transform.GetChild(j).gameObject.SetActive(true);
                }
            }
        }
    }
}
