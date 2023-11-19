using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Define.Define;

public class DoorOpenTrigger : MonoBehaviour
{
    [SerializeField, Tooltip("목표 색")]
    private Color targetColor;

    [SerializeField]
    private ColorDoor linkedDoor; // 열어줄 문

    [SerializeField] private ArrowUI arrowUI; //처음에 나오는 크리스탈 위의 화살표.

    private List<Rigidbody> _rigids = new List<Rigidbody>();

    bool _isOpend = false;

    private void Awake()
    {
        _rigids.AddRange(GetComponentsInChildren<Rigidbody>());

        Canvas canvas = GetComponentInChildren<Canvas>(); //캔버스에 카메라 넣어줌
        if (canvas != null)
        {
            canvas.worldCamera = MainCam;
        }
    }

    public void ColorMatch(Color inputColor) // 다른 함수에서 실행하여 비교 함
    {
        if (ColorSystem.CompareColor(inputColor, targetColor) && !_isOpend)
        {
            linkedDoor.OpenDoor(); // 같은 색이라면 문 염
            arrowUI?.FadeToDisable(); //화살표 UI Fade후 Destroy
            SoundManager.Instance.PlaySFXSound("StoneFall");

            _rigids.ForEach(v => //체인들 떨어뜨리기
            {
                v.useGravity = true;
                Destroy(v.gameObject, 2f);
            });

            _isOpend = true;
        }
    }
}
