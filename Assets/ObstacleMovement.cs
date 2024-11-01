using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] float movement = 0.8f;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] const int SPEED = 15;
    [SerializeField] const int X_VALUE_BOUNDARY = 18;
    [SerializeField] const int X_VALUE_SAFEZONE = 0;
    [SerializeField] bool checkingPosition;
    [SerializeField] bool isFacingRight = true;
  //  [SerializeField] bool movingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();

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
    //Vector2 newVelocity = rigid.velocity;
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
    }

    private void ChangeDirection(){
           movement *= -1;
           FlipX();
    }

    private void FlipX()
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Side")
            ChangeDirection();
        else
            Debug.Log(collision.gameObject.tag);
    }

   }
