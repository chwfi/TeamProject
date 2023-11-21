using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define.Define;

public class Crystal : Glow
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Transform _firePos;


    private Vector3[] SetTransform = //Æ÷Áö¼Ç
    {
        new Vector3(0.03f, -0.42f, 0.79f),
        new Vector3(-90f, 0f, 20f),
        new Vector3(22,22,22),
    };
    private float holdTime = .6f;
    public override void OnPickUp()
    {
        var rootTrm = transform.root;
        rootTrm.parent = PlayerTrm.Find("PlayerCameraRoot");

        rootTrm.DOLocalMove(SetTransform[0], holdTime);
        rootTrm.DOLocalRotate(SetTransform[1], holdTime);
        rootTrm.DOScale(SetTransform[2], holdTime);


        _inputReader.SetInputUser(this);

        _inputReader.OnStartFireEvent += OnStartShootLight;
        _inputReader.OnStopFireEvent += OnStopShootLight;
        _inputReader.OnShootingFireEvent += OnShootingLight;
    }

    public override void OnPutDown()
    {
        _inputReader.OnStartFireEvent -= OnStartShootLight;
        _inputReader.OnStopFireEvent -= OnStopShootLight;
        _inputReader.OnShootingFireEvent -= OnShootingLight;
    }

    public override void OnStartShootLight()
    {
        base.OnStartShootLight();
        Debug.Log("OnStartShootLight");
    }
    public override void OnStopShootLight()
    {
        base.OnStopShootLight();
        Debug.Log("OnStopShootLight");
    }
    public override void OnShootingLight()
    {
        //base.OnShootingLight();
        Debug.Log("OnShootingLight");
        StartShootLight(_firePos.position, _firePos.forward);
    }
    public override void SetReflectDataModify(ReflectData reflectData)
    {
        Darkstart refObj = OnShootRaycast<Darkstart>(reflectData.hitPos, reflectData.direction);

        ChangedReflectObject(refObj);
        refObj?.OnReflectTypeChanged(ReflectState.OnReflect);
        refObj?.ColorMatch(defaultColor);
    }
}
