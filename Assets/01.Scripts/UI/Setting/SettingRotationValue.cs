using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingRotationValue : MonoBehaviour
{
    private TextMeshProUGUI _valueText;

    public float ValueScale
    {
        get;
        set;
    }

    private void Awake()
    {
        ValueScale = 60;
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

        ValueScale = Mathf.Clamp(ValueScale, 20f, 120f);
        SetValueText();
    }

    private void SetValueText()
    {
        _valueText.text = $"{ValueScale}";
    }
}
