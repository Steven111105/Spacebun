using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public CarSpawner carSpawner;
    public GameObject redLines;
    public int redLightDirection;
    public int activatedTower;
    public int[] redLightTimer = new int[4];
    Color greenLight = new Color(0.6f, 0.8980393f, 0.8078432f, 1);
    private void OnEnable()
    {
        activatedTower = -1;
        redLightDirection = -1;
        StartCoroutine(RedLightTimer());
        Unblock();
        UpdateRedLines();
    }
    IEnumerator RedLightTimer(){
        while(true){
            for(int i = 0; i < 4; i++){
                if(i != redLightDirection){
                    redLightTimer[i]++;
                    if(redLightTimer[i] >= 8){
                        redLightTimer[i] = 8;
                    }
                }else{
                    redLightTimer[i]--;
                    if(redLightTimer[i] < 0){
                        redLightTimer[i] = 0;
                        redLightDirection = -1;
                        activatedTower = -1;
                        Unblock();
                    }
                    
                }
            }
            yield return new WaitForSeconds(1.25f);
            UpdateRedLines();
        }
    }

    public void Block(int index){
        if(activatedTower == -1){
            // Debug.Log("activated " + activatedTower + " index " + index);
            // Block the path
            activatedTower = index;
            redLightDirection = index;  
            switch(index){
                case 0:
                    carSpawner.blockedPath[0] = true;
                    carSpawner.blockedPath[2] = true;
                    break;
                case 1:
                    carSpawner.blockedPath[1] = true;
                    carSpawner.blockedPath[3] = true;
                    break;
                case 2:
                    carSpawner.blockedPath[2] = true;
                    carSpawner.blockedPath[0] = true;
                    break;
                case 3:
                    carSpawner.blockedPath[1] = true;
                    carSpawner.blockedPath[3] = true;
                    break;
            }
            UpdateRedLines();
        }
    }

    public void Unblock(){
        // Unblock the path
        carSpawner.blockedPath[0] = false;
        carSpawner.blockedPath[1] = false;
        carSpawner.blockedPath[2] = false;
        carSpawner.blockedPath[3] = false;
        activatedTower = -1;
        redLightDirection = -1;
        UpdateRedLines();
    }

    void UpdateRedLines(){
        for(int i = 0; i < 4; i++){
            for(int j = 0; j < 8; j++){
                if(i == redLightDirection){
                    if(j <= redLightTimer[i]-1){
                        redLines.transform.GetChild(i).transform.GetChild(j).GetComponent<SpriteRenderer>().color = Color.red;
                    }else{
                        redLines.transform.GetChild(i).transform.GetChild(j).GetComponent<SpriteRenderer>().color = Color.yellow;

                    }
                }else{
                    if(j >= redLightTimer[i]){
                        redLines.transform.GetChild(i).transform.GetChild(j).GetComponent<SpriteRenderer>().color = Color.yellow;
                    }else{
                        redLines.transform.GetChild(i).transform.GetChild(j).GetComponent<SpriteRenderer>().color = greenLight;
                    }
                }
            }
        }
    }
}
