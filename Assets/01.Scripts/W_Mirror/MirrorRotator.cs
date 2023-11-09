using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using static Define.Define;

public enum DistanceState
{
    Inside,
    Outside
}

public class MirrorRotator : MonoBehaviour
{
    [SerializeField] private float _ableDistance;
    [SerializeField] private float _rotateSpeed;

    [SerializeField] private List<LookCamUI> _lookCamUI;

    private DistanceState _currentState;
    public DistanceState State
    {
        get => _currentState;
        set
        {
            _currentState = value;

            if (_currentState == DistanceState.Inside)
            {
                ShowUI(1, 0.4f);
            }
            else if (_currentState == DistanceState.Outside)
            {
                ShowUI(0, 0.4f);
            }
        }
    }

    private void Awake()
    {
        State = DistanceState.Outside;
    }

    private void Update()
    {
        RotateMirror();
    }

    private void RotateMirror()
    {
        if (Vector3.Distance(transform.position, PlayerTrm.position) < _ableDistance && !TutorialManager.Instance.IsActive)
        {
            State = DistanceState.Inside;

            if (Keyboard.current.qKey.isPressed)
                transform.Rotate(new Vector3(0, 1, 0) * _rotateSpeed * Time.deltaTime);

            if (Keyboard.current.eKey.isPressed)
                transform.Rotate(new Vector3(0, -1, 0) * _rotateSpeed * Time.deltaTime);
        }
        else
        {
            State = DistanceState.Outside;
        }
    }

    private void ShowUI(float value, float time)
    {
        _lookCamUI.ForEach(p => p.Fade(value, time));
    }
}
