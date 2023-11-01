using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(fileName = "InputReader", menuName = "SO/Input/InputReader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event Action<Vector2> MovementEvent;
    public event Action<bool> FireEvent;

    public Vector2 AimPosition { get; private set; } 
    private Controls _playerInputAction; //싱글톤으로 사용할 녀석

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
        if (context.performed)
        {
            FireEvent?.Invoke(true);
        }
        else if (context.canceled)
        {
            FireEvent?.Invoke(false);
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        AimPosition = context.ReadValue<Vector2>(); 
    }
}