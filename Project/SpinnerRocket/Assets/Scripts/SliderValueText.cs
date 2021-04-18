using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueText : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The text shown will be formatted using this string.  {0} is replaced with the actual value")]
    public string formatText = "0.0";
    public TextMeshProUGUI tmproText;

    private void Start()
    {
        tmproText.text = GetComponentInParent<Slider>().value.ToString(formatText);
        GetComponentInParent<Slider>().onValueChanged.AddListener(HandleValueChanged);
    }
    private void HandleValueChanged(float value)
    {
        tmproText.text = value.ToString(formatText);
    }
}
