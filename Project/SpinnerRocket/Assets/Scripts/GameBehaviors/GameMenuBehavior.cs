using UnityEngine;
public class GameMenuBehavior : MonoBehaviour
{
    [HideInInspector] public MenuScript objMenu;
    [HideInInspector] public GameManager objGameManager;
    public GameObject MenuPause;

    #region Start & Update
    void Start()
    {
        objGameManager = GameObject.Find("GameManager").gameObject.GetComponentsInChildren<GameManager>(true)[0];
        objMenu = MenuPause.GetComponent<MenuScript>();
    }
    void Update()
    {
        if (!objGameManager.StartGame)
        {
            StartMenuGame();
        }
        if (objGameManager.StartGame && objGameManager.GameOver)
        {
            GameOverScreen();
        }
        if (objGameManager.StartGame && !objGameManager.GameOver)
        {
            if (!GameManager.PauseGame)
            {
                GameScreen();
            }
            CheckPause();
        }
    }
    #endregion

    #region Screens
    public void StartMenuGame()
    {
        MenuPause.SetActive(false);
        objGameManager.StartGame = true;
        GameManager.PauseGame = false;
        objGameManager.GameOver = false;
        Time.timeScale = 1;
        if (!GameManager.MuteGame)
        {
            objGameManager.objAudioMusic.Play();
        }
        var lstObjects = this.gameObject.GetComponentsInChildren<SpawnObject>(true);
        foreach (var obj in lstObjects)
        {
            obj.objMathRNG = objGameManager.objMathRNG;
            obj.Spawn();
        }
    }
    public void GameScreen()
    {
        if (!GameManager.PauseGame && !GameManager.MuteGame)
        {
            objGameManager.objAudioMusic.UnPause();
        }
        MenuPause.SetActive(false);
    }
    public void GameOverScreen()
    {
        Time.timeScale = 0;
        if (objMenu != null && !objMenu.gameObject.activeSelf)
        {
            objGameManager.objAudioMusic.Stop();
            objMenu.ClickGameOver();
        }
    }
    #endregion

    #region Pause & Resume
    public void CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || (GameManager.PauseGame && !MenuPause.activeSelf))
        {
            if (GameManager.PauseGame)
            {
                if (objMenu != null)
                {
                    objMenu.ClickResumeGame();
                }
                Resumegame();
                MenuPause.SetActive(false);
            }
            else
            {
                MenuPause.SetActive(true);
                if (objMenu != null)
                {
                    objMenu.ReactivateFocus();
                }
                GameManager.PauseGame = true;
                Time.timeScale = 0;
                objGameManager.objAudioMusic.Pause();
            }
        }
        if (!Input.GetKey("space") || Input.GetKeyUp("space"))
        {
            GameManager.BlockKeyBoard = false;
        }
    }
    public void Resumegame()
    {
        GameManager.PauseGame = false;
        Time.timeScale = 1;
        if (!GameManager.MuteGame)
        {
            objGameManager.objAudioMusic.UnPause();
        }
        GameManager.BlockKeyBoard = true;
    }
    #endregion
}
