using UnityEngine;
using UnityEngine.EventSystems;
public class OnlyKeyBoardInputModule : StandaloneInputModule
{
    public GameObject objFocus;
    public bool isMouseInputActive = false;
    public bool GetMouseState
    {
        get { return isMouseInputActive; }
    }
    public void MouseSwitcher()
    {
        isMouseInputActive = isMouseInputActive == false ? true : false;
    }
    public override void Process()
    {
        bool usedEvent = SendUpdateEventToSelectedObject();
        if (eventSystem.sendNavigationEvents)
        {
            if (!usedEvent)
                usedEvent |= SendMoveEventToSelectedObject();
            if (!usedEvent)
                SendSubmitEventToSelectedObject();
        }
        if (isMouseInputActive)
            ProcessMouseEvent();
    }
    public void Update()
    {
        var focusActual = eventSystem.currentSelectedGameObject;
        objFocus = focusActual == null ? objFocus : focusActual;
        focusActual = objFocus;
        eventSystem.SetSelectedGameObject(focusActual);
    }
}