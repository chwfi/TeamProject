using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build;
using UnityEngine;

public static class ColorSystem
{
    public static Color GetColorCombination(Color v1, Color v2) //색 2개 더해서 반환
        => v1 + v2;

    // 색 2개의 RGB 값을 HTML 형식의 문자열(red: "#FF0000")로 반환하여 비교
    public static bool CompareColor(Color nowColor, Color targetColor)
    {
        return ColorUtility.ToHtmlStringRGB(nowColor) ==
               ColorUtility.ToHtmlStringRGB(targetColor);
    }
}
