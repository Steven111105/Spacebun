using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopUIManager : MonoBehaviour
{
    public GameObject[] upgradesGO = new GameObject[4];
    public bool[] unlockedUpgrades = new bool[4];
    public TMP_Text carrotsText;
    int upgradeLevelIndex;
    private void OnEnable()
    {
        //Scenes
        //0 = Main Menu
        //1 = Level 1
        //2 = Level 2
        //3 = Level 3
        //4 = Shop Lvl 1    //upgradeLevelIndex = 4-4 = 0
        //5 = Shop Lvl 2
        //6 = Shop Lvl 3
        upgradeLevelIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex - 4;
        //upgrade 0 = zebra cross for blind
        //upgrade 1 = Warning lights
        //upgrade 2 = speed bump
        //upgrade 3 = bubble thing
        for(int i = 0; i < 4; i++){
            upgradesGO[i].SetActive(false);
        }
        carrotsText.text = PlayerPrefs.GetInt("Carrots", 0).ToString();
        RefreshUpgrades();
    }

    public void Buy(int upgrade){
        if (PlayerPrefs.GetInt("Carrots") >= 10)
        {
            PlayerPrefs.SetInt("Carrots", PlayerPrefs.GetInt("Carrots") - 10);
            PlayerPrefs.SetInt("Upgrade" + upgradeLevelIndex + upgrade, 1);
            carrotsText.text = PlayerPrefs.GetInt("Carrots", 0).ToString();
            RefreshUpgrades();
        }
    }

    void RefreshUpgrades(){
        for (int i = 0; i < 4; i++)
        {
            if(PlayerPrefs.GetInt("Upgrade" + upgradeLevelIndex + i, 0) == 1){
                //change color instead of set active
                upgradesGO[i].SetActive(true);
                //deactivating button from shop
                transform.GetChild(i+1).gameObject.SetActive(false);
            }
        }
    }

    public void Play(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(upgradeLevelIndex + 1);
    }

    public void Menu(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
