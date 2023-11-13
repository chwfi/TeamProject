using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Define.Define;

public abstract class LookCamUIModule : MonoBehaviour //카메라를 바라볼 UI들의 부모
{
    protected abstract void Awake();

    protected virtual void Update()
    {
        Vector3 cameraRotation = MainCam.transform.rotation * Vector3.forward;
        Vector3 posTarget = transform.position + cameraRotation;
        Vector3 orientationTarget = MainCam.transform.rotation * Vector3.forward;
        transform.LookAt(posTarget);
    }
}
