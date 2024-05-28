using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public CarSpawner carSpawner;
    public int redLightDirection;
    public GameObject[] pathGameObject = new GameObject[4];
    private void OnEnable()
    {
        Unblock();
        UpdateRedLines();
    }

    public void Block(int index){
        // Block the path
        index /= 2;
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

    public void Unblock(){
        // Unblock the path
        carSpawner.blockedPath[0] = false;
        carSpawner.blockedPath[1] = false;
        carSpawner.blockedPath[2] = false;
        carSpawner.blockedPath[3] = false;
        redLightDirection = -1;
        UpdateRedLines();
    }

    void UpdateRedLines(){
        for(int i = 0; i < 4; i++){
            if(i == redLightDirection){
                pathGameObject[i].GetComponent<SpriteRenderer>().color = new Color(1,0,0,1);
            }else{
                pathGameObject[i].GetComponent<SpriteRenderer>().color = new Color(0,1,0,1);
            }
        }
    }
}
