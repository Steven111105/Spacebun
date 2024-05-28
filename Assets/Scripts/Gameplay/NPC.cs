using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class NPC : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    UIManager uiManager;
    public int npcType;
    public Vector2 direction;
    public float speed;
    public bool canDash;
    Rigidbody2D rb;
    bool stopping = false;
    public bool gettingHelp = false;
    public GameObject endTrigger;
    GameObject patienceBar;
    [SerializeField]
    int waitingTime = 0;
    bool hasEnd = false;
    private void Awake()
    {
        patienceBar = transform.GetChild(0).gameObject;
        gettingHelp = false;
        rb = GetComponent<Rigidbody2D>();
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        animator = GetComponent<Animator>();
        waitingTime = 0;
    }

    public void SetNPC(){
        //biasa = speed normal, dash
        //kecil = speed fast, dash
        //tua = speed slow, no dash
        //blind = speed normal, no dash
        if (npcType == 0)
        {
            speed = 2;
            canDash = false;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (npcType == 1)
        {
            speed = 3;
            canDash = true;
            GetComponent<SpriteRenderer>().color = Color.blue;
        }else if (npcType == 2)
        {
            speed = 1;
            canDash = false;
            GetComponent<SpriteRenderer>().color = Color.green;
        }else if (npcType == 3)
        {
            speed = 1.5f;
            canDash = false;
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(!stopping){
            rb.velocity = direction * speed;
        }
    }
    void MakeVector(Transform origin, Transform Target){
        direction = (Target.position - origin.position).normalized;
    }

    void DestroyNPC(){
        if(gettingHelp){
            transform.parent.GetComponent<PlayerMovement>().helping = false;
            transform.parent.GetComponent<PlayerMovement>().helpTarget = null;
            transform.parent.GetComponent<PlayerMovement>().SetDefaultSpeed();
            transform.parent = null;
        }
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        uiManager.AddScore(100 + (10-waitingTime)*10);
        uiManager.AddCarrot(2 + Random.Range(0, 3));
        rb.velocity = direction * speed;
        Destroy(gameObject,0.5f);
    }
    public void Hit(){
        StartCoroutine(HitRoutine());
    }

    IEnumerator HitRoutine(){
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        for(int i = 0; i<5;i++){
            sr.enabled = false;
            yield return new WaitForSeconds(0.2f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
    }
    IEnumerator Waiting(){
        // Debug.Log("Waiting");
        while(stopping){
            if(!gettingHelp){
                patienceBar.SetActive(true);
                yield return new WaitForSeconds(1f);
                waitingTime ++;
                patienceBar.transform.localScale = new Vector3((10-waitingTime)*0.3f, patienceBar.transform.localScale.y, 1);
                if(waitingTime >= 10){
                    // Debug.Log("Waiting time is over");
                    stopping = false;
                    patienceBar.SetActive(false);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("enter " + other.gameObject.name);
        if (other.gameObject.CompareTag("ZebraStop") && !hasEnd)
        {
            rb.velocity = Vector2.zero;
            endTrigger = other.gameObject.GetComponent<ZebraCross>().endTrigger;
            MakeVector(transform, endTrigger.transform);
            if(other.gameObject.GetComponent<ZebraCross>().blindStop || npcType != 3){
                stopping = true;
            }
            hasEnd = true;
            patienceBar.SetActive(true);

            StartCoroutine(Waiting());
        }else if(hasEnd && other.gameObject.CompareTag("ZebraExit") && (other.gameObject.name == endTrigger.name)){
            // Debug.Log("Reached Zebra Exit");
            patienceBar.SetActive(false);
            DestroyNPC();
        }else if (other.gameObject.CompareTag("Destroy")){
            Destroy(gameObject);
        }
        // else if (other.gameObject.CompareTag("ZebraTrigger"))
        // {
        //     direction = other.gameObject.GetComponent<ZebraCross>().direction;
        // }
    }
}
