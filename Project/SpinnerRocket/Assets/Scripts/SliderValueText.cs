using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SliderValueText : MonoBehaviour
{
    [SerializeField]
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