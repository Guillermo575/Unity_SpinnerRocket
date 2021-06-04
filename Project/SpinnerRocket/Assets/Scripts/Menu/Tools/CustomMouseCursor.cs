using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CustomMouseCursor : MonoBehaviour
{
    [HideInInspector] Vector2 hotSpot = new Vector2(0, 0);
    [HideInInspector] CursorMode cursorMode = CursorMode.Auto;
    public Texture2D CursorNormal;
    public Texture2D CursorPress;
    void Start()
    {
        Cursor.SetCursor(CursorNormal, hotSpot, cursorMode);
    }
    public void OnEnterExit()
    {
        Cursor.SetCursor(CursorNormal, hotSpot, cursorMode);
    }
    public void OnEnterPress()
    {
        Cursor.SetCursor(CursorPress, hotSpot, cursorMode);
    }
}