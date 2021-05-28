using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    #region Variables
    [Header("Hidden")]
    [HideInInspector] public bool StartGame = false;
    [HideInInspector] public bool GameOver = false;
    [HideInInspector] public int Score = 0;
    [HideInInspector] public MathRNG objMathRNG = new MathRNG(45289574);
    [Header("Static")]
    [HideInInspector] public static bool PauseGame = false;
    [HideInInspector] public static bool MuteGame;
    [HideInInspector] public static bool BlockKeyBoard = false;
    [Header("Scene Bounds")]
    [HideInInspector] public Vector3 minValues;
    [HideInInspector] public Vector3 maxValues;
    public Tilemap TileGrid;
    #endregion

    #region Start & Update
    void Start()
    {
        if(TileGrid != null)
        {
            minValues = new Vector3(TileGrid.editorPreviewOrigin.x, TileGrid.editorPreviewOrigin.y, 0);
            maxValues = new Vector3(TileGrid.editorPreviewOrigin.x + TileGrid.editorPreviewSize.x, TileGrid.editorPreviewOrigin.y + TileGrid.editorPreviewSize.y, 0);
        }
        StartGame = true;
        PauseGame = false;
        GameOver = false;
        Score = 0;
        Time.timeScale = 1;
        var lstObjects = this.gameObject.GetComponentsInChildren<SpawnObject>(true);
        foreach (var obj in lstObjects)
        {
            obj.TileGrid = obj.TileGrid == null ? TileGrid : obj.TileGrid;
            obj.objMathRNG = objMathRNG;
            obj.Spawn();
        }
    }
    void Update()
    {
    }
    #endregion
}