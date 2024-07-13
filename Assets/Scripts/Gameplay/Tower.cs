using UnityEngine;

public class Tower : MonoBehaviour
{
    TowerManager towerManager;
    bool activated;
    bool canActivate;
    private void OnEnable(){
        towerManager = transform.parent.GetComponent<TowerManager>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)&& canActivate){
            if(!activated){
                towerManager.Block(transform.GetSiblingIndex()/2);
            }else{
                towerManager.Unblock();
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
}
