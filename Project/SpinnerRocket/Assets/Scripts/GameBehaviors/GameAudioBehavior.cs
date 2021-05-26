using UnityEngine;
public class GameAudioBehavior : MonoBehaviour
{
    [HideInInspector] public GameManager objGameManager;
    [HideInInspector] public AudioClip ClipBGM;
    public AudioSource objAudioMusic;
    public AudioSource objAudioSound;
    void Start()
    {
        objGameManager = GameObject.Find("GameManager").gameObject.GetComponentsInChildren<GameManager>(true)[0];
        ClipBGM = Resources.Load<AudioClip>("Audio/magical_light_parade");
        objAudioMusic.clip = ClipBGM;
        objAudioMusic.volume = PlayerPrefs.GetFloat("masterVolume", 1);
        objAudioSound.volume = PlayerPrefs.GetFloat("masterSound", 1);
    }
    void Update()
    {
        if (!GameManager.MuteGame)
        {
            if (objGameManager.StartGame && !objAudioMusic.isPlaying)
            {
                objAudioMusic.Play();
            }
            if (!GameManager.PauseGame)
            {
                objAudioMusic.UnPause();
            }
            if (objGameManager.StartGame)
            {
                if (objGameManager.GameOver)
                {
                    objAudioMusic.Stop();
                }
                if (!objGameManager.GameOver)
                {
                    objAudioMusic.UnPause();
                }
            }
        }
        if (GameManager.PauseGame)
        {
            objAudioMusic.Pause();
        }
        if (PlayerPrefs.GetInt("VolumeChanged", 0) == 1)
        {
            objAudioMusic.volume = PlayerPrefs.GetFloat("masterVolume", 1);
            objAudioSound.volume = PlayerPrefs.GetFloat("masterSound", 1);
            if (!objAudioMusic.isPlaying && !GameManager.PauseGame)
            {
                objAudioMusic.Play();
            }
            PlayerPrefs.SetInt("VolumeChanged", 0);
        }
    }
    public void ToogleMute()
    {
        if (objAudioMusic.isPlaying)
        {
            objAudioMusic.Pause();
            GameManager.MuteGame = true;
        }
        else
        {
            GameManager.MuteGame = false;
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
}