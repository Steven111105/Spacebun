using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public GameObject[] upgradesGO = new GameObject[4];
    public bool[] unlockedUpgrades = new bool[4];
    private void OnEnable()
    {
        for(int i = 0; i < 4; i++){
            upgradesGO[i].SetActive(false);
        }
        //upgrade 1 = zebra cross for blind
        //upgrade 2 = Warning lights
        //upgrade 3 = speed bump
        //upgrade 4 = bubble thing
        for (int i = 0; i < unlockedUpgrades.Length; i++)
        {
            unlockedUpgrades[i] = PlayerPrefs.GetInt("Upgrade" + i, 0) == 1;
            upgradesGO[i].SetActive(unlockedUpgrades[i]);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
