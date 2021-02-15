using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    #region Variables
    [HideInInspector] public static AudioSource objAudio;
    [HideInInspector] public List<GameObject> lstStar;
    [HideInInspector] public List<GameObject> lstAsteroid;
    [HideInInspector] public bool GameOver = false;
    [HideInInspector] public bool StartGame = false;
    [HideInInspector] public bool PauseGame = false;
    [HideInInspector] public int Score = 0;

    [Header("Objects")]
    public GameObject objStar;
    public GameObject objAsteroid;
    public GameObject objPlayer;

    [Header("Menu")]
    public GameObject MenuStart;
    public GameObject MenuGameOver;
    public GameObject HUD;
    public GameObject MenuPause;

    [Header("HUD")]
    public TextMeshProUGUI txtScore;
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
        GameOver = false;
        StartGame = false;
        objAsteroid.GetComponent<Obstacle>().GameManager = this;
        Score = 0;
        objAudio = this.GetComponent<AudioSource>();
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
        txtScore.text = "Score: " + Score;
        btnSoundON.enabled = objAudio.isPlaying;
        btnSoundOFF.enabled = !objAudio.isPlaying;
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
    public void PauseMusic()
    {
        if(objAudio.isPlaying)
        {
            objAudio.Pause();
        }
        else
        {
            if(objAudio.time != 0)
            {
                objAudio.UnPause();
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
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartGame = true;
            GameOver = false;
            objAudio.Play();
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
        MenuStart.SetActive(false);
        MenuPause.SetActive(false);
        HUD.SetActive(true);
        FollowCamera();
    }
    public void GameOverScreen()
    { 
        objAudio.Stop();
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
            if(PauseGame)
            {
                PauseGame = false;
                Time.timeScale = 1;
                objAudio.UnPause();
            }
            else
            {
                PauseGame = true;
                Time.timeScale = 0;
                objAudio.Pause();
            }
        }
    }
    #endregion

    #region Menu Mouse Clicks
    public void MouseClick(string buttonType)
    {
        if (buttonType == "Sound")
        {
            PauseMusic();
        }
    }
    #endregion

}