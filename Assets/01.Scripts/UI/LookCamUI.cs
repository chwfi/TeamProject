using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Define.Define;

public class LookCamUI : MonoBehaviour
{
    private TextMeshPro _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        Vector3 cameraRotation = MainCam.transform.rotation * Vector3.forward;
        Vector3 posTarget = transform.position + cameraRotation;
        Vector3 orientationTarget = MainCam.transform.rotation * Vector3.forward;
        transform.LookAt(posTarget);
    }

    public void Fade(float value, float time)
    {
        _text.DOFade(value, time);
    }
}
