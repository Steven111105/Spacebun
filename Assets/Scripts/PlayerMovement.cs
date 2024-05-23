using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
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
    private void OnEnable(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        speed = defaultSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Speed",movement.Abs().magnitude);
        if(movement.x != 0){
            transform.localScale = new Vector3(-movement.x, transform.localScale.y, 1);
        }

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
            if(helpTarget != null){
                if(touchingNPC && !helping){
                    helping = true;
                    animator.SetBool("Helping", true);
                    animator.SetInteger("HelpType", helpTarget.GetComponent<NPC>().npcType);
                    helpTarget.transform.parent = transform;
                    helpTarget.GetComponent<NPC>().gettingHelp = true;
                    speed = helpTarget.GetComponent<NPC>().speed;
                }else{
                    helping = false;
                    helpTarget.GetComponent<NPC>().gettingHelp = false;
                    helpTarget.GetComponent<NPC>().GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    helpTarget.transform.parent = null;
                    animator.SetInteger("HelpType", -1);
                    speed = defaultSpeed;
                }
            }else{
                helping = false;
                helpTarget = null;
                animator.SetInteger("HelpType", -1);
                speed = defaultSpeed;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = movement.normalized * speed;
        if(helping){
            if(helpTarget != null){
                helpTarget.GetComponent<NPC>().GetComponent<Rigidbody2D>().velocity = movement * speed;
            }else{
                helping = false;
                animator.SetBool("Helping", false);
            }
        }
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
    IEnumerator DashRoutine(){
        canDash = false;
        animator.SetTrigger("Dash");
        speed = dashSpeed;
        yield return new WaitForSeconds(dashLength);
        speed = defaultSpeed;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
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
            animator.SetBool("Helping", true);
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
