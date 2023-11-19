using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Define.Define;

public class DoorOpenTrigger : MonoBehaviour
{
    [SerializeField, Tooltip("��ǥ ��")]
    private Color targetColor;

    [SerializeField]
    private ColorDoor linkedDoor; // ������ ��

    [SerializeField] private ArrowUI arrowUI; //ó���� ������ ũ����Ż ���� ȭ��ǥ.

    private List<Rigidbody> _rigids = new List<Rigidbody>();

    bool _isOpend = false;

    private void Awake()
    {
        _rigids.AddRange(GetComponentsInChildren<Rigidbody>());

        Canvas canvas = GetComponentInChildren<Canvas>(); //ĵ������ ī�޶� �־���
        if (canvas != null)
        {
            canvas.worldCamera = MainCam;
        }
    }

    public void ColorMatch(Color inputColor) // �ٸ� �Լ����� �����Ͽ� �� ��
    {
        if (ColorSystem.CompareColor(inputColor, targetColor) && !_isOpend)
        {
            linkedDoor.OpenDoor(); // ���� ���̶�� �� ��
            arrowUI?.FadeToDisable(); //ȭ��ǥ UI Fade�� Destroy
            SoundManager.Instance.PlaySFXSound("StoneFall");

            _rigids.ForEach(v => //ü�ε� ����߸���
            {
                v.useGravity = true;
                Destroy(v.gameObject, 2f);
            });

            _isOpend = true;
        }
    }
}
