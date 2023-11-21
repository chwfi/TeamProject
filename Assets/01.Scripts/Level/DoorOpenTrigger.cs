using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Define.Define;

public class DoorOpenTrigger : MonoBehaviour, ICheckDistance
{
    [SerializeField, Tooltip("목표 색")]
    private Color targetColor;

    [SerializeField]
    private ColorDoor linkedDoor; // 열어줄 문

    [SerializeField] private ArrowUI arrowUI; //처음에 나오는 크리스탈 위의 화살표.

    public List<Reflective> _reflectiveList; //닿은 빛들을 담는 리스트

    bool _isOpend = false;

    private DistanceState _currentState;
    public DistanceState State
    {
        get => _currentState;
        set
        {
            _currentState = value;

            if (_currentState == DistanceState.Inside)
            {
                ChangeLayer("DoorCrystal");
            }
            else if (_currentState == DistanceState.Outside)
            {
                ChangeLayer("Default");
            }
        }
    }

    private void Awake()
    {
        Canvas canvas = GetComponentInChildren<Canvas>(); //캔버스에 카메라 넣어줌
        if (canvas != null)
        {
            canvas.worldCamera = MainCam;
        }
    }

    private void ChangeLayer(string value)
    {
        this.gameObject.layer = LayerMask.NameToLayer($"{value}");
    }

    private Reflective curRef;
    public void ColorMatch(Color inputColor, Reflective reflective) // 다른 함수에서 실행하여 비교 함
    {
        if (curRef == reflective) return;

        curRef = reflective;
        _reflectiveList.Add(reflective);

        if (_reflectiveList.Count > 1)
        {
            this.gameObject.layer = LayerMask.NameToLayer("Default");
            linkedDoor.OpenDoor(); // 같은 색이라면 문 염
            arrowUI?.FadeToDisable(); //화살표 UI Fade후 Destroy
            SoundManager.Instance.PlaySFXSound(SFX.StoneFall);
            _isOpend = true;
        }

        if (ColorSystem.CompareColor(inputColor, targetColor) && !_isOpend)
        {
            this.gameObject.layer = LayerMask.NameToLayer("Default");
            linkedDoor.OpenDoor(); // 같은 색이라면 문 염
            arrowUI?.FadeToDisable(); //화살표 UI Fade후 Destroy
            SoundManager.Instance.PlaySFXSound(SFX.StoneFall);
            _isOpend = true;
        }
    }

    private void Update()
    {
        if (!_isOpend)
            CheckDistance();
    }

    public DistanceState CheckDistance()
    {
        if (Vector3.Distance(PlayerTrm.position, this.transform.position) < 50)
        {
            return State = DistanceState.Inside;
        }
        else
        {
            return State = DistanceState.Outside;
        }
    }
}
