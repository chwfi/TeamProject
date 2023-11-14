using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class LanternShaking : MonoBehaviour
{  
    [SerializeField]
    private float cameraShakeSensitive;

    Animator _animator;

    bool isMoving => GameManager.Instance.Player.CanMove && GameManager.Instance.Player.TargetSpeed > 0;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!TutorialManager.Instance.IsActive) //튜토리얼 팝업이 뜬 상태가 아니라면
            _animator.SetBool("isMoving", isMoving);
        else
            _animator.SetBool("isMoving", false);
    }
}
