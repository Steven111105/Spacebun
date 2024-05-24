using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public string[] staticDirections = {"IdleUpRight", "IdleUpLeft", "IdleDownRight", "IdleDownLeft"};
    public string[] runDirections = {"UpRight", "UpLeft", "DownRight", "DownLeft"};
    Rigidbody2D rb;
    Animator animator;
    [SerializeField]
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
    Vector3 defaultScale;
    [SerializeField]
    Vector2 lastDirection;
    // Start is called before the first frame update
    private void OnEnable(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        speed = defaultSpeed;
        defaultScale = transform.localScale;
    }

    // Update is called once per frame
    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Speed",movement.Abs().magnitude);
        animator.SetFloat("Vertical", movement.y);
        bool stopped = false;

        if(movement.x > 0 && movement.y > 0){
            // Debug.Log("UpRight");
            animator.Play("UpRight");
        }else if(movement.x < 0 && movement.y > 0){
            // Debug.Log("UpLeft");
            animator.Play("UpLeft");
        }else if(movement.x > 0 && movement.y < 0){
            // Debug.Log("DownRight");
            animator.Play("DownRight");
        }else if(movement.x < 0 && movement.y < 0){
            // Debug.Log("DownLeft");
            animator.Play("DownLeft");
        }else if(movement.Abs().magnitude < 0.1f){
            stopped = true;
            if(lastDirection.x > 0 && lastDirection.y > 0){
                //top right
                animator.Play("IdleUpRight");
            }else if(lastDirection.x < 0 && lastDirection.y > 0){
                //top left
                animator.Play("IdleUpLeft");
                // transform.localScale = new Vector3(defaultScale.x, transform.localScale.y, 1);
            }else if(lastDirection.x > 0 && lastDirection.y < 0){
                //bottom right
                animator.Play("IdleDownRight");
                transform.localScale = new Vector3(-defaultScale.x, transform.localScale.y, 1);
            }else if(lastDirection.x < 0 && lastDirection.y < 0){
                //bottom left
                animator.Play("IdleDownLeft");
            }else{
                animator.Play("IdleDownLeft");
            
            }
        }
        if(!stopped){
            if(movement.x != 0){
                lastDirection.x = movement.x;
            }
            if(movement.y != 0){
                lastDirection.y = movement.y;
            }
            // transform.localScale = new Vector3(defaultScale.x, defaultScale.y, 1);
        }
            

        // if(movement.x != 0 && movement.y != 0){
        //     if(movement.y > 0){
        //         if(movement.x > 0){
        //             transform.localScale = new Vector3(movement.x, transform.localScale.y, 1);
        //         }else if(movement.x < 0){
        //             transform.localScale = new Vector3(movement.x, transform.localScale.y, 1);
        //         }else{
        //             transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
        //         }
        //     }else if(movement.y < 0){
        //         if(movement.x > 0){
        //             transform.localScale = new Vector3(-movement.x, transform.localScale.y, 1);
        //         }else if(movement.x < 0){
        //             transform.localScale = new Vector3(-movement.x, transform.localScale.y, 1);
        //         }else{
        //             transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
        //         }
        //     }
        // }

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
    public void SetDirection(){
        // string
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
