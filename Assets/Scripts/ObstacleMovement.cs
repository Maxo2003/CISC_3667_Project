using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] private float movement = 0.8f;
    [SerializeField] private Rigidbody2D rigid;
    private const int Speed = 15;
    private const int XValueBoundary = 18;
    private const int XValueSafeZone = 0;
    [SerializeField] private bool checkingPosition;
    [SerializeField] private bool isFacingRight = true;
  //  [SerializeField] bool movingRight = true;

    // Start is called before the first frame update
    private void Start()
    {
        if (!rigid)
            rigid = GetComponent<Rigidbody2D>();

        if(PersistentData.Instance.GetDifficulty() > 2 || SceneManager.GetActiveScene().name == "Scene2" && PersistentData.Instance.GetDifficulty() == 0){
            movement = 2.2f;
        }

    }

    //called potentially many times per frame
    //use for physics & movement
    private void FixedUpdate()
    {
    //Vector2 newVelocity = rigid.velocity;
    //Movement on X axis
            rigid.linearVelocity = new Vector2(Speed * movement, rigid.linearVelocity.y);
            if (movement < 0 && isFacingRight || movement > 0 && !isFacingRight){
                FlipX();
            }

            //Can object safely flip
            if (transform.position.x > XValueSafeZone  && isFacingRight || transform.position.x < XValueSafeZone && !isFacingRight) {
                checkingPosition = true;
            }

            if (!checkingPosition) return;
            if (!(transform.position.x < -XValueBoundary) && !(transform.position.x > XValueBoundary)) return;
            // Checking direction of movement
            checkingPosition = false;

            ChangeDirection();
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
        if (collision.gameObject.CompareTag("Side"))
            ChangeDirection();
        else
            Debug.Log(collision.gameObject.tag);
    }

   }
