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
        if (npcType == 0)
        {
            speed = 3;
            canDash = false;
        }
        else if (npcType == 1)
        {
            speed = 5;
            canDash = true;
        }
        rb = GetComponent<Rigidbody2D>();
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
        transform.parent = null;
        rb.velocity = direction * speed;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision");
        if (other.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ZebraStop") && !hasEnd)
        {
            rb.velocity = Vector2.zero;
            stopping = true;
            endTrigger = other.gameObject.GetComponent<ZebraCross>().endTrigger;
            hasEnd = true;
        }else if(gettingHelp && other.gameObject.CompareTag("ZebraExit") && (other.gameObject.name == endTrigger.name)){
            StartCoroutine(DestroyNPC());
        }
        // else if (other.gameObject.CompareTag("ZebraTrigger"))
        // {
        //     direction = other.gameObject.GetComponent<ZebraCross>().direction;
        // }
    }
}
