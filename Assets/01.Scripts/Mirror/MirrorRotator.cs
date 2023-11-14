using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using static Define.Define;

public class MirrorRotator : MonoBehaviour, ICheckDistance
{
    [SerializeField] private float _ableDistance;
    [SerializeField] private float _rotateSpeed;

    public bool CanFullRotate = false;
    public float _keyPressTime = 0;

    private float RotationSpeed => UIManager.Instance.settingRotUI.ValueScale;

    public List<KeyGuideUI> _lookCamUI = new List<KeyGuideUI>();

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
        CheckDistance();
    }

    private void ShowUI(float value, float time)
    {
        _lookCamUI.ForEach(p => p.Fade(value, time));
    }

    public DistanceState CheckDistance()
    {
        if (Vector3.Distance(transform.position, PlayerTrm.position) < _ableDistance && !TutorialManager.Instance.IsActive)
        {
            if (CanFullRotate)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0)
                    transform.Rotate(new Vector3(0, 0, -4) * RotationSpeed * Time.deltaTime);
                else if (Input.GetAxis("Mouse ScrollWheel") < 0)
                    transform.Rotate(new Vector3(0, 0, 4) * RotationSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(new Vector3(0, 1, 0) * RotationSpeed * _keyPressTime * Time.deltaTime);
                _keyPressTime += Time.deltaTime * 6f;
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                _keyPressTime = 0;
            }

            if (Keyboard.current.eKey.isPressed)
            {
                transform.Rotate(new Vector3(0, -1, 0) * RotationSpeed * Time.deltaTime);
            }

            return State = DistanceState.Inside;
        }
        else
        {
            return State = DistanceState.Outside;
        }
    }
}
