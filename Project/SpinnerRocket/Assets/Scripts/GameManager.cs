﻿using UnityEngine;
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
    public Vector3 minValues;
    public Vector3 maxValues;
    #endregion

    #region Start & Update
    void Start()
    {
        StartGame = true;
        PauseGame = false;
        GameOver = false;
        Score = 0;
        Time.timeScale = 1;
        var lstObjects = this.gameObject.GetComponentsInChildren<SpawnObject>(true);
        foreach (var obj in lstObjects)
        {
            obj.objMathRNG = objMathRNG;
            obj.Spawn();
        }
    }
    void Update()
    {

    }
    #endregion
}