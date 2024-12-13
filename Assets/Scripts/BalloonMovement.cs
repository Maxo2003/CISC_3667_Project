using UnityEngine;
using UnityEngine.SceneManagement;

public class BalloonMovement : MonoBehaviour
{
    private static readonly int IsPopped = Animator.StringToHash("IsPopped");
    [SerializeField] private float movement = 0.85f;
    [SerializeField] private Rigidbody2D rigid;
    private const int Speed = 15;
    private const float ScaleChange = 1.5f;
    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private bool checkingPosition;
    //[SerializeField] GameObject Balloon;
    [SerializeField] private bool popCalled;
    [SerializeField] private bool poppedByPlayer;
    private const int XValueBoundary = 18;
    private const int XValueSafeZone = 0;
    [SerializeField] private GameObject controller;
    [SerializeField] private int repeaterCalled;
    private Animator _balloonAnimator;

    // Start is called before the first frame update
    private void Start()
    {
        //Scaling balloon periodically
        InvokeRepeating(nameof(IncreaseSize), 2.0f, 2.0f);
        if (PersistentData.Instance.GetDifficulty() == 0)
        {
            if (SceneManager.GetActiveScene().name == "Scene1")
            {
                movement = 1.0f;
            }

            if (SceneManager.GetActiveScene().name == "Scene2")
            {
                movement = 2.0f;
            }
        } else if (PersistentData.Instance.GetDifficulty() == 2)
        {
            movement = 1.0f;
        } else if (PersistentData.Instance.GetDifficulty() == 3)
        {
            movement = 2.0f;
        }

        rigid = GetComponent<Rigidbody2D>();

        controller = GameObject.FindGameObjectWithTag("GameController");

        _balloonAnimator = GetComponent<Animator>();


        _balloonAnimator.SetBool(IsPopped, false);


        GetComponent<AudioSource>();


    }

    // Update is called once per frame --used for user input
    //do NOT use for physics & movement

    //called potentially many times per frame
    //use for physics & movement
    private void FixedUpdate()
    {
    //Movement on X axis
            rigid.linearVelocity = new Vector2(Speed * movement, rigid.linearVelocity.y);
            if (movement < 0 && isFacingRight || movement > 0 && !isFacingRight){
                FlipX();
            }

            //Can object safely flip
            if (transform.position.x > XValueSafeZone  && isFacingRight || transform.position.x < XValueSafeZone && !isFacingRight) {
                checkingPosition = true;
            }

            if(checkingPosition){
                if (transform.position.x < -XValueBoundary || transform.position.x > XValueBoundary ) {
                    //Checking direction of movement
                    checkingPosition = false;

                    ChangeDirection();
                }
            }

            //If balloon gets too big, "pop" it
            if(transform.localScale.x > 16){
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

    private void IncreaseSize()
        {
            transform.localScale *= ScaleChange;
            repeaterCalled++;
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

     public void DestroyAfterAnimation(){
        Destroy(gameObject);
        var obj = controller.GetComponent<Scorekeeper>();
        obj.RestartScene();
        if(poppedByPlayer){
            controller.GetComponent<Scorekeeper>().ChangeScene();
        }
     }
 
     private void PopBalloon(){
        if(popCalled){
        } else{
            popCalled = true;
            _balloonAnimator.SetBool(IsPopped, true);
            AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position);
        } 
     }

     private void OnTriggerEnter2D(Collider2D collision)
     {
         if (!collision.gameObject.CompareTag("Pin")) return;
         controller.GetComponent<Scorekeeper>().AddPoints(repeaterCalled);
         poppedByPlayer = true;
         PopBalloon();
     }
}
