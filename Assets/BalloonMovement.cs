using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BalloonMovement : MonoBehaviour
{
    [SerializeField] float movement = 0.85f;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] const int SPEED = 15;
    [SerializeField] const float SCALE_CHANGE = 2.0f;
    [SerializeField] bool isFacingRight = true;
    [SerializeField] bool checkingPosition;
    [SerializeField] GameObject Balloon;
    [SerializeField] const int X_VALUE_BOUNDARY = 18;
    [SerializeField] const int X_VALUE_SAFEZONE = 0;
    [SerializeField] AudioSource balloonAudio;
    [SerializeField] GameObject controller;
    [SerializeField] int repeaterCalled = 0;


    // Start is called before the first frame update
    void Start()
    {
        //Scaling balloon periodically
        InvokeRepeating("IncreaseSize", 2.0f, 3.0f);

        if(SceneManager.GetActiveScene().name == "Scene2"){
            movement = 1.0f;
        }

        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();

        if (controller == null)
        {
            controller = GameObject.FindGameObjectWithTag("GameController");
        }

        if (GetComponent<AudioSource>() == null)
            balloonAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame --used for user input
    //do NOT use for physics & movement
    void Update()
    {

    }

    //called potentially many times per frame
    //use for physics & movement
    private void FixedUpdate()
    {
    //Movement on X axis
            rigid.velocity = new Vector2(SPEED * movement, rigid.velocity.y);
            if (movement < 0 && isFacingRight || movement > 0 && !isFacingRight){
                FlipX();
                }

            //Can object safely flip
            if (transform.position.x > X_VALUE_SAFEZONE  && isFacingRight || transform.position.x < X_VALUE_SAFEZONE && !isFacingRight) {
                        checkingPosition = true;
                    }

            if(checkingPosition){
                if (transform.position.x < -X_VALUE_BOUNDARY || transform.position.x > X_VALUE_BOUNDARY ) {
                //Checking direction of movement
                   checkingPosition = false;

                   ChangeDirection();
                   }
            }
            //If balloon gets too big, "pop" it
            if(transform.localScale.x > 6){
                PopBalloon();
            }
    }

    private void ChangeDirection(){
          movement *= -1;
      }

    private void FlipX()
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }

    void IncreaseSize()
        {
            transform.localScale *= SCALE_CHANGE;
            repeaterCalled++;
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

     private void PopBalloon(){
        controller.GetComponent<Spawner>().BalloonPopped();
        AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position);
        Destroy(gameObject);
     }

     private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.tag == "Pin"){
                controller.GetComponent<Scorekeeper>().AddPoints(repeaterCalled);
                PopBalloon();
                controller.GetComponent<Scorekeeper>().changeScene();
                }
        }
}
