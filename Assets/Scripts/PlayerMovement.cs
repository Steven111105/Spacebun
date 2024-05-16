using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField]
    float speed;
    Vector2 movement;
    [SerializeField]
    float dashLength;
    [SerializeField]
    float dashCooldown;
    bool canDash = true;
    [SerializeField]
    public bool helping;
    public GameObject helpTarget;
    bool touchingNPC = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(canDash){
                if(helping){
                    if(helpTarget.GetComponent<NPC>().canDash){
                        Dash();
                    }
                }else{
                    Dash();
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.E)){
            if(touchingNPC && !helping){
                helping = true;
                helpTarget.transform.parent = transform;
                helpTarget.GetComponent<NPC>().gettingHelp = true;
            }else{
                helping = false;
                helpTarget.transform.parent = null;
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movement * speed;
        if(helping){
            helpTarget.GetComponent<NPC>().GetComponent<Rigidbody2D>().velocity = movement * speed;
        }
    }
    IEnumerator DashRoutine(){
        canDash = false;
        speed = 15;
        yield return new WaitForSeconds(dashLength);
        speed = 5;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;

    }
    void Dash(){
        StartCoroutine(DashRoutine());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("enter");
        if(other.gameObject.CompareTag("NPC")){
            touchingNPC = true;
            helpTarget = other.gameObject;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("exit");
        if(other.gameObject.CompareTag("NPC")){
            touchingNPC = false;
        }
    }
}
