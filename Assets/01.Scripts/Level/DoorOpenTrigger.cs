using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenTrigger : MonoBehaviour
{
    [SerializeField, Tooltip("��ǥ ��")]
    private Color targetColor;

    [SerializeField]
    private ColorDoor linkedDoor; // ������ ��

    public void ColorMatch(Color inputColor) // �ٸ� �Լ����� �����Ͽ� �� ��
    {
        if (ColorSystem.CompareColor(inputColor, targetColor))
        {
            linkedDoor.OpenDoor(); // ���� ���̶�� �� ��
        }
    }
}
