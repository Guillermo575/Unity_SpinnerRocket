using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject MenuStart;
    public GameObject MenuOptions;
    [Header("Levels To Load")]
    public string _newGameButtonLevel;
    void Start()
    {       
    }
    void Update()
    {       
    }
    public void ClickNewGameDialog()
    {
        SceneManager.LoadScene(_newGameButtonLevel);
    }
    public void ClickExitGame()
    {
        Application.Quit();
    }
    public void ClickShowOptions()
    {
        HideCanvas();
        MenuOptions.SetActive(true);
    }
    public void ClickShowStart()
    {
        HideCanvas();
        MenuStart.SetActive(true);
    }
    public void HideCanvas()
    {
        MenuStart.SetActive(false);
        MenuOptions.SetActive(false);
    }
}
