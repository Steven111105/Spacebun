using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tower : MonoBehaviour
{
    TowerManager towerManager;
    TMP_Text timeText;
    bool activated;
    bool canActivate;
    int savedTime;
    int index;
    private void OnEnable(){
        towerManager = transform.parent.GetComponent<TowerManager>();
        index = transform.GetSiblingIndex();
        timeText = transform.GetChild(0).GetComponent<TMP_Text>();
        StartCoroutine(TowerTimer());
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)&& canActivate){
            if(!activated){
                towerManager.Unblock();
                towerManager.Block(transform.GetSiblingIndex());
                activated = true;
            }else{
                towerManager.Unblock();
                activated = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            
            canActivate = false;
        }
    }
    IEnumerator TowerTimer(){
        while(true){
            yield return new WaitForSeconds(1f);
            if(activated){
                savedTime--;
            }else{
                savedTime ++;
            }if(savedTime >10){
                savedTime = 10;
            }
            if(savedTime <= 0){
                towerManager.Unblock();
                activated = false;
            }
            timeText.text = savedTime.ToString();
        }
    }
}
