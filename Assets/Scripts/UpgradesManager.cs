using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public GameObject[] upgradesGO = new GameObject[4];
    public bool[] unlockedUpgrades = new bool[4];
    private void OnEnable()
    {
        GameObject canvas = GameObject.Find("Canvas");
        for(int i = 0; i < 4; i++){
            upgradesGO[i].SetActive(false);
        }
        //upgrade 0 = zebra cross for blind
        //upgrade 1 = Warning lights
        //upgrade 2 = speed bump
        //upgrade 3 = bubble thing
    }

    void RefreshUpdates(){
        for (int i = 0; i < 4; i++)
        {
            if(PlayerPrefs.GetInt("Upgrade" + i, 0) == 1){
                upgradesGO[i].SetActive(true);
            }
            if(i == 2){
                SpeedBumpUpgrade();
            }
        }
    }
    //TODO speedbump speed reduction
    void SpeedBumpUpgrade(){
        return;
    }
}
