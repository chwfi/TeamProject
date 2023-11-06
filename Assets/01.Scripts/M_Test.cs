using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Test : MonoBehaviour
{
    [SerializeField]
    private Color color1;

    [SerializeField]
    private Color color2;

    [SerializeField]
    private Color targetColor;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            color1 = Color.red;
            color2 = Color.blue;

            Debug.Log($"curColor: {ColorUtility.ToHtmlStringRGB(ColorSystem.GetColorCombination(color1, color2))}");
            Debug.Log($"targetColor: {ColorUtility.ToHtmlStringRGB(targetColor)}");
        }

        if (ColorSystem.CompareColor(ColorSystem.GetColorCombination(color1, color2), targetColor))
        {
            Debug.Log("°°´Ù");
        }
    }
}
