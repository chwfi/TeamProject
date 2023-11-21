using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
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
    public float _keyPressTime = 0.2f;
    public float KeyPressTime
    {
        get { return _keyPressTime; }
        set
        {
            _keyPressTime = Mathf.Clamp(value, 0.2f, 15f);
        }
    }

    private float RotationSpeed => UIManager.Instance.settingRotUI.ValueScale;
    //private float RotationSpeed => 10;

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

            if (Input.GetKeyDown(KeyCode.Q))
            {
                SoundManager.Instance.PlaySFXSound(SFX.Rotate);
            }

            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(new Vector3(0, 1, 0) * RotationSpeed * KeyPressTime * Time.deltaTime);
                KeyPressTime += Time.deltaTime * 4f;
            }
            if (Input.GetKeyUp(KeyCode.Q))
            {
                SoundManager.Instance.PauseSFXSound(SFX.Rotate);
                KeyPressTime = 0.2f;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                SoundManager.Instance.PlaySFXSound(SFX.Rotate);
            }

            if (Input.GetKey(KeyCode.E))
            {     
                transform.Rotate(new Vector3(0, -1, 0) * RotationSpeed * KeyPressTime * Time.deltaTime);
                KeyPressTime += Time.deltaTime * 4f;
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                SoundManager.Instance.PauseSFXSound(SFX.Rotate);
                KeyPressTime = 0.2f;
            }

            return State = DistanceState.Inside;
        }
        else
        {
            return State = DistanceState.Outside;
        }
    }
}
