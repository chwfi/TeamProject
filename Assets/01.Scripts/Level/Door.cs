using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;

public enum ColorM
{
    Default,
    Red,
    Green,
    Blue
}

public class Door : MonoBehaviour
{
    [SerializeField, Tooltip("��ǥ ��")]
    private ColorM goalColor; //��ǥ ��
    public ColorM curColor; //���� ���� �� �׽�Ʈ ������ public. ���߿� private��.

    Animator _animator;

    private bool isOpen => curColor == goalColor; // ���� color�� ��ǥ color�� �Ǹ� true. ���� ����
    
    private readonly int openDoorHash = Animator.StringToHash("OpenDoorTrigger");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        curColor = default(ColorM); //���� ���� �� �ʱ�ȭ
    }

    private void Update()
    {
        if (isOpen) { _animator.SetTrigger(openDoorHash); curColor = default(ColorM); } //�� ���� �� �ʱ�ȭ
    }

    public void ChangeColor(ColorM nextColor) //�ۿ��� ������ �� �ٲٴ� �Լ�
    {
        curColor = nextColor;
    }
}
