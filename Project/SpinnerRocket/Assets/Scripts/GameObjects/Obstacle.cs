using UnityEngine;
public class Obstacle : MonoBehaviour
{
    #region Variable
    [HideInInspector] private new Transform transform;
    [HideInInspector] private new Rigidbody2D rigidbody;
    [HideInInspector] public GameManager GameManager;
    [HideInInspector] public GameObject objTarget;
    [HideInInspector] public bool targetLock = false;
    public float SpeedMovement = 3;
    #endregion

    #region General
    void Start()
    {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
        GameManager = GameObject.Find("GameManager").gameObject.GetComponentsInChildren<GameManager>(true)[0];
        var objmain = GameObject.Find("MainPlayer");
        objTarget = objmain == null ? null : objmain.gameObject;
    }
    void Update()
    {
        if (GameManager.StartGame)
        {
            if(!targetLock)
            {
                RotateTowards(objTarget == null ? new Vector3(0f, 0f, 0f) : objTarget.transform.position);
                targetLock = true;
            }
            if (transform.position.x < GameManager.minValues.x - 1 || transform.position.x > GameManager.maxValues.x + 1 || 
                transform.position.y < GameManager.minValues.y - 1 || transform.position.y > GameManager.maxValues.y + 1)
            {
                transform.position = GameManager.objMathRNG.getRandomSpawnPoint(GameManager.minValues, GameManager.maxValues);
                RotateTowards(objTarget == null ? new Vector3(0f, 0f, 0f) : objTarget.transform.position);
            }
            setSpeed(SpeedMovement);
        }
        else
        {
            setSpeed(0);
        }
    }
    #endregion

    #region setDirection
    public void setSpeed(float Speed)
    {
        Vector2 newVelocity;
        var angle = rigidbody.rotation;
        newVelocity.x = Mathf.Cos(angle * Mathf.Deg2Rad) * Speed;
        newVelocity.y = Mathf.Sin(angle * Mathf.Deg2Rad) * Speed;
        newVelocity.x = Mathf.Round(newVelocity.x);
        newVelocity.y = Mathf.Round(newVelocity.y);
        rigidbody.velocity = (newVelocity);
    }
    public void RotateTowards(Vector2 target)
    {
        var targetPosition = target == null ? new Vector2(0f, 0f) : target;
        var offset = 180f;
        Vector2 direction = (Vector2)transform.position - targetPosition;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }
    #endregion
}
//newVelocity.x = rigidbody.rotation > 90.0f || rigidbody.rotation < -90.0f ? -newVelocity.x : newVelocity.x;
//newVelocity.y = rigidbody.rotation > 0 ? -newVelocity.y : newVelocity.y;