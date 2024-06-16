using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShopUIManager : MonoBehaviour
{
    // public GameObject test;
    public GameObject[] upgradesGO = new GameObject[5];
    readonly int[,,] upgradeCosts = new int[,,]{
        {
            //zebra                  //warning lights      //speed bump            //bubble
            {5,10,15,25,0,0,0,0,0}, {60,0,0,0,0,0,0,0,0}, {15,25,40,50,0,0,0,0,0}, {15,25,40,50,0,0,0,0,0}
        },
        {
            {15,25,30,40,45,50,0,0,0}, {85,0,0,0,0,0,0,0,0}, {25,35,45,60,0,0,0,0,0}, {30,40,65,0,0,0,0,0,0}
        },
        {
            {30,35,45,50,55,65,70,80,80}, {150,0,0,10,0,0,0,0,0}, {40,50,60,70,0,0,0,0,0}, {40,55,75,100,0,0,0,0,0}
        }
    };     
    readonly int[,] upgradesLength = { {6, 1, 3, 3}, { 6, 1, 3, 3}, {8, 1, 4, 4}};

    public TMP_Text carrotsText;
    int upgradeLevelIndex;
    Color greenLight = new(0.6f, 0.8980393f, 0.8078432f, 1);
    public bool[] hasSeenLvlStory = new bool[3];
    
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
        hasSeenLvlStory[0] = PlayerPrefs.GetInt("HasSeenLvl1Story", 0) == 1;
        hasSeenLvlStory[1] = PlayerPrefs.GetInt("HasSeenLvl2Story", 0) == 1;
        hasSeenLvlStory[2] = PlayerPrefs.GetInt("HasSeenLvl3Story", 0) == 1;
        Time.timeScale = 1;
        upgradeLevelIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex - 4;
        carrotsText.text = PlayerPrefs.GetInt("Carrots", 0).ToString();
        RefreshUpgrades();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)){
            Debug.Log("money");
            PlayerPrefs.SetInt("Carrots", 1000);
        }
    }
    public void Buy(Transform button){
        int index = button.GetSiblingIndex();
        int upgrade = button.parent.GetSiblingIndex()-1;
        int level = PlayerPrefs.GetInt("Upgrade" + upgradeLevelIndex + upgrade + "Level", 0);
        if (PlayerPrefs.GetInt("Carrots") >= upgradeCosts[upgradeLevelIndex,upgrade, level])
        {
            GetComponent<ShopSFXManager>().BuySFX();
            //minus carrot
            PlayerPrefs.SetInt("Carrots", PlayerPrefs.GetInt("Carrots") - upgradeCosts[upgradeLevelIndex,upgrade, level]);
            //upgrade levels ++
            PlayerPrefs.SetInt("Upgrade" + upgradeLevelIndex + upgrade + "Level", level + 1);
            Debug.Log("Upgrade" + upgradeLevelIndex + upgrade + index);
            //upgrade bool/int
            PlayerPrefs.SetInt("Upgrade" + upgradeLevelIndex + upgrade + index, 1);
            carrotsText.text = PlayerPrefs.GetInt("Carrots", 0).ToString();
            RefreshUpgrades();
        }else{
            GetComponent<ShopSFXManager>().NotEnoughMoney();
        }
    }

    void RefreshUpgrades(){
        RefreshZebra();
        RefreshPolice();
        RefreshSpeedBump();
        RefreshTower();
        RefreshCosts();
    }

    void RefreshZebra(){
        for(int i = 0; i < upgradesLength[upgradeLevelIndex,0]; i++){
            bool unlocked = PlayerPrefs.GetInt("Upgrade" + upgradeLevelIndex + 0 + i, 0) == 1? true : false;
            Color color = unlocked? Color.white : Color.gray;
            upgradesGO[0].transform.GetChild(i).GetComponent<SpriteRenderer>().color = color;
            if(upgradesGO[0].transform.GetChild(i).gameObject.activeSelf){
                transform.GetChild(1).GetChild(i).gameObject.SetActive(!unlocked);
            }else{
                transform.GetChild(1).GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    void RefreshPolice(){
        bool unlocked = PlayerPrefs.GetInt("Upgrade" + upgradeLevelIndex + 1 + 0, 0) == 1? true : false;
        Color color = unlocked? Color.white : Color.gray;
        for(int i = 0; i < 5; i++){
            upgradesGO[1].transform.GetChild(0).GetChild(i).GetComponent<SpriteRenderer>().color = color;
        }
        transform.GetChild(2).GetChild(0).gameObject.SetActive(!unlocked);
    }
    void RefreshSpeedBump(){
        for(int i = 0; i < upgradesLength[upgradeLevelIndex,2]; i++){
            bool unlocked = PlayerPrefs.GetInt("Upgrade" + upgradeLevelIndex + 2 + i, 0) == 1? true : false;
            Color color = unlocked? Color.white : Color.gray;
            for(int j = 0; j < 3; j++){
                for(int k = 0; k < 3; k++){
                    upgradesGO[2].transform.GetChild(i).GetChild(j).GetChild(k).GetComponent<SpriteRenderer>().color = color;
                }
            }
            if(upgradesGO[2].transform.GetChild(i).gameObject.activeSelf){
                transform.GetChild(3).GetChild(i).gameObject.SetActive(!unlocked);
            }else{
                transform.GetChild(3).GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    void RefreshTower(){
        for(int i = 0; i < upgradesLength[upgradeLevelIndex,3]; i++){
            bool unlocked = PlayerPrefs.GetInt("Upgrade" + upgradeLevelIndex + 3 + i, 0) == 1? true : false;
            Color color = unlocked? Color.white : Color.gray;
            Color lightColor = unlocked? greenLight : Color.gray;
            upgradesGO[3].transform.GetChild(i*2).GetComponent<SpriteRenderer>().color = color;
            upgradesGO[3].transform.GetChild(i*2+1).GetComponent<SpriteRenderer>().color = color;
            for(int j = 0; j < 8; j++){
                upgradesGO[4].transform.GetChild(i).transform.GetChild(j).GetComponent<SpriteRenderer>().color = lightColor;
            }
            if(upgradesGO[3].transform.GetChild(i*2).gameObject.activeSelf){
                transform.GetChild(4).GetChild(i).gameObject.SetActive(!unlocked);
            }else{
                transform.GetChild(4).GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    void RefreshCosts(){
        for (int i = 0; i < 4; i++)
        {
            int level = PlayerPrefs.GetInt("Upgrade" + upgradeLevelIndex + i + "Level", 0);
            int cost = upgradeCosts[upgradeLevelIndex,i, level];
            for(int j = 0; j < upgradesLength[upgradeLevelIndex,i]; j++){
                transform.GetChild(i+1).transform.GetChild(j).GetChild(0).GetComponent<TMP_Text>().text = cost.ToString();
            }
        }
    }

    public void PlayButton(){
        if(!hasSeenLvlStory[upgradeLevelIndex]){
            Debug.Log("Playing Story");
            hasSeenLvlStory[upgradeLevelIndex] = true;
            PlayerPrefs.SetInt("HasSeenLvl" + (upgradeLevelIndex+1) + "Story", 1);
            GetComponent<LevelStory>().StartStory(upgradeLevelIndex);
            return;
        }
        GetComponent<ShopSFXManager>().Play();
        StartCoroutine(PlayCoroutine());
    }
    IEnumerator PlayCoroutine(){
        yield return new WaitForSeconds(0.5f);
        Play();
    }

    public void ShopHover(Transform button){
        int index = button.GetSiblingIndex();
        int upgrade = button.parent.GetSiblingIndex()-1;
        if(upgrade == 1){
            for(int k = 0; k < 5; k++){
                upgradesGO[upgrade].transform.GetChild(index).GetChild(k).GetComponent<SpriteRenderer>().color = Color.white;
            }
        }else if(upgrade == 2){
            for(int i = 0; i < 3; i++){
                for(int j = 0; j < 3; j++){
                    upgradesGO[upgrade].transform.GetChild(index).GetChild(i).GetChild(j).GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }else if(upgrade == 3){
            upgradesGO[upgrade].transform.GetChild(index*2).GetComponent<SpriteRenderer>().color = Color.white;
            upgradesGO[upgrade].transform.GetChild(index*2+1).GetComponent<SpriteRenderer>().color = Color.white;
            for(int k = 0; k < 8; k++){
                upgradesGO[upgrade+1].transform.GetChild(index).transform.GetChild(k).GetComponent<SpriteRenderer>().color = greenLight;
            }
        }else{
            upgradesGO[upgrade].transform.GetChild(index).GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void ShopHoverLeave(Transform button){
        int index = button.GetSiblingIndex();
        int upgrade = button.parent.GetSiblingIndex()-1;
        if(upgrade == 1){
            for(int k = 0; k < 5; k++){
                upgradesGO[upgrade].transform.GetChild(index).GetChild(k).GetComponent<SpriteRenderer>().color = Color.gray;
            }
        }else if(upgrade == 2){
            for(int i = 0; i < 3; i++){
                for(int j = 0; j < 3; j++){
                    upgradesGO[upgrade].transform.GetChild(index).GetChild(i).GetChild(j).GetComponent<SpriteRenderer>().color = Color.gray;
                }
            }
        }else if(upgrade == 3){
            upgradesGO[upgrade].transform.GetChild(index*2).GetComponent<SpriteRenderer>().color = Color.gray;
            upgradesGO[upgrade].transform.GetChild(index*2+1).GetComponent<SpriteRenderer>().color = Color.gray;
            for(int k = 0; k < 8; k++){
                upgradesGO[upgrade+1].transform.GetChild(index).transform.GetChild(k).GetComponent<SpriteRenderer>().color = Color.gray;
            }
        }else{
            upgradesGO[upgrade].transform.GetChild(index).GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }

    void Play(){
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
