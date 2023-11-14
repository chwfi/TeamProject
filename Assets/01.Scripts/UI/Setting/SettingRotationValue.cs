using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingRotationValue : MonoBehaviour
{
    private TextMeshProUGUI _valueText;

    [SerializeField] private float _maxRotationValue = 90f;
    [SerializeField] private float _minRotationValue = 10f;

    public float ValueScale
    {
        get;
        set;
    }

    private void Awake()
    {
        ValueScale = 30; //초기값 설정
        _valueText = GetComponent<TextMeshProUGUI>();
        SetValueText();
    }

    public void SetMouseValue(bool value)
    {
        if (value)
        {
            ValueScale += 10f;
        }
        else
        {
            ValueScale -= 10f;
        }

        ValueScale = Mathf.Clamp(ValueScale, _minRotationValue, _maxRotationValue);
        SetValueText();
    }

    private void SetValueText()
    {
        _valueText.text = $"{ValueScale}";
    }
}
