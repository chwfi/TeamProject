using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingMouseSenseValue : MonoBehaviour
{
    private TextMeshProUGUI _valueText;

    public float ValueScale
    {
        get;
        set;
    }

    private void Awake()
    {
        ValueScale = 1.5f;
        _valueText = GetComponent<TextMeshProUGUI>();
        SetValueText();
    }

    public void SetMouseValue(bool value)
    {
        if (value)
        {
            ValueScale += 0.25f;
        }
        else
        {
            ValueScale -= 0.25f;
        }

        ValueScale = Mathf.Clamp(ValueScale, 0.25f, 3f);
        SetValueText();
    }

    private void SetValueText()
    {
        _valueText.text = $"{ValueScale}";
    }
}
