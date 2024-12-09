using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinMovement : MonoBehaviour {
    [SerializeField] float movement;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] const int SPEED = 15;
    [SerializeField] const int X_VALUE_BOUNDARY = 18;
    [SerializeField] const int X_VALUE_SAFEZONE = 0;
    [SerializeField] bool isFacingRight = true;
    [SerializeField] bool justInitiated = true;
    [SerializeField] bool checkingPosition;
    [SerializeField] AudioSource pinAudio;
    [SerializeField] GameObject player;
    [SerializeField] Movement playerScript;
//  [SerializeField] bool movingRight = true;

    // Start is called before the first frame update
    void Start() {
        if (rigid == null) {
            rigid = GetComponent<Rigidbody2D>();
        }

        //Used to check which direction the player is facing when firing a pin
        if (player == null) {
                    player = GameObject.Find("NinjaIdle_1");
                }
        if (player != null) {
                playerScript = player.GetComponent<Movement>();
            }

            InvokeRepeating("DeletePin", 2.0f, 2.0f);
    }

    // Update is called once per frame --used for user input
    //do NOT use for physics & movement
    void Update() {

    }

    //called potentially many times per frame
    //use for physics & movement
    private void FixedUpdate() {
    //Movement on X axis
            if(justInitiated) { //Stops pins from constantly facing where player is facing
                bool currentPlayerDirection = playerScript.isFacingRight;

                //Sets movement based on where player is facing
                if (currentPlayerDirection){
                    movement = 0.75f;
                }else{
                    movement = -0.75f;
                }
                justInitiated = false;//Pins now face towards direction of movement
                }

                rigid.velocity = new Vector2(SPEED * movement, rigid.velocity.y);
                if (movement < 0 && isFacingRight || movement > 0 && !isFacingRight){
                    FlipX();
                    }

                //Can object safely flip
                if (transform.position.x > X_VALUE_SAFEZONE  && isFacingRight || transform.position.x < X_VALUE_SAFEZONE && !isFacingRight) {
                            checkingPosition = true;
                        }

                //Stops object from constantly changing direction at boundary
                if(checkingPosition) {
                    if (transform.position.x < -X_VALUE_BOUNDARY || transform.position.x > X_VALUE_BOUNDARY ) {
                    //Checking direction of movement
                       movement = 0f;
                       checkingPosition = false;
                       ChangeDirection();
                       }
                }
    }

    private void ChangeDirection() {
           //movement *= -1;
           if(isFacingRight)
               movement = -0.75f;
           else
               movement = 0.75f;
    }

    private void FlipX() {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }

    private void DeletePin(){
        AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Side"){
            ChangeDirection();
        }else{
            Debug.Log(collision.gameObject.tag);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Obstacle"){
            DeletePin();
        }
    }

}
