using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    #region Hidden & Static Variables
    [Header("Hidden")]
    [HideInInspector] public bool StartGame = false;
    [HideInInspector] public bool GameOver = false;
    [HideInInspector] public int Score = 0;
    [HideInInspector] public AudioClip ClipBGM;
    [HideInInspector] public MathRNG objMathRNG = new MathRNG(45289574);

    [Header("Static")]
    [HideInInspector] public static bool PauseGame = false;
    [HideInInspector] public static bool MuteGame;
    [HideInInspector] public static bool BlockKeyBoard = false;
    #endregion

    #region Editor Variables 
    [Header("Audio")]
    public AudioSource objAudioMusic;
    public AudioSource objAudioSound;

    [Header("Menu")]
    public GameObject HUD;

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
    }
    void Update()
    {
        CheckConfig();
    }
    #endregion

    #region General
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
}