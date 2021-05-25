using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    #region Hidden Variables
    [Header("Hidden")]
    [HideInInspector] public bool StartGame = false;
    [HideInInspector] public bool GameOver = false;
    [HideInInspector] public int Score = 0;
    [HideInInspector] public AudioClip ClipBGM;
    [HideInInspector] public MathRNG objMathRNG = new MathRNG(45289574);
    [HideInInspector] MenuScript objMenu;
    #endregion

    #region Static variables
    [Header("Static")]
    [HideInInspector] public static bool PauseGame = false;
    [HideInInspector] public static bool MuteGame;
    [HideInInspector] public static bool BlockKeyBoard = false;
    #endregion

    #region Editor Variables 
    [Header("Objects")]
    public GameObject objPlayer;

    [Header("Audio")]
    public AudioSource objAudioMusic;
    public AudioSource objAudioSound;

    [Header("Menu")]
    public GameObject HUD;
    public GameObject MenuPause;

    [Header("HUD")]
    public TextMeshProUGUI txtScore;
    public TextMeshProUGUI txtHighScore;
    public Image btnSoundON;
    public Image btnSoundOFF;

    [Header("Scene Bounds")]
    public Vector3 minValues;
    public Vector3 maxValues;
    #endregion

    #region Start & Update
    void Start()
    {
        StartGame = false;
        PauseGame = false;
        GameOver = false;
        Score = 0;
        ClipBGM = Resources.Load<AudioClip>("Audio/magical_light_parade");
        objAudioMusic.clip = ClipBGM;
        objAudioMusic.volume = PlayerPrefs.GetFloat("masterVolume", 1);
        objAudioSound.volume = PlayerPrefs.GetFloat("masterSound", 1);
        HUD.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.5f * (1 - PlayerPrefs.GetFloat("masterBrightness", 1)));
        objMenu = MenuPause.GetComponent<MenuScript>();
    }
    void Update()
    {
        if (!StartGame)
        {
            StartMenuGame();
        }
        if (StartGame && GameOver)
        {
            GameOverScreen();
        }
        if (StartGame && !GameOver)
        {
            if(!PauseGame)
            {
                GameScreen();
            }
            CheckPause();
        }
        setScore();
        CheckConfig();
        btnSoundON.enabled = !MuteGame;
        btnSoundOFF.enabled = MuteGame;
    }
    #endregion

    #region General
    void setScore()
    {
        if(txtScore != null)
        {
            txtScore.text = "Score: " + Score;
        }
        if (PlayerPrefs.GetInt("HighScore", 0) < Score)
        {
            PlayerPrefs.SetInt("HighScore", Score);
        }
        if (txtHighScore != null)
        {
            txtHighScore.text = "High: " + PlayerPrefs.GetInt("HighScore", 0);
        }
    }
    public void ToogleMute()
    {
        if (objAudioMusic.isPlaying)
        {
            objAudioMusic.Pause();
            MuteGame = true;
        }
        else
        {
            MuteGame = false;
            if (objAudioMusic.time != 0)
            {
                objAudioMusic.UnPause();
            }
            else
            {
                objAudioMusic.Play();
            }
        }
    }
    public void CheckConfig()
    {
        if (PlayerPrefs.GetInt("GraphicsChanged", 0) == 1)
        {
            HUD.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.5f * (1 - PlayerPrefs.GetFloat("masterBrightness", 1)));
            PlayerPrefs.SetInt("GraphicsChanged", 0);
        }
        if (PlayerPrefs.GetInt("VolumeChanged", 0) == 1)
        {
            objAudioMusic.volume = PlayerPrefs.GetFloat("masterVolume", 1);
            objAudioSound.volume = PlayerPrefs.GetFloat("masterSound", 1);
            if (!objAudioMusic.isPlaying && !PauseGame)
            {
                objAudioMusic.Play();
            }
            PlayerPrefs.SetInt("VolumeChanged", 0);
        }
    }
    #endregion

    #region Screens
    public void StartMenuGame()
    {
        MenuPause.SetActive(false);
        HUD.SetActive(false);
        StartGame = true;
        PauseGame = false;
        GameOver = false;
        Time.timeScale = 1;
        if (!MuteGame)
        {
            objAudioMusic.Play();
        }
        var lstObjects = this.gameObject.GetComponentsInChildren<SpawnObject>(true);
        foreach (var obj in lstObjects)
        {
            obj.objMathRNG = objMathRNG;
            obj.Spawn();
        }
    }
    public void GameScreen()
    {
        if (!PauseGame && !MuteGame)
        {
            objAudioMusic.UnPause();
        }
        MenuPause.SetActive(false);
        HUD.SetActive(true);
    }
    public void GameOverScreen()
    {
        Time.timeScale = 0;
        if (objMenu != null && !objMenu.gameObject.activeSelf)
        {
            objAudioMusic.Stop();
            HUD.SetActive(false);
            objMenu.ClickGameOver();
        }
    }
    #endregion

    #region Pause & Resume
    public void CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || (PauseGame && !MenuPause.activeSelf))
        {
            if (PauseGame)
            {
                if(objMenu != null)
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
                PauseGame = true;
                Time.timeScale = 0;
                objAudioMusic.Pause();
            }
        }
        if (!Input.GetKey("space") || Input.GetKeyUp("space"))
        {
            BlockKeyBoard = false;
        }
    }
    public void Resumegame()
    {
        PauseGame = false;
        Time.timeScale = 1;
        if (!MuteGame)
        {
            objAudioMusic.UnPause();
        }
        BlockKeyBoard = true;
    }
    #endregion
}