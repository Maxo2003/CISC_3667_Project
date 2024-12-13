using UnityEngine;

public class PinMovement : MonoBehaviour {
    [SerializeField] private float movement = 0.85f;
    [SerializeField] private Rigidbody2D rigid;
    private const int Speed = 15;
    private const int XValueBoundary = 18;
    private const int XValueSafeZone = 0;
    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private bool justInitiated = true;
    [SerializeField] private bool checkingPosition;
    [SerializeField] private AudioSource pinAudio;
    [SerializeField] private GameObject player;
    [SerializeField] private Movement playerScript;
//  [SerializeField] bool movingRight = true;

    // Start is called before the first frame update
    private void Start() {
        if (!rigid) {
            rigid = GetComponent<Rigidbody2D>();
        }

        //Used to check which direction the player is facing when firing a pin
        if (!player) {
                    player = GameObject.Find("NinjaIdle_1");
        }
        if (player) {
            playerScript = player.GetComponent<Movement>();
        }

        InvokeRepeating(nameof(DeletePin), 2.0f, 2.0f);
    }

    //called potentially many times per frame
    //use for physics & movement
    private void FixedUpdate() {
    //Movement on X axis
            if(justInitiated) { //Stops pins from constantly facing where player is facing
                var currentPlayerDirection = playerScript.isFacingRight;

                //Sets movement based on where player is facing
                if (currentPlayerDirection){
                    movement = 0.85f;
                }else{
                    movement = -0.85f;
                }
                justInitiated = false;//Pins now face towards direction of movement
            }

            rigid.linearVelocity = new Vector2(Speed * movement, rigid.linearVelocity.y);
            if (movement < 0 && isFacingRight || movement > 0 && !isFacingRight){
                FlipX();
            }

            //Can object safely flip
            if (transform.position.x > XValueSafeZone  && isFacingRight || transform.position.x < XValueSafeZone && !isFacingRight) {
                checkingPosition = true;
            }

            //Stops object from constantly changing direction at boundary
            if (!checkingPosition) return;
            if (!(transform.position.x < -XValueBoundary) && !(transform.position.x > XValueBoundary)) return;
            //Checking direction of movement
            movement = 0f;
            checkingPosition = false;
            ChangeDirection();
    }

    private void ChangeDirection() {
           //movement *= -1;
           if(isFacingRight){
               movement = -0.85f;
           }
           else {
               movement = 0.85f;
           }
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
        if (collision.gameObject.CompareTag("Side")){
            ChangeDirection();
        }else{
            Debug.Log(collision.gameObject.tag);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Obstacle")){
            DeletePin();
        }
    }

}
