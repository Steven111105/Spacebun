using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public int npcType;
    public Vector2 direction;
    public float speed;
    public bool canDash;
    Rigidbody2D rb;
    bool stopping = false;
    public bool gettingHelp = false;
    public GameObject endTrigger;
    bool hasEnd = false;
    private void OnEnable()
    {
        gettingHelp = false;
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetNPC(){
        if (npcType == 0)
        {
            speed = 1.5f;
            canDash = false;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (npcType == 1)
        {
            speed = 3;
            canDash = true;
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(!stopping){
            rb.velocity = direction * speed;
        }
        
        if(stopping){
            
        }
    }

    IEnumerator DestroyNPC(){
        transform.parent.GetComponent<PlayerMovement>().helping = false;
        transform.parent.GetComponent<PlayerMovement>().helpTarget = null;
        transform.parent.GetComponent<PlayerMovement>().SetDefaultSpeed();
        transform.parent = null;
        rb.velocity = direction * speed;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
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

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     Debug.Log(other.gameObject.name);
    //     if (other.gameObject.CompareTag("Destroy"))
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("enter " + other.gameObject.name);
        if (other.gameObject.CompareTag("ZebraStop") && !hasEnd)
        {
            rb.velocity = Vector2.zero;
            stopping = true;
            endTrigger = other.gameObject.GetComponent<ZebraCross>().endTrigger;
            hasEnd = true;
        }else if(gettingHelp && other.gameObject.CompareTag("ZebraExit") && (other.gameObject.name == endTrigger.name)){
            StartCoroutine(DestroyNPC());
        }else if (other.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
        // else if (other.gameObject.CompareTag("ZebraTrigger"))
        // {
        //     direction = other.gameObject.GetComponent<ZebraCross>().direction;
        // }
    }
}
