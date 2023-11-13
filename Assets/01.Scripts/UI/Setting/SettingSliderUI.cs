using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingSliderUI : MonoBehaviour
{
    [SerializeField] private Image _filledImage;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Update()
    {
        _filledImage.fillAmount = _slider.value;
    }
}
