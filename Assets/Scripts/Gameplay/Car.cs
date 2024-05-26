using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditor.Callbacks;
using UnityEngine;

public class Car : MonoBehaviour
{
    public AnimatorController[] animatorControllers = new AnimatorController[2];
    private Animator animator;
    public CarSpawner carSpawner;
    public bool hasSpeedBump;
    public int carType;
    public float movespeed;
    public Vector2 direction;
    bool hasSwitchedDirection;
    Rigidbody2D rb;
    public float turnDelay;
    public float turnDelayWithSpeedBump;
    bool hasAttacked;


    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hasAttacked = false;
    }
    public void SetCar(){
        hasSwitchedDirection = true;
        if(carType == 0)
        {
            animator.runtimeAnimatorController = animatorControllers[0];
            animator.Play("Meteor");
            GetComponent<CircleCollider2D>().enabled = true;
            GetComponent<CircleCollider2D>().radius = 1.187288f;
            //meteor
            if(hasSpeedBump){
                movespeed = 1;
            }else{
                movespeed = 2;
            }
            // transform.localScale = new Vector3(3f, 3f, 1f);
        }
        else if(carType == 1)
        {
            //star
            animator.runtimeAnimatorController = animatorControllers[1];
            Debug.Log(Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg+45f);
            transform.localRotation = Quaternion.Euler(0,0,Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg+45f);

            GetComponent<CircleCollider2D>().radius = 0.37f;
            transform.localScale = new Vector3(1.5f, 1.5f, 1f);
            StartCoroutine(StarDelay());
        }else if(carType == 2){
            //UFO
            animator.runtimeAnimatorController = animatorControllers[0];
            animator.Play("UFO");
            GetComponent<BoxCollider2D>().enabled = true;
            if(hasSpeedBump){
                movespeed = 3;
            }else{
                movespeed = 5;
            }
            hasSwitchedDirection = false;
            carSpawner = GameObject.Find("Spawner").GetComponent<CarSpawner>();
            // transform.localScale = new Vector3(2f, 2f, 1f);
        }
    }
    IEnumerator StarDelay(){
        movespeed = 0;
        yield return new WaitForSeconds(2f);
        if(hasSpeedBump){
            movespeed = 10;
        }else{
            movespeed = 15;
        }
        animator.Play("Star");
    }
    IEnumerator UFOTurn(float seconds){
        if(!hasSwitchedDirection){
            Debug.Log("UFO Switched Direction");
            hasSwitchedDirection = true;
            int randomSpawn = Random.Range(0, carSpawner.spawns.Length);
            yield return new WaitForSeconds(seconds);
            direction = carSpawner.spawns[randomSpawn].direction.normalized;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * movespeed;    
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
                if(hasSpeedBump){
                    StartCoroutine(UFOTurn(turnDelayWithSpeedBump));
                }else{
                    StartCoroutine(UFOTurn(turnDelay));
                }
            }
        }else if (other.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }
}
