using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuScript : MonoBehaviour
{
    #region Variables
    [Header("Menus")]
    public List<GameObject> lstMenuTree;
    public GameObject InitialMenu;
    public GameObject GameOverMenu;
    public GameObject LevelClearedMenu;
    #endregion

    #region Start & Update
    void Start()
    {
        lstMenuTree = new List<GameObject>();
        ClickShowMenu(InitialMenu);
    }
    void Update()
    {
    }
    #endregion

    #region General
    public void HideShowCanvas(bool value = false)
    {
        var lst = this.gameObject.GetComponentsInChildren<Canvas>(true);
        foreach(var x in lst)
        {
            HideShowIfExist(x.gameObject);
        }
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
            lstMenuTree.Add(objMenu);
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
    public void ClickBackMenu()
    {
        if(lstMenuTree.Count > 1)
        {
            var objBack = lstMenuTree[lstMenuTree.Count - 2];
            lstMenuTree.Remove(lstMenuTree[lstMenuTree.Count - 1]);
            ClickShowMenu(objBack);
            lstMenuTree.Remove(lstMenuTree[lstMenuTree.Count - 1]);
        }
    }
    public void ClickResumeGame()
    {
        lstMenuTree = new List<GameObject>();
        ClickShowMenu(InitialMenu);
        this.gameObject.SetActive(false);
        ReactivateFocus();
    }
    public void ClickGameOver()
    {
        ClickShowMenu(GameOverMenu);
        this.gameObject.SetActive(true);
        ReactivateFocus();
    }
    public void ClickLevelCleared()
    {
        ClickShowMenu(LevelClearedMenu);
        this.gameObject.SetActive(true);
        ReactivateFocus();
    }
    #endregion
}