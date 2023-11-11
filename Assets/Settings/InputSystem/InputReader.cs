using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(fileName = "InputReader", menuName = "SO/Input/InputReader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event Action<Vector2> MovementEvent;

    public event Action OnStartFireEvent;
    public event Action OnShootingFireEvent;
    public event Action OnStopFireEvent;

    public Vector2 AimPosition { get; private set; }
    private Controls _playerInputAction; //싱글톤으로 사용할 녀석

    private bool isClicking = false;

    private void OnEnable()
    {
        if (_playerInputAction == null)
        {
            _playerInputAction = new Controls();
            _playerInputAction.Player.SetCallbacks(this); //플레이어 인풋이 발생하면 이 인스턴스를 연결
        }

        _playerInputAction.Player.Enable(); //활성화
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        MovementEvent?.Invoke(value);
    }

    public void OnFire(InputAction.CallbackContext context) //나중에
    {
        if (context.started)
        {
            // 마우스 왼쪽 버튼을 처음 눌렀을 때 실행됩니다.
            isClicking = true;
            OnStartFireEvent?.Invoke();
        }
        else if (context.canceled)
        {
            // 마우스 왼쪽 버튼을 놓았을 때 실행됩니다.
            isClicking = false;
            OnStopFireEvent?.Invoke();
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        AimPosition = context.ReadValue<Vector2>();
    }
    public void Update()
    {
        if (isClicking)
        {
            OnShootingFireEvent?.Invoke();
        }
    }
}