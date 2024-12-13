using UnityEngine;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour {
    private static readonly int PlayerAttacks = Animator.StringToHash("PlayerAttacks");
    private static readonly int PlayerAnimation = Animator.StringToHash("PlayerAnimation");
    [SerializeField] private float movementX;
    [SerializeField] private float movementY;
    [SerializeField] private bool firePressed;
    [SerializeField] private Rigidbody2D rigid;
    private const int Speed = 15;
    private const float ClearingPlayerOnInitiation = 2.2f;
    public bool isFacingRight = true;
    [SerializeField] private bool isUpright = true;
    [SerializeField] private bool jumpPressed;
    [SerializeField] private float jumpForce = 1000.0f;
    [SerializeField] private bool isGrounded = true;
    [FormerlySerializedAs("pinPFB")] [SerializeField]
    private GameObject pinPfb;
    private Animator _animator;
    private const int Idle = 0;
    private const int Run = 1;
    [FormerlySerializedAs("ATTACK")] public bool attack = true;
    private const int JUMP = 3;

    // Start is called before the first frame update
    private void Start() {
        if (!rigid)
            rigid = GetComponent<Rigidbody2D>();

        _animator = GetComponent<Animator>();
        _animator.SetInteger(PlayerAnimation, Idle);
    }


    // Update is called once per frame --used for user input
    //do NOT use for physics & movement
    private void Update() {
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
    var newVelocity = rigid.linearVelocity;

    //Movement on X axis
        if(movementX != 0)
            newVelocity.x = Speed * movementX;
        if (movementX < 0 && isFacingRight || movementX > 0 && !isFacingRight)
            FlipX();

    //Movement on Y axis
        if(movementY != 0)
            newVelocity.y = Speed * movementY;
        if (movementY < 0 && isUpright || movementY > 0 && !isUpright)
            FlipY();

        rigid.linearVelocity = newVelocity;

    //Shoot
    if (firePressed) {
        Fire1();
        firePressed = false;
    }

    //Jump
        if (jumpPressed && isGrounded)
            Jump();
        else
            jumpPressed = false;

        if (!isGrounded) return;
        _animator.SetInteger(PlayerAnimation, movementX is < 0 or > 0 ? Run : Idle);
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
        _animator.SetInteger(PlayerAnimation, JUMP);
        rigid.linearVelocity = new Vector2(rigid.linearVelocity.x, 0);
        rigid.AddForce(new Vector2(0, jumpForce));
        jumpPressed = false;
        isGrounded = false;
    }
    private void Fire1(){
        _animator.SetBool(PlayerAttacks, attack);
        if(isFacingRight){
            var position = new Vector2((transform.position.x + ClearingPlayerOnInitiation) , transform.position.y);
            Instantiate(pinPfb, position, Quaternion.identity);
        }else{
            var position = new Vector2((transform.position.x + -ClearingPlayerOnInitiation) , transform.position.y);
            Instantiate(pinPfb, position, Quaternion.identity);
        }
    }

    public void EndAttackAnimation(){
        _animator.SetBool(PlayerAttacks, false);
    }

    public void EndJumpAnimation(){
        _animator.SetInteger(PlayerAnimation, Idle);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
        else
            Debug.Log(collision.gameObject.tag);
        //animator.SetInteger("PlayerAnimation", IDLE);
    }

}
