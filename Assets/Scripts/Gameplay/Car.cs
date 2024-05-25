using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Car : MonoBehaviour
{
    CarSpawner carSpawner;
    public bool hasSpeedBump;
    public int carType;
    public float movespeed;
    public Vector2 direction;
    bool hasSwitchedDirection;
    Rigidbody2D rb;
    public float turnDelay = 0.37f;
    bool hasAttacked;


    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        hasAttacked = false;
    }
    public void SetCar(){
        hasSwitchedDirection = true;
        if(carType == 0)
        {
            //meteor
            if(hasSpeedBump){
                movespeed = 1;
            }else{
                movespeed = 2;
            }
            transform.localScale = new Vector3(3f, 3f, 1f);
        }
        else if(carType == 1)
        {
            //comet
            if(hasSpeedBump){
                movespeed = 10;
            }else{
                movespeed = 15;
            }
            transform.localScale = new Vector3(1.5f, 4f, 1f);
            StartCoroutine(UFODelay());
        }else if(carType == 2){
            //UFO
            if(hasSpeedBump){
                movespeed = 3;
            }else{
                movespeed = 5;
            }
            hasSwitchedDirection = false;
            carSpawner = GameObject.Find("Spawner").GetComponent<CarSpawner>();
            transform.localScale = new Vector3(2f, 2f, 1f);
        }
    }
    IEnumerator UFODelay(){
        movespeed = 0;
        yield return new WaitForSeconds(2f);
        movespeed = 10;
    }
    IEnumerator UFOTurn(float seconds){
        if(!hasSwitchedDirection){
            Debug.Log("UFO Switched Direction");
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
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("NPC"))
        {
            if(!hasAttacked){
                GameObject.Find("Canvas").GetComponent<UIManager>().MinusHealth();
                if(other.gameObject.CompareTag("Player")){
                    hasAttacked = true; 
                    Debug.Log("Player Hit");
                    GameObject.Find("Player").GetComponent<PlayerMovement>().Hit();
                }else{
                    hasAttacked = true;
                    Debug.Log("NPC Hit");
                    other.gameObject.GetComponent<NPC>().Hit();
                }
            }
            // Destroy(gameObject);
        }else if(other.gameObject.CompareTag("CometTurn"))
        {
            if(carType == 2){
                StartCoroutine(UFOTurn(turnDelay));
            }
        }
    }
}
