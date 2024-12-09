using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    [SerializeField] float movementX;
    [SerializeField] float movementY;
    [SerializeField] bool firePressed;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] const int SPEED = 15;
    [SerializeField] const float CLEARING_PLAYER_ON_INITIATION = 2.2f;
    public bool isFacingRight = true;
    [SerializeField] bool isUpright = true;
    [SerializeField] bool jumpPressed = false;
    [SerializeField] float jumpForce = 750.0f;
    [SerializeField] bool isGrounded = true;
    [SerializeField] GameObject pinPFB;
    private Animator animator;
    const int IDLE = 0;
    const int RUN = 1;
    public bool ATTACK = true;
    const int JUMP = 3;

    // Start is called before the first frame update
    void Start() {
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        animator.SetInteger("PlayerAnimation", IDLE);
    }


    // Update is called once per frame --used for user input
    //do NOT use for physics & movement
    void Update() {
        movementX = Input.GetAxis("Horizontal");
        movementY = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Fire1"))
            firePressed = true;


        if (Input.GetButtonDown("Jump"))
            jumpPressed = true;
    }

    //called potentially many times per frame
    //use for physics & movement
    private void FixedUpdate() {
    Vector2 newVelocity = rigid.velocity;

    //Movement on X axis
        if(movementX != 0)
            newVelocity.x = SPEED * movementX;
            if (movementX < 0 && isFacingRight || movementX > 0 && !isFacingRight)
                FlipX();

    //Movement on Y axis
        if(movementY != 0)
            newVelocity.y = SPEED * movementY;
            if (movementY < 0 && isUpright || movementY > 0 && !isUpright)
                FlipY();

        rigid.velocity = newVelocity;

    //Shoot
            if (firePressed)
                Fire1();
                firePressed = false;

    //Jump
        if (jumpPressed && isGrounded)
            Jump();
        else
            jumpPressed = false;

        if(isGrounded){
            if(movementX < 0 || movementX > 0){
                animator.SetInteger("PlayerAnimation", RUN);
            } else {
                animator.SetInteger("PlayerAnimation", IDLE);
            }
        }
   }

    private void FlipX() {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }

    private void FlipY() {
            transform.Rotate(180, 0, 0);
            isUpright = !isUpright;
        }

    private void Jump() {
        animator.SetInteger("PlayerAnimation", JUMP);
        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.AddForce(new Vector2(0, jumpForce));
        Debug.Log("jumped");
        jumpPressed = false;
        isGrounded = false;
    }
    private void Fire1(){
        animator.SetBool("PlayerAttacks", ATTACK);
        if(isFacingRight){
            Vector2 position = new Vector2((transform.position.x + CLEARING_PLAYER_ON_INITIATION) , transform.position.y);
            Instantiate(pinPFB, position, Quaternion.identity);
        }else{
            Vector2 position = new Vector2((transform.position.x + -CLEARING_PLAYER_ON_INITIATION) , transform.position.y);
            Instantiate(pinPFB, position, Quaternion.identity);
        }
    }

    public void EndAttackAnimation(){
        animator.SetBool("PlayerAttacks", false);
    }

    public void EndJumpAnimation(){
        animator.SetInteger("PlayerAnimation", IDLE);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Ground")
            isGrounded = true;
        else
            Debug.Log(collision.gameObject.tag);
        //animator.SetInteger("PlayerAnimation", IDLE);
    }

}
