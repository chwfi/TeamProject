using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CameraShaking : MonoBehaviour
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
        _animator.SetBool("isMoving", isMoving);
    }
}
