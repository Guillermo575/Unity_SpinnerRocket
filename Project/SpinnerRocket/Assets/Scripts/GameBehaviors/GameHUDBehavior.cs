using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameHUDBehavior : MonoBehaviour
{
    [HideInInspector] public GameManager objGameManager;
    public GameObject HUD;
    public Image btnSoundON;
    public Image btnSoundOFF;
    public TextMeshProUGUI txtScore;
    public TextMeshProUGUI txtHighScore;
    void Start()
    {
        objGameManager = GameObject.Find("GameManager").gameObject.GetComponentsInChildren<GameManager>(true)[0];
        HUD.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.5f * (1 - PlayerPrefs.GetFloat("masterBrightness", 1)));
    }
    void Update()
    {
        setScore();
        btnSoundON.enabled = !GameManager.MuteGame;
        btnSoundOFF.enabled = GameManager.MuteGame;
        if (PlayerPrefs.GetInt("GraphicsChanged", 0) == 1)
        {
            HUD.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.5f * (1 - PlayerPrefs.GetFloat("masterBrightness", 1)));
            PlayerPrefs.SetInt("GraphicsChanged", 0);
        }
        if (!objGameManager.StartGame)
        {
            HUD.SetActive(false);
        }
        if (objGameManager.StartGame && objGameManager.GameOver)
        {
            HUD.SetActive(false);
        }
        if (objGameManager.StartGame && !objGameManager.GameOver && !GameManager.PauseGame)
        {
            HUD.SetActive(true);
        }
    }
    public void setScore()
    {
        if (txtScore != null)
        {
            txtScore.text = "Score: " + objGameManager.Score;
        }
        if (PlayerPrefs.GetInt("HighScore", 0) < objGameManager.Score)
        {
            PlayerPrefs.SetInt("HighScore", objGameManager.Score);
        }
        if (txtHighScore != null)
        {
            txtHighScore.text = "High: " + PlayerPrefs.GetInt("HighScore", 0);
        }
    }
}