using System;
using UnityEngine;
public class Obstacle : MonoBehaviour
{
    #region Variable
    [HideInInspector] private Transform transform;
    [HideInInspector] private Rigidbody2D rigidbody;

    [HideInInspector] public GameObject objTarget;
    [HideInInspector] private Vector2 target = new Vector2();
    [HideInInspector] public GameManager GameManager;
    #endregion

    #region General
    void Start()
    {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
        objTarget = GameManager.objPlayer;
        RotateTowards(objTarget.transform.position);
    }
    void Update()
    {
        if (GameManager.StartGame && !GameManager.GameOver)
        {
            if (transform.position.x < GameManager.minValues.x - 1 || transform.position.x > GameManager.maxValues.x + 1 || 
                transform.position.y < GameManager.minValues.y - 1 || transform.position.y > GameManager.maxValues.y + 1)
            {
                transform.position = GameManager.objMathRNG.getRandomSpawnPoint(GameManager.minValues, GameManager.maxValues);
                RotateTowards(objTarget.transform.position);
            }
            //RotateTowards(objTarget.transform.position);
            setSpeed(3);
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
        //newVelocity.x = rigidbody.rotation > 90.0f || rigidbody.rotation < -90.0f ? -newVelocity.x : newVelocity.x;
        //newVelocity.y = rigidbody.rotation > 0 ? -newVelocity.y : newVelocity.y;
        rigidbody.velocity = (newVelocity);
    }
    public void RotateTowards(Vector2 target)
    {
        this.target = target == null ? new Vector2(0f, 0f) : target;
        var offset = 180f;
        Vector2 direction = (Vector2)transform.position - target;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }
    #endregion
}