using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    #region Hidden
    [HideInInspector] public List<GameObject> lstObj;
    [HideInInspector] public MathRNG objMathRNG = new MathRNG(45289574);
    #endregion

    #region Editor Variables
    [Header("General")]
    public GameObject obj;
    public int Quantity;
    public enum SpawnType
    {
        Border = 1,
        InScene = 2,
    }
    public SpawnType type;

    [Header("Scene Bounds")]
    public Vector3 minValues;
    public Vector3 maxValues;
    #endregion

    #region Methods
    void Start()
    {
    }
    public void Spawn()
    {
        for (int i = 0; i < Quantity; i++)
        {
            Vector2 position = new Vector2();
            switch(type)
            {
                case SpawnType.Border: position = new Vector2(objMathRNG.NextValueFloat(minValues.x, maxValues.x), objMathRNG.NextValueFloat(minValues.y, maxValues.y)); break;
                case SpawnType.InScene: position = objMathRNG.getRandomSpawnPoint(minValues, maxValues); break;
            }
            lstObj.Add(Instantiate(obj, position, Quaternion.identity));
        }
    }
    #endregion
}