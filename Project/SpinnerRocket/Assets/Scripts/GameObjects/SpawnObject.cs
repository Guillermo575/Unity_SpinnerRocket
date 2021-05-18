using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject obj;
    [Header("Scene Bounds")]
    public Vector3 minValues;
    public Vector3 maxValues;
    public enum SpawnType
    {
        Border = 1,
        InScene = 2,
    }
    public SpawnType type;
    void Start()
    {
        
    }
}
