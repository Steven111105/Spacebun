using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class ShopUIManager : MonoBehaviour
{
    // public GameObject test;
    public GameObject[] upgradesGO = new GameObject[4];
    public int[,] upgradeCosts = new int[4,9]{
        {5,10,15,25,30,35,50,55,55},
        {60,0,10,10,0,0,0,0,0},
        {20,25,40,50,0,0,0,0,0},
        {20,25,40,50,0,0,0,0,0}
    }; 
    readonly int[,] upgradesLength = { {4, 1, 4, 4}, { 6, 1, 4, 6}, {8, 1, 4, 8}};
    public TMP_Text carrotsText;
    int upgradeLevelIndex;
    private void OnEnable()
    {
        // Debug.Log(test.transform.GetSiblingIndex());
        //Scenes
        //0 = Main Menu
        //1 = Level 1
        //2 = Level 2
        //3 = Level 3
        //4 = Shop Lvl 1    //upgradeLevelIndex = 4-4 = 0
        //5 = Shop Lvl 2
        //6 = Shop Lvl 3
        Time.timeScale = 1;
        upgradeLevelIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex - 4;
        carrotsText.text = PlayerPrefs.GetInt("Carrots", 0).ToString();
        RefreshUpgrades();
    }

    public void Buy(Transform button){
        int index = button.GetSiblingIndex();
        int upgrade = button.parent.GetSiblingIndex()-1;
        int level = PlayerPrefs.GetInt("Upgrade" + upgradeLevelIndex + upgrade + "Level", 0);
        if (PlayerPrefs.GetInt("Carrots") >= upgradeCosts[upgrade, level])
        {
            GetComponent<ShopSFXManager>().BuySFX();
            PlayerPrefs.SetInt("Carrots", PlayerPrefs.GetInt("Carrots") - upgradeCosts[upgrade, level]);
            PlayerPrefs.SetInt("Upgrade" + upgradeLevelIndex + upgrade + "Level", level + 1);
            Debug.Log("Upgrade" + upgradeLevelIndex + upgrade + index);
            PlayerPrefs.SetInt("Upgrade" + upgradeLevelIndex + upgrade + index, 1);
            carrotsText.text = PlayerPrefs.GetInt("Carrots", 0).ToString();
            RefreshUpgrades();
        }else{
            GetComponent<ShopSFXManager>().NotEnoughMoney();
        }
    }

    void RefreshUpgrades(){
        for (int i = 0; i < 4; i++)
        {
            int level = PlayerPrefs.GetInt("Upgrade" + upgradeLevelIndex + i + "Level", 0);
            int cost = upgradeCosts[i, level];
            for(int j = 0; j < upgradesLength[upgradeLevelIndex,i]; j++){
                Debug.Log("upgrade" + i + " index" + j);
                //upgrade 0 = zebra cross for blind
                //upgrade 1 = Warning lights
                //upgrade 2 = speed bump
                //upgrade 3 = bubble thing
                if(PlayerPrefs.GetInt("Upgrade" + upgradeLevelIndex + i + j, 0) == 1){
                    upgradesGO[i].transform.GetChild(j).gameObject.SetActive(true);
                    upgradesGO[i].transform.GetChild(j).GetComponent<SpriteRenderer>().color = Color.white;
                    //deactivating button from shop
                    transform.GetChild(i+1).transform.GetChild(j).gameObject.SetActive(false);
                }else{
                    if(i != 1){
                        upgradesGO[i].transform.GetChild(j).GetComponent<SpriteRenderer>().color = Color.gray;
                    }else{
                        //Find a way to make police grayed out
                    }
                    //activating button from shop
                    transform.GetChild(i+1).transform.GetChild(j).gameObject.SetActive(true);
                    transform.GetChild(i+1).transform.GetChild(j).GetChild(0).GetComponent<TMP_Text>().text = cost.ToString();
                }
            }
        }
    }

    public void PlayButton(){
        GetComponent<ShopSFXManager>().Play();
        StartCoroutine(PlayCoroutine());
    }
    IEnumerator PlayCoroutine(){
        Debug.Log("Play");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Play2");
        Play();
    }

    public void Play(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(upgradeLevelIndex + 1);
    }

    public void Menu(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void DeleteAll(){
        PlayerPrefs.DeleteAll();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
