using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Door : MonoBehaviour
{
    [SerializeField, Tooltip("��ǥ ��")]
    private Color goalColor; //��ǥ ��
    private Color curColor; //���� ���� �� �׽�Ʈ ������ public. ���߿� private��.

    Animator _animator;

    private bool isOpen => curColor == goalColor; // ���� color�� ��ǥ color�� �Ǹ� true. ���� ����
    
    private readonly int openDoorHash = Animator.StringToHash("OpenDoorTrigger");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        curColor = default(Color); //���� ���� �� �ʱ�ȭ
    }

    private void Update()
    {
        if (isOpen) { _animator.SetTrigger(openDoorHash); curColor = default(Color); } //�� ���� �� �ʱ�ȭ
    }

    public void ChangeColor(Color nextColor) //�ۿ��� ������ �� �ٲٴ� �Լ�
    {
        curColor = nextColor;
    }
}
