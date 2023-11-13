using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using static Define.Define;
using static UnityEngine.Rendering.DebugUI;

public enum DistanceState
{
    Inside,
    Outside
}

public class MirrorRotator : MonoBehaviour
{
    [SerializeField] private float _ableDistance;
    [SerializeField] private float _rotateSpeed;

    private float RotationSpeed => UIManager.Instance.settingRotUI.ValueScale;

    private List<KeyGuideUI> _lookCamUI = new List<KeyGuideUI>();

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
        _lookCamUI.AddRange(GetComponentsInChildren<KeyGuideUI>());
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
                transform.Rotate(new Vector3(0, 1, 0) * RotationSpeed * Time.deltaTime);

            if (Keyboard.current.eKey.isPressed)
                transform.Rotate(new Vector3(0, -1, 0) * RotationSpeed * Time.deltaTime);
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
