using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    //This is the script where we will manage the upgrades while playing
    public GameObject[] upgradesGO = new GameObject[4];
    public bool[] unlockedUpgrades = new bool[4];
    
    int upgradeLevelIndex ;
    private void OnEnable()
    {
        upgradeLevelIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex-1;
        for(int i = 0; i < 4; i++){
            upgradesGO[i].SetActive(false);
        }
        
        for (int i = 0; i < 4; i++)
        {
            if(PlayerPrefs.GetInt("Upgrade" + upgradeLevelIndex + i, 0) == 1){
                //change color instead of set active
                upgradesGO[i].SetActive(true);
                unlockedUpgrades[i] = true;
            }
        }
    }
}
