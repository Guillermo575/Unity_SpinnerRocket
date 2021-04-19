using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    #region Hidden Variables
    [Header("Hidden")]
    [HideInInspector] public bool StartGame = false;
    [HideInInspector] public static bool PauseGame = false;
    [HideInInspector] public bool GameOver = false;
    [HideInInspector] public static bool MuteGame;
    [HideInInspector] public List<GameObject> lstStar;
    [HideInInspector] public List<GameObject> lstAsteroid;
    [HideInInspector] public int Score = 0;
    [HideInInspector] public bool SkipStart = true;
    [HideInInspector] public AudioClip ClipBGM;
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
    public GameObject MenuStart;
    public GameObject MenuGameOver;
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

    #region General
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
            GameScreen();
            CheckPause();
        }
        if (PauseGame)
        {
            PauseScreen();
        }
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
        btnSoundON.enabled = !MuteGame;
        btnSoundOFF.enabled = MuteGame;
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
    #endregion

    #region Screens
    public void StartMenuGame()
    {
        MenuStart.SetActive(true);
        MenuPause.SetActive(false);
        HUD.SetActive(false);
        if (Input.GetKeyDown(KeyCode.X) || SkipStart)
        {
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
                lstStar.Add(Instantiate(objStar, new Vector2(UnityEngine.Random.Range(minValues.x, maxValues.x), UnityEngine.Random.Range(minValues.y, maxValues.y)), Quaternion.identity));
            }
            for (int i = 0; i < 6; i++)
            {
                lstAsteroid.Add(Instantiate(objAsteroid, objAsteroid.GetComponent<Obstacle>().getRandomSpawnPoint(), Quaternion.identity));
            }
        };
    }
    public void GameScreen()
    {
        if (!PauseGame && !MuteGame)
        {
            objAudioMusic.UnPause();
        }
        MenuStart.SetActive(false);
        MenuPause.SetActive(false);
        HUD.SetActive(true);
        FollowCamera();
    }
    public void GameOverScreen()
    {
        objAudioMusic.Stop();
        MenuGameOver.SetActive(true);
        HUD.SetActive(false);
        if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public void PauseScreen()
    {
        MenuPause.SetActive(true);
    }
    public void CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseGame)
            {
                var objMenu = MenuPause.GetComponent<MenuScript>();
                if(objMenu != null)
                {
                    objMenu.ClickResumeGame();
                }
                else
                {
                    PauseGame = false;
                    Time.timeScale = 1;
                }
                if (!MuteGame)
                {
                    objAudioMusic.UnPause();
                }
            }
            else
            {
                PauseGame = true;
                Time.timeScale = 0;
                objAudioMusic.Pause();
            }
        }
    }
    public static void Resumegame()
    {
        PauseGame = false;
        Time.timeScale = 1;
    }
    #endregion

}