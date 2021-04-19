using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnlyKeyBoardInputModule : StandaloneInputModule
{
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
}
