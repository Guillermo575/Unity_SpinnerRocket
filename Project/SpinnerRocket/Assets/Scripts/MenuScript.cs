using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{

    #region Variables
    [Header("Menus")]
    public GameObject InitialMenu;
    public GameObject MenuTitle;
    public GameObject MenuStart;
    public GameObject MenuOptions;
    public GameObject MenuSound;
    public GameObject MenuGraphics;
    #endregion

    #region Start & Update
    void Start()
    {
        ClickShowMenu(InitialMenu == null ? MenuStart : InitialMenu);
    }
    void Update()
    {
    }
    #endregion

    #region General
    public void HideShowCanvas(bool value = false)
    {
        HideShowIfExist(MenuTitle);
        HideShowIfExist(MenuStart);
        HideShowIfExist(MenuOptions);
        HideShowIfExist(MenuSound);
        HideShowIfExist(MenuGraphics);
    }
    public void HideShowIfExist(GameObject objMenu, bool value = false)
    {
        if(objMenu != null && objMenu.activeSelf)
        {
            objMenu.SetActive(value);
        }
    }
    public void ReactivateFocus()
    {
        var obj = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(obj);
    }
    #endregion

    #region Menu Buttons
    public void ClickChangeScene(string NewScene)
    {
        SceneManager.LoadScene(NewScene);
    }
    public void ClickExitGame()
    {
        Application.Quit();
    }
    public void ClickShowMenu(GameObject objMenu)
    {
        if(objMenu != null)
        {
            HideShowCanvas();
            objMenu.SetActive(true);
            Button[] buttons = this.GetComponentsInChildren<Button>();
            Slider[] Sliders = this.GetComponentsInChildren<Slider>();
            if (buttons.Length > 0)
            {
                EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
            }
            if (Sliders.Length > 0)
            {
                EventSystem.current.SetSelectedGameObject(Sliders[0].gameObject);
            }
            ReactivateFocus();
        }
    }
    public void ClickResumeGame()
    {
        ClickShowMenu(MenuStart);
        this.gameObject.SetActive(false);
        ReactivateFocus();
    }
    #endregion

}
