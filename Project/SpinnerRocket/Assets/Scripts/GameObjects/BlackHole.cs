using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [HideInInspector] public new Transform transform;
    [HideInInspector] public GameManager GameManager;
    void Start()
    {
        GameManager = GameObject.Find("GameManager").gameObject.GetComponentsInChildren<GameManager>(true)[0];
        transform = GetComponent<Transform>();
    }
    void Update()
    {
        transform.Rotate(0, 0, -(5 * Time.deltaTime));
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (GameManager.StartGame)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.transform.position = Vector2.MoveTowards(collision.gameObject.transform.position, transform.position, 1 * Time.deltaTime);
            }
        }
    }
}
