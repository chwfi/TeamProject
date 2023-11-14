using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build;
using UnityEngine;

public static class ColorSystem
{
    public static Color GetColorCombination(Color v1, Color v2) //�� 2�� ���ؼ� ��ȯ
        => v1 + v2;

    // �� 2���� RGB ���� HTML ������ ���ڿ�(red: "#FF0000")�� ��ȯ�Ͽ� ��
    public static bool CompareColor(Color nowColor, Color targetColor)
    {
        return ColorUtility.ToHtmlStringRGB(nowColor) ==
               ColorUtility.ToHtmlStringRGB(targetColor);
    }
}
