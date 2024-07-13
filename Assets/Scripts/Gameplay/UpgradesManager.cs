using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    //This is the script where we will manage the upgrades while playing
    public GameObject[] upgradesGO = new GameObject[5];
    public GameObject zebraStops;
    readonly int[,] upgradesLength = { {6, 1, 3, 3}, { 6, 1, 3, 3}, {8, 1, 4, 4}};
    public bool[,] unlockedUpgrades = new bool[4,8];
    
    int upgradeLevelIndex ;
    private void OnEnable()
    {
        upgradeLevelIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex-1;
        for(int i = 0; i < 4; i++){
            for(int j = 0; j < upgradesLength[upgradeLevelIndex,i]; j++){
                if(i == 3){
                    upgradesGO[i].transform.GetChild(j*2).gameObject.SetActive(false);
                    upgradesGO[i].transform.GetChild(j*2+1).gameObject.SetActive(false);
                    for(int k = 0; k < 8; k++){
                        upgradesGO[i+1].transform.GetChild(j).transform.GetChild(k).gameObject.SetActive(false);
                    }
                }else{
                    upgradesGO[i].transform.GetChild(j).gameObject.SetActive(false);
                }
                if(PlayerPrefs.GetInt("Upgrade" + upgradeLevelIndex + i + j, 0) == 1){
                    unlockedUpgrades[i,j] = true;
                }
            }
        }
        
        for (int i = 0; i < 4; i++){
            for(int j = 0; j < upgradesLength[upgradeLevelIndex,i]; j++){
                // Debug.Log("upgrade" + i + " index" + j);
                if(unlockedUpgrades[i,j]){
                    if(upgradeLevelIndex ==0){
                        if(i == 0){
                            if(j == 2 || j == 3){
                                continue;
                            }else{
                                zebraStops.transform.GetChild(j).GetComponent<ZebraCross>().blindStop = true;
                                upgradesGO[i].transform.GetChild(j).gameObject.SetActive(true);
                            }
                        }else if(i == 3){
                            if(j == 1){
                                continue;
                            }else{
                                upgradesGO[i].transform.GetChild(j*2).gameObject.SetActive(true);
                                upgradesGO[i].transform.GetChild(j*2+1).gameObject.SetActive(true);
                                for(int k = 0; k < 8; k++){
                                    upgradesGO[i+1].transform.GetChild(j).transform.GetChild(k).gameObject.SetActive(true);
                                }
                            }
                        }else{
                            if(j != 1){
                                upgradesGO[i].transform.GetChild(j).gameObject.SetActive(true);
                            }
                        }
                    }else{
                        if(i == 0){
                            zebraStops.transform.GetChild(j).GetComponent<ZebraCross>().blindStop = true;
                            upgradesGO[i].transform.GetChild(j).gameObject.SetActive(true);
                        }else if(i == 3){
                            upgradesGO[i].transform.GetChild(j*2).gameObject.SetActive(true);
                            upgradesGO[i].transform.GetChild(j*2+1).gameObject.SetActive(true);
                            for(int k = 0; k < 8; k++){
                                upgradesGO[i+1].transform.GetChild(j).transform.GetChild(k).gameObject.SetActive(true);
                            }
                        }else{
                            upgradesGO[i].transform.GetChild(j).gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
    }
}
