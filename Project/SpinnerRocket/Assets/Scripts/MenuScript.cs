using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{

    #region Variables
    [Header("Menus")]
    public GameObject InitialMenu;
    public GameObject MenuTitle;
    public GameObject MenuStart;
    public GameObject MenuOptions;
    public GameObject MenuSound;
    public GameObject MenuGraphics;
    #endregion

    #region Hidden Variables
    [HideInInspector] public const float defaultVolume = 1.0f;
    [HideInInspector] public const float defaultSound = 1.0f;
    [HideInInspector] public const float defaultBrightness = 1.0f;
    #endregion

    #region Start & Update
    void Start()
    {
        ClickShowMenu(InitialMenu == null ? MenuStart : InitialMenu);
    }
    void Update()
    {
        if (MenuTitle != null && MenuTitle.activeSelf)
        {
            if (Input.anyKey && !string.IsNullOrEmpty(Input.inputString))
            {
                ClickShowMenu(MenuStart);
            }
        }
    }
    #endregion

    #region General
    public void HideShowCanvas(bool value = false)
    {
        HideShowIfExist(MenuTitle);
        HideShowIfExist(MenuStart);
        HideShowIfExist(MenuOptions);
        HideShowIfExist(MenuSound);
        HideShowIfExist(MenuGraphics);
    }
    public void HideShowIfExist(GameObject objMenu, bool value = false)
    {
        if(objMenu != null && objMenu.activeSelf)
        {
            objMenu.SetActive(value);
        }
    }
    #endregion

    #region Menu Buttons
    public void ClickChangeScene(string NewScene)
    {
        SceneManager.LoadScene(NewScene);
    }
    public void ClickExitGame()
    {
        Application.Quit();
    }
    public void ClickShowMenu(GameObject objMenu)
    {
        if(objMenu != null)
        {
            HideShowCanvas();
            objMenu.SetActive(true);
            Button[] buttons = this.GetComponentsInChildren<Button>();
            if(buttons.Length > 0)
            {
                EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
            }
        }
    }
    public void ClickResumeGame()
    {
        ClickShowMenu(MenuStart);
        this.gameObject.SetActive(false);
        GameManager.Resumegame();
    }
    #endregion

    #region Reset Buttons
    public void ResetVolume()
    {
        Slider SliderVolume = GameObject.Find("SliderMusic").GetComponent<Slider>();
        Slider SliderSound = GameObject.Find("SliderSound").GetComponent<Slider>();
        PlayerPrefs.SetFloat("masterVolume", defaultVolume);
        PlayerPrefs.SetFloat("masterSound", defaultSound);
        SliderVolume.value = defaultVolume;
        SliderSound.value = defaultSound;
        TextMeshProUGUI LabelSoundStatus = GameObject.Find("LabelSoundStatus").GetComponent<TextMeshProUGUI>();
        LabelSoundStatus.text = "Changes restored!";
    }
    public void ResetGraphics()
    {
        Slider SliderBrightness = GameObject.Find("SliderBrightness").GetComponent<Slider>();
        PlayerPrefs.SetFloat("masterBrightness", defaultBrightness);
        SliderBrightness.value = defaultBrightness;
        TextMeshProUGUI LabelGraphicStatus = GameObject.Find("LabelGraphicStatus").GetComponent<TextMeshProUGUI>();
        LabelGraphicStatus.text = "Changes restored!";
    }
    #endregion

    #region Apply Buttons
    public void ApplyVolume()
    {
        Slider SliderVolume = GameObject.Find("SliderMusic").GetComponent<Slider>();
        Slider SliderSound = GameObject.Find("SliderSound").GetComponent<Slider>();
        PlayerPrefs.SetFloat("masterVolume", SliderVolume.value);
        PlayerPrefs.SetFloat("masterSound", SliderSound.value);
        Debug.Log(PlayerPrefs.GetFloat("masterVolume") + " " + PlayerPrefs.GetFloat("masterSound"));
        TextMeshProUGUI LabelSoundStatus = GameObject.Find("LabelSoundStatus").GetComponent<TextMeshProUGUI>();
        LabelSoundStatus.text = "Changes Saved!";
    }
    public void ApplyGraphics()
    {
        Slider SliderBrightness = GameObject.Find("SliderBrightness").GetComponent<Slider>();
        PlayerPrefs.SetFloat("masterBrightness", SliderBrightness.value);
        Debug.Log(PlayerPrefs.GetFloat("masterBrightness"));
        TextMeshProUGUI LabelGraphicStatus = GameObject.Find("LabelGraphicStatus").GetComponent<TextMeshProUGUI>();
        LabelGraphicStatus.text = "Changes Saved!";
    }
    #endregion

}
