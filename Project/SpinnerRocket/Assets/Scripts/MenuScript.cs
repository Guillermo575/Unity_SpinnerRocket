using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{

    #region Variables
    [Header("Menus")]
    public GameObject MenuMain;
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

    #region General
    void Start()
    {
        HideShowCanvas();
        MenuStart.SetActive(true);
    }
    void Update()
    {
    }
    public void HideShowCanvas(bool value = false)
    {
        if (MenuStart.activeSelf)
            MenuStart.SetActive(value);
        if (MenuOptions.activeSelf) 
            MenuOptions.SetActive(value);
        if (MenuSound.activeSelf)
            MenuSound.SetActive(value);
        if (MenuGraphics.activeSelf)
            MenuGraphics.SetActive(value);
    }
    public void StartTextSlider()
    {
        if (MenuSound.activeSelf)
        {
            Slider SliderVolume = GameObject.Find("SliderMusic").GetComponent<Slider>();
            SliderVolume.value = PlayerPrefs.GetFloat("masterVolume");
            TextMeshProUGUI LabelMusicLevel = GameObject.Find("LabelMusicLevel").GetComponent<TextMeshProUGUI>();
            LabelMusicLevel.text = SliderVolume.value.ToString("0.0");
            Slider SliderSound = GameObject.Find("SliderSound").GetComponent<Slider>();
            SliderSound.value = PlayerPrefs.GetFloat("masterSound");
            TextMeshProUGUI LabelSoundLevel = GameObject.Find("LabelSoundLevel").GetComponent<TextMeshProUGUI>();
            LabelSoundLevel.text = SliderSound.value.ToString("0.0");
        }
        if (MenuGraphics.activeSelf)
        { 
            Slider SliderBrightness = GameObject.Find("SliderBrightness").GetComponent<Slider>();
            SliderBrightness.value = PlayerPrefs.GetFloat("masterBrightness");
            TextMeshProUGUI LabelBrightnessLevel = GameObject.Find("LabelBrightnessLevel").GetComponent<TextMeshProUGUI>();
            LabelBrightnessLevel.text = SliderBrightness.value.ToString("0.0");
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
        HideShowCanvas();
        objMenu.SetActive(true);
        StartTextSlider();
    }
    public void ClickResumeGame()
    {
        ClickShowMenu(MenuStart);
        MenuMain.SetActive(false);
        GameManager.Resumegame();
    }
    #endregion

    #region Sliders
    public void SliderVolume(float value)
    {
        TextMeshProUGUI LabelMusicLevel = GameObject.Find("LabelMusicLevel").GetComponent<TextMeshProUGUI>();
        LabelMusicLevel.text = value.ToString("0.0");
        TextMeshProUGUI LabelSoundStatus = GameObject.Find("LabelSoundStatus").GetComponent<TextMeshProUGUI>();
        LabelSoundStatus.text = "";
    }
    public void SliderSound(float value)
    {
        TextMeshProUGUI LabelSoundLevel = GameObject.Find("LabelSoundLevel").GetComponent<TextMeshProUGUI>();
        LabelSoundLevel.text = value.ToString("0.0");
        TextMeshProUGUI LabelSoundStatus = GameObject.Find("LabelSoundStatus").GetComponent<TextMeshProUGUI>();
        LabelSoundStatus.text = "";
    }
    public void SliderBrightness(float value)
    {
        TextMeshProUGUI LabelBrightnessLevel = GameObject.Find("LabelBrightnessLevel").GetComponent<TextMeshProUGUI>();
        LabelBrightnessLevel.text = value.ToString("0.0");
        TextMeshProUGUI LabelGraphicStatus = GameObject.Find("LabelGraphicStatus").GetComponent<TextMeshProUGUI>();
        LabelGraphicStatus.text = "";
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
