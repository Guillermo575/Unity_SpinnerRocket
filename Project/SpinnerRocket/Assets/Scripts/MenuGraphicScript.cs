using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuGraphicScript : MonoBehaviour
{
    #region Hidden Variables
    [HideInInspector] public const float defaultBrightness = 1.0f;
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
        Slider SliderBrightness = GameObject.Find("SliderBrightness").GetComponent<Slider>();
        PlayerPrefs.SetFloat("masterBrightness", SliderBrightness.value);
        Debug.Log(PlayerPrefs.GetFloat("masterBrightness"));
        TextMeshProUGUI LabelGraphicStatus = GameObject.Find("LabelGraphicStatus").GetComponent<TextMeshProUGUI>();
        LabelGraphicStatus.text = "Changes Saved!";
    }
    public void SetSlider()
    {
        Slider SliderBrightness = GameObject.Find("SliderBrightness").GetComponent<Slider>();
        SliderBrightness.value = PlayerPrefs.GetFloat("masterBrightness", 1);
    }
    public void ResetConfig()
    {
        Slider SliderBrightness = GameObject.Find("SliderBrightness").GetComponent<Slider>();
        PlayerPrefs.SetFloat("masterBrightness", defaultBrightness);
        SliderBrightness.value = defaultBrightness;
        TextMeshProUGUI LabelGraphicStatus = GameObject.Find("LabelGraphicStatus").GetComponent<TextMeshProUGUI>();
        LabelGraphicStatus.text = "Changes restored!";
    }
    #endregion
}
