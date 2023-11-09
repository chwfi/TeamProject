using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CameraShaking : MonoBehaviour
{
    [SerializeField]
    private float cameraShakeSensitive;

    CinemachineCameraOffset cameraOffset;

    Animator _animator;

    bool isShaking;
    bool isMoving => GameManager.Instance.Player.TargetSpeed > 0;

    private void Awake()
    {
        cameraOffset = GetComponent<CinemachineCameraOffset>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!TutorialManager.Instance.IsActive) //Ʃ�丮�� �˾��� �� ���°� �ƴ϶��
            _animator.SetBool("isMoving", isMoving);
        else
            _animator.SetBool("isMoving", false);
    }
}
