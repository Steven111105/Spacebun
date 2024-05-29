using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerSFXManager playerSFXManager;
    Rigidbody2D rb;
    Animator animator;
    Vector2 movement;
    float speed;
    [SerializeField]
    float defaultSpeed;
    [SerializeField]
    float dashSpeed;
    [SerializeField]
    float dashLength;
    [SerializeField]
    float dashCooldown;
    bool canDash = true;
    public bool helping;
    public GameObject helpTarget;
    GameObject helpEndTrigger;
    bool touchingNPC = false;
    string directionString; //for anim
    //biasa = speed normal, dash
    //kecil = speed fast, dash
    //tua = speed slow, no dash
    //blind = speed normal, no dash
    readonly string[] helpTypeStrings = {"Normal", "Kid", "Old", "Blind" ,""};
    [SerializeField]
    int helpType;
    Vector3 defaultScale;
    [SerializeField]
    Vector2 lastDirection;
    // Start is called before the first frame update
    private void OnEnable(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        speed = defaultSpeed;
        defaultScale = transform.localScale;
        helpType = 4;
    }

    // Update is called once per frame
    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        bool stopped = false;
        if(movement.x > 0 && movement.y > 0){
            // Debug.Log("UpRight");
            directionString = "UpRight";
        }else if(movement.x < 0 && movement.y > 0){
            // Debug.Log("UpLeft");
            directionString = "UpLeft";
        }else if(movement.x > 0 && movement.y < 0){
            // Debug.Log("DownRight");
            directionString = "DownRight";
        }else if(movement.x < 0 && movement.y < 0){
            // Debug.Log("DownLeft");
            directionString = "DownLeft";
        }else if(movement.Abs().magnitude < 0.1f){
            stopped = true;
            if(lastDirection.x > 0 && lastDirection.y > 0){
                //top right
                directionString = "IdleUpRight";
            }else if(lastDirection.x < 0 && lastDirection.y > 0){
                //top left
                directionString = "IdleUpLeft";
                // transform.localScale = new Vector3(defaultScale.x, transform.localScale.y, 1);
            }else if(lastDirection.x > 0 && lastDirection.y < 0){
                //bottom right
                directionString = "IdleDownRight";
                transform.localScale = new Vector3(-defaultScale.x, transform.localScale.y, 1);
            }else if(lastDirection.x < 0 && lastDirection.y < 0){
                //bottom left
                directionString = "IdleDownLeft";
            }else{
                directionString = "IdleDownLeft";
            }
        }
        if(!helping){
            helpType = 4;
        }

        animator.Play(helpTypeStrings[helpType] + directionString);

        if(!stopped){
            if(movement.x != 0){
                lastDirection.x = movement.x;
            }
            if(movement.y != 0){
                lastDirection.y = movement.y;
            }
        }

        if(helpTarget != null && helping){
            helpEndTrigger.transform.GetChild(0).gameObject.SetActive(true);
        }else{
            if(helpEndTrigger != null){
                helpEndTrigger.transform.GetChild(0).gameObject.SetActive(false);
            }
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
                if(touchingNPC && !helping && helpTarget.GetComponent<NPC>().hasEnd){
                    helping = true;
                    helpEndTrigger = helpTarget.GetComponent<NPC>().endTrigger;
                    helpType = helpTarget.GetComponent<NPC>().npcType;
                    helpTarget.transform.parent = transform;
                    helpTarget.GetComponent<NPC>().gettingHelp = true;
                    speed = helpTarget.GetComponent<NPC>().speed;
                    playerSFXManager.PlaySFX(1);
                }else{
                    helping = false;
                    helpTarget.GetComponent<NPC>().gettingHelp = false;
                    helpTarget.GetComponent<NPC>().GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    helpTarget.transform.parent = null;
                    helpType = 4;
                    speed = defaultSpeed;
                }
            }else{
                helping = false;
                helpTarget = null;
                helpType = 4;
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
        playerSFXManager.PlaySFX(2);
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
        playerSFXManager.PlaySFX(0);
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
