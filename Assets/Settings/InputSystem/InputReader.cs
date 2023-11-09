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
    private Controls _playerInputAction; //�̱������� ����� �༮

    private bool isClicking = false;

    private void OnEnable()
    {
        if (_playerInputAction == null)
        {
            _playerInputAction = new Controls();
            _playerInputAction.Player.SetCallbacks(this); //�÷��̾� ��ǲ�� �߻��ϸ� �� �ν��Ͻ��� ����
        }

        _playerInputAction.Player.Enable(); //Ȱ��ȭ
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        MovementEvent?.Invoke(value);
    }

    public void OnFire(InputAction.CallbackContext context) //���߿�
    {
        if (context.started)
        {
            // ���콺 ���� ��ư�� ó�� ������ �� ����˴ϴ�.
            isClicking = true;
            OnStartFireEvent?.Invoke();
        }
        else if (context.canceled)
        {
            // ���콺 ���� ��ư�� ������ �� ����˴ϴ�.
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