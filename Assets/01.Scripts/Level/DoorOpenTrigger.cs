using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Define.Define;

public class DoorOpenTrigger : MonoBehaviour, ICheckDistance
{
    [SerializeField, Tooltip("��ǥ ��")]
    private Color targetColor;

    [SerializeField]
    private ColorDoor linkedDoor; // ������ ��

    [SerializeField] private ArrowUI arrowUI; //ó���� ������ ũ����Ż ���� ȭ��ǥ.

    public List<Reflective> _reflectiveList; //���� ������ ��� ����Ʈ

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
        Canvas canvas = GetComponentInChildren<Canvas>(); //ĵ������ ī�޶� �־���
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
    public void ColorMatch(Color inputColor, Reflective reflective) // �ٸ� �Լ����� �����Ͽ� �� ��
    {
        if (curRef == reflective) return;

        curRef = reflective;
        _reflectiveList.Add(reflective);

        if (_reflectiveList.Count > 1)
        {
            this.gameObject.layer = LayerMask.NameToLayer("Default");
            linkedDoor.OpenDoor(); // ���� ���̶�� �� ��
            arrowUI?.FadeToDisable(); //ȭ��ǥ UI Fade�� Destroy
            SoundManager.Instance.PlaySFXSound(SFX.StoneFall);
            _isOpend = true;
        }

        if (ColorSystem.CompareColor(inputColor, targetColor) && !_isOpend)
        {
            this.gameObject.layer = LayerMask.NameToLayer("Default");
            linkedDoor.OpenDoor(); // ���� ���̶�� �� ��
            arrowUI?.FadeToDisable(); //ȭ��ǥ UI Fade�� Destroy
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
