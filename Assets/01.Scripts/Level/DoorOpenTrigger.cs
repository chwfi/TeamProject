using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenTrigger : MonoBehaviour
{
    [SerializeField, Tooltip("목표 색")]
    private Color targetColor;

    [SerializeField]
    private ColorDoor linkedDoor; // 열어줄 문

    bool _isOpend = false;

    public void ColorMatch(Color inputColor) // 다른 함수에서 실행하여 비교 함
    {
        if (ColorSystem.CompareColor(inputColor, targetColor) && !_isOpend)
        {
            linkedDoor.OpenDoor(); // 같은 색이라면 문 염
            _isOpend = true;
        }
    }
}
