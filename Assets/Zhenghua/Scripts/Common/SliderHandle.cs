using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderHandle : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    [SerializeField] private TMP_Text _valueText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _slider.onValueChanged.AddListener(OnSliderValueChange);
    }

    private void OnDestroy()
    {
        _slider.onValueChanged.RemoveListener(OnSliderValueChange);
    }


    private void OnSliderValueChange(float value)
    {
        _valueText.text = (value * 100f).ToString("0");
    }
    
    public void UpdateValue()
    {
        OnSliderValueChange(_slider.value);
    }
}
