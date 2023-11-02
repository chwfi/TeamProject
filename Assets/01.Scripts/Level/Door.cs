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
    [SerializeField, Tooltip("목표 색")]
    private ColorM goalColor; //목표 색
    public ColorM curColor; //현재 문의 색 테스트 용으로 public. 나중에 private로.

    Animator _animator;

    private bool isOpen => curColor == goalColor; // 현재 color가 목표 color가 되면 true. 문을 연다
    
    private readonly int openDoorHash = Animator.StringToHash("OpenDoorTrigger");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        curColor = default(ColorM); //현재 문의 색 초기화
    }

    private void Update()
    {
        if (isOpen) { _animator.SetTrigger(openDoorHash); curColor = default(ColorM); } //문 열고 색 초기화
    }

    public void ChangeColor(ColorM nextColor) //밖에서 실행할 색 바꾸는 함수
    {
        curColor = nextColor;
    }
}
