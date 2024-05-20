using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Car : MonoBehaviour
{
    CarSpawner carSpawner;
    public int carType;
    public float movespeed;
    public Vector2 direction;
    bool hasSwitchedDirection;
    Rigidbody2D rb;
    public float cometDelay;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void SetCar(){
        hasSwitchedDirection = true;
        if(carType == 0)
        {
            //meteor
            movespeed = 2;
            transform.localScale = new Vector3(2.5f, 2.5f, 1f);
        }
        else if(carType == 1)
        {
            //comet
            movespeed = 5;
            hasSwitchedDirection = false;
            carSpawner = GameObject.Find("Spawner").GetComponent<CarSpawner>();
            transform.localScale = new Vector3(1.5f, 3f, 1f);
        }else if(carType == 2){
            //idk
        }
    }
    IEnumerator CometTurn(float seconds){
        if(!hasSwitchedDirection){
            Debug.Log("Comet Switched Direction");
            hasSwitchedDirection = true;
            int randomSpawn = Random.Range(0, carSpawner.spawns.Length);
            yield return new WaitForSeconds(seconds);
            transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(carSpawner.spawns[randomSpawn].direction.y, carSpawner.spawns[randomSpawn].direction.x)*Mathf.Rad2Deg - 90f);
            direction = carSpawner.spawns[randomSpawn].direction.normalized;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * movespeed;    
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"||other.gameObject.tag == "NPC"){
            Debug.Log("Player Hit");
            // Destroy(gameObject);
        }else if(other.gameObject.tag == "CometTurn"){
            if(carType == 1){
                StartCoroutine(CometTurn(0.37f));
            }
        }
    }
}
