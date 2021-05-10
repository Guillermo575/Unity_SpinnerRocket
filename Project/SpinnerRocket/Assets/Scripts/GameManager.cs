using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    #region Hidden Variables
    [Header("Hidden")]
    [HideInInspector] public bool StartGame = false;
    [HideInInspector] public bool GameOver = false;
    [HideInInspector] public List<GameObject> lstStar;
    [HideInInspector] public List<GameObject> lstAsteroid;
    [HideInInspector] public int Score = 0;
    [HideInInspector] public AudioClip ClipBGM;
    [HideInInspector] public MathRNG objMathRNG = new MathRNG(45289574);
    #endregion

    #region Static variables
    [Header("Static")]
    [HideInInspector] public static bool PauseGame = false;
    [HideInInspector] public static bool MuteGame;
    [HideInInspector] public static bool BlockKeyBoard = false;
    #endregion

    #region Editor Variables 
    [Header("Objects")]
    public GameObject objStar;
    public GameObject objAsteroid;
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

    [Header("Camera")]
    public Camera Camera;
    public Transform target;
    public Vector3 offset;
    [Range(0, 10)]
    public float smoothFactor;
    #endregion

    #region Start & Update
    void Start()
    {
        StartGame = false;
        PauseGame = false;
        GameOver = false;
        objAsteroid.GetComponent<Obstacle>().GameManager = this;
        Score = 0;
        ClipBGM = Resources.Load<AudioClip>("Audio/magical_light_parade");
        objAudioMusic.clip = ClipBGM;
        objAudioMusic.volume = PlayerPrefs.GetFloat("masterVolume", 1);
        objAudioSound.volume = PlayerPrefs.GetFloat("masterSound", 1);
        HUD.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.5f * (1 - PlayerPrefs.GetFloat("masterBrightness", 1)));
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
    void FollowCamera()
    {
        float height = 2f * Camera.orthographicSize;
        float width = height * Camera.aspect;
        Vector3 targetPosition = target.position + offset;
        Vector3 boundPosition = new Vector3(Mathf.Clamp(targetPosition.x, minValues.x + (width / 2), maxValues.x - (width / 2)), Mathf.Clamp(targetPosition.y, minValues.y + (height / 2), maxValues.y - (height / 2)), -10);
        Vector3 smoothPosition = Vector3.Lerp(Camera.transform.position, boundPosition, smoothFactor * Time.fixedDeltaTime);
        Camera.transform.position = (smoothFactor == 0) ? boundPosition : smoothPosition;
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
        for (int i = 0; i < 3; i++)
        {
            lstStar.Add(Instantiate(objStar, new Vector2((float)objMathRNG.NextValue(-9, 9), (float)objMathRNG.NextValue(-5, 5)), Quaternion.identity));
        }
        for (int i = 0; i < 6; i++)
        {
            lstAsteroid.Add(Instantiate(objAsteroid, objAsteroid.GetComponent<Obstacle>().getRandomSpawnPoint(), Quaternion.identity));
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
        FollowCamera();
    }
    public void GameOverScreen()
    {
        var objMenu = MenuPause.GetComponent<MenuScript>();
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
                var objMenu = MenuPause.GetComponent<MenuScript>();
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
                var objMenu = MenuPause.GetComponent<MenuScript>();
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