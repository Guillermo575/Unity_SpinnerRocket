using UnityEngine;

public class Player : MonoBehaviour
{

    #region Variables
    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private Transform transform;
    private Rigidbody2D rigidbody;
    private Renderer renderer;

    [HideInInspector] public bool InMovement = false;
    [HideInInspector] public float FraccAngle = 360/8;
    public bool Stucked = false;
    public GameManager GameManager;
    public int RotationXMin = 180;
    public int SpeedMovement = 15;
    #endregion

    #region General
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<Renderer>();
    }
    void Update()
    {
        if (GameManager.StartGame && !GameManager.PauseGame && !GameManager.GameOver)
        {
            animator.SetBool("Death", false);
            var MinX = GameManager.minValues.x;
            var MinY = GameManager.minValues.y;
            var MaxX = GameManager.maxValues.x;
            var MaxY = GameManager.maxValues.y;
            var RenderWidth = (renderer.bounds.size.x / 2);
            var RenderHeight = (renderer.bounds.size.y / 2);
            var transformX = Mathf.Clamp(transform.position.x, MinX + RenderWidth, MaxX - RenderWidth);
            var transformY = Mathf.Clamp(transform.position.y, MinY + RenderHeight, MaxY - RenderHeight);
            Stucked = Stucked == false ? !(transformX == transform.position.x && transformY == transform.position.y) : Stucked;
            transform.position = new Vector3(transformX, transformY, transform.position.z);
            if (!Input.GetKey("space"))
            {
                Stucked = false;
                transform.Rotate(0, 0, -(RotationXMin * Time.deltaTime));
                setSpeed(0);
                InMovement = false;
                animator.SetBool("Run", false);
            }
            else
            {
                animator.SetBool("Run", true);
                if (!InMovement)
                {
                    InMovement = true;
                    //var angles = (transform.rotation.z * Mathf.Rad2Deg) * Mathf.PI;
                    //angles = Mathf.Round(angles / FraccAngle) * FraccAngle;
                    //rigidbody.rotation = angles;
                    //Debug.Log(angles);
                }
                setSpeed(SpeedMovement);
            }
        }
        else
        {
            setSpeed(0);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.StartGame)
        {
            if (collision.gameObject.tag == "Star")
            {
                collision.gameObject.transform.position = new Vector3(UnityEngine.Random.Range(-9, 9), UnityEngine.Random.Range(-5, 5), 0);
                GameManager.Score += 1;
            }
            if (collision.gameObject.tag == "Obstaculo")
            {
                animator.SetBool("Death", true);
                GameManager.GameOver = true;
            }
        }
    }
    #endregion

    #region Direction
    public void setSpeed(float Speed)
    {
        Vector2 newVelocity;
        var angle = rigidbody.rotation;
        newVelocity.x = Mathf.Cos(angle * Mathf.Deg2Rad) * Speed;
        newVelocity.y = Mathf.Sin(angle * Mathf.Deg2Rad) * Speed;
        newVelocity.x = Mathf.Round(newVelocity.x);
        newVelocity.y = Mathf.Round(newVelocity.y);
        if(Stucked)
        {
            newVelocity.x = 0;
            newVelocity.y = 0;
        }
        rigidbody.velocity = (newVelocity);
    }
    #endregion

}
//Vector2 newVelocity;
//var angle = transform.rotation.z;
//newVelocity.x = Mathf.Cos(angle) * Speed;
//newVelocity.x = rigidbody.rotation > 90.0f || rigidbody.rotation < -90.0f ? -newVelocity.x : newVelocity.x;
//newVelocity.y = Mathf.Sin(angle) * Speed;
//rigidbody.velocity = (newVelocity);