using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraBehavior : MonoBehaviour
{
    [HideInInspector] public GameManager objGameManager;
    public Camera Camera;
    public Transform target;
    public Vector3 offset;
    [Range(0, 10)] public float smoothFactor;
    void Start()
    {
        objGameManager = GameObject.Find("GameManager").gameObject.GetComponentsInChildren<GameManager>(true)[0];
    }
    void Update()
    {
        FollowCamera();
    }
    public void FollowCamera()
    {
        float height = 2f * Camera.orthographicSize;
        float width = height * Camera.aspect;
        Vector3 targetPosition = target.position + offset;
        Vector3 boundPosition = new Vector3(Mathf.Clamp(targetPosition.x, objGameManager.minValues.x + (width / 2), objGameManager.maxValues.x - (width / 2)), Mathf.Clamp(targetPosition.y, objGameManager.minValues.y + (height / 2), objGameManager.maxValues.y - (height / 2)), -10);
        Vector3 smoothPosition = Vector3.Lerp(Camera.transform.position, boundPosition, smoothFactor * Time.fixedDeltaTime);
        Camera.transform.position = (smoothFactor == 0) ? boundPosition : smoothPosition;
    }
}
