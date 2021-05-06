using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MenuSoundScript : MonoBehaviour
{
    #region Hidden Variables
    [HideInInspector] public const float defaultVolume = 1.0f;
    [HideInInspector] public const float defaultSound = 1.0f;
    #endregion

    #region Start & Update
    void Start()
    {
        SetSlider();
    }

    void Update()
    {        
    }
    #endregion

    #region Interfaz
    public void ApplyChanges()
    {
        Slider SliderVolume = GameObject.Find("SliderMusic").GetComponent<Slider>();
        Slider SliderSound = GameObject.Find("SliderSound").GetComponent<Slider>();
        PlayerPrefs.SetFloat("masterVolume", SliderVolume.value);
        PlayerPrefs.SetFloat("masterSound", SliderSound.value);
        TextMeshProUGUI LabelSoundStatus = GameObject.Find("LabelSoundStatus").GetComponent<TextMeshProUGUI>();
        PlayerPrefs.SetInt("VolumeChanged", 1);
        LabelSoundStatus.text = "Changes Saved!";
    }
    public void SetSlider()
    {
        Slider SliderVolume = GameObject.Find("SliderMusic").GetComponent<Slider>();
        Slider SliderSound = GameObject.Find("SliderSound").GetComponent<Slider>();
        SliderVolume.value = PlayerPrefs.GetFloat("masterVolume", 1);
        SliderSound.value = PlayerPrefs.GetFloat("masterSound", 1);
    }
    public void ResetConfig()
    {
        Slider SliderVolume = GameObject.Find("SliderMusic").GetComponent<Slider>();
        Slider SliderSound = GameObject.Find("SliderSound").GetComponent<Slider>();
        PlayerPrefs.SetFloat("masterVolume", defaultVolume);
        PlayerPrefs.SetFloat("masterSound", defaultSound);
        SliderVolume.value = defaultVolume;
        SliderSound.value = defaultSound;
        TextMeshProUGUI LabelSoundStatus = GameObject.Find("LabelSoundStatus").GetComponent<TextMeshProUGUI>();
        PlayerPrefs.SetInt("VolumeChanged", 1);
        LabelSoundStatus.text = "Changes restored!";
    }
    #endregion
}