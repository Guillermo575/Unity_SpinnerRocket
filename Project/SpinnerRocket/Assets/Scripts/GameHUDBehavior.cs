using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameHUDBehavior : MonoBehaviour
{
    [HideInInspector] public GameManager objGameManager;
    public Image btnSoundON;
    public Image btnSoundOFF;
    public TextMeshProUGUI txtScore;
    public TextMeshProUGUI txtHighScore;
    void Start()
    {
        objGameManager = GameObject.Find("GameManager").gameObject.GetComponentsInChildren<GameManager>(true)[0];
    }
    void Update()
    {
        setScore();
        btnSoundON.enabled = !GameManager.MuteGame;
        btnSoundOFF.enabled = GameManager.MuteGame;
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
