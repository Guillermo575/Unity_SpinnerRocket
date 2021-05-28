using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
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
    [HideInInspector] public Vector3 minValues;
    [HideInInspector] public Vector3 maxValues;
    public Tilemap TileGrid;
    public float OffSetUnits;
    #endregion

    #region Methods
    void Start()
    {
    }
    public void Spawn()
    {
        if (TileGrid != null)
        {
            minValues = new Vector3(TileGrid.editorPreviewOrigin.x, TileGrid.editorPreviewOrigin.y, 0);
            maxValues = new Vector3(TileGrid.editorPreviewOrigin.x + TileGrid.editorPreviewSize.x, TileGrid.editorPreviewOrigin.y + TileGrid.editorPreviewSize.y, 0);
        }
        var min = new Vector2(minValues.x + OffSetUnits, minValues.y + OffSetUnits);
        var max = new Vector2(maxValues.x - OffSetUnits, maxValues.y - OffSetUnits);
        for (int i = 0; i < Quantity; i++)
        {
            Vector2 position = new Vector2();
            switch(type)
            {
                case SpawnType.Border: position = objMathRNG.getRandomSpawnPoint(min, max); break;
                case SpawnType.InScene: position = new Vector2(objMathRNG.NextValueFloat(min.x, max.x), objMathRNG.NextValueFloat(min.y, max.y)); break;
            }
            lstObj.Add(Instantiate(obj, position, Quaternion.identity));
        }
    }
    #endregion
}