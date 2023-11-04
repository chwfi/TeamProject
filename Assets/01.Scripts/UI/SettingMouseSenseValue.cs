using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingMouseSenseValue : MonoBehaviour
{
    private TextMeshProUGUI _valueText;
    private float _valueScale = 1.5f;
    public float ValueScale => _valueScale;

    private void Awake()
    {
        _valueText = GetComponent<TextMeshProUGUI>();
        SetValueText();
    }

    public void SetMouseValue(bool value)
    {
        if (value)
        {
            _valueScale += 0.25f;
        }
        else
        {
            _valueScale -= 0.25f;
        }

        _valueScale = Mathf.Clamp(_valueScale, 0.25f, 3f);
        SetValueText();
    }

    private void SetValueText()
    {
        _valueText.text = $"{_valueScale}";
    }
}
