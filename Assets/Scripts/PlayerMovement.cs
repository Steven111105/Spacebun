using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 movement;
    [SerializeField]
    float speed;
    [SerializeField]
    float defaultSpeed;
    [SerializeField]
    float dashSpeed;
    [SerializeField]
    float dashLength;
    [SerializeField]
    float dashCooldown;
    [SerializeField]
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
                speed = helpTarget.GetComponent<NPC>().speed;
            }else{
                helping = false;
                helpTarget.GetComponent<NPC>().gettingHelp = false;
                helpTarget.GetComponent<NPC>().GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                helpTarget.transform.parent = null;
                speed = defaultSpeed;
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movement.normalized * speed;
        if(helping){
            helpTarget.GetComponent<NPC>().GetComponent<Rigidbody2D>().velocity = movement * speed;
        }
    }
    IEnumerator DashRoutine(){
        canDash = false;
        speed = dashSpeed;
        yield return new WaitForSeconds(dashLength);
        speed = defaultSpeed;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    public void SetDefaultSpeed(){
        speed = defaultSpeed;
    }

    public void Hit(){
        StartCoroutine(HitRoutine());
        if(helping){
            helpTarget.GetComponent<NPC>().Hit();
        }
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

    void Dash(){
        StartCoroutine(DashRoutine());
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Player hit something");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Debug.Log("Player enter trigger");
        if(other.gameObject.CompareTag("NPC") && !helping){
            touchingNPC = true;
            helpTarget = other.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log("Player exit trigger");
        if(other.gameObject.CompareTag("NPC")){
            touchingNPC = false;
        }
    }
}
