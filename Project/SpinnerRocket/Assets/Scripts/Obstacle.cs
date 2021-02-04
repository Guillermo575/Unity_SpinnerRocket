using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    #region Variable
    private Transform transform;
    private Rigidbody2D rigidbody;

    private Vector2 target = new Vector2();
    public GameManager GameManager;
    #endregion

    #region General
    void Start()
    {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
        target = GameManager.objPlayer.transform.position;
        RotateTowards(target);
    }
    void Update()
    {
        if (GameManager.StartGame && !GameManager.GameOver)
        {
            if (transform.position.x < GameManager.minValues.x - 1 || transform.position.x > GameManager.maxValues.x + 1 || 
                transform.position.y < GameManager.minValues.y - 1 || transform.position.y > GameManager.maxValues.y + 1)
            {
                transform.position = getRandomSpawnPoint();
                target = GameManager.objPlayer.transform.position;
                RotateTowards(target);
            }
            //target = GameManager.objPlayer.transform.position;
            //RotateTowards(target);
            setSpeed(3);
        }
        else
        {
            setSpeed(0);
        }
    }
    public Vector2 getRandomSpawnPoint()
    {
        var RanX = UnityEngine.Random.Range(0, 3);
        Vector2 vecSpawn = new Vector2();
        switch (RanX)
        {
            case 0: vecSpawn = new Vector2(UnityEngine.Random.Range(GameManager.minValues.x, GameManager.maxValues.x), GameManager.maxValues.y); break;
            case 1: vecSpawn = new Vector2(UnityEngine.Random.Range(GameManager.minValues.x, GameManager.maxValues.x), GameManager.minValues.y); break;
            case 2: vecSpawn = new Vector2(GameManager.maxValues.x, UnityEngine.Random.Range(GameManager.minValues.y, GameManager.maxValues.y)); break;
            case 3: vecSpawn = new Vector2(GameManager.minValues.x, UnityEngine.Random.Range(GameManager.minValues.y, GameManager.maxValues.y)); break;
        }
        return vecSpawn;
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
        var offset = 180f;
        Vector2 direction = (Vector2)transform.position - target;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }
    #endregion

}
