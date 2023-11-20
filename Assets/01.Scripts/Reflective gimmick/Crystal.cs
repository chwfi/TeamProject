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
        new Vector3(0,-0.5f,0.3f),
        new Vector3(-90f,20 ,0),
        new Vector3(25,25,25),
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
        Debug.Log("SetReflectDataModify");
        ReflectiveObject refObj = OnShootRaycast<ReflectiveObject>(reflectData.hitPos, reflectData.direction);

        ChangedReflectObject(refObj);
        refObj?.OnReflectTypeChanged(ReflectState.OnReflect);
        refObj?.GetReflectedObjectDataModify(myReflectData);

        TriangluarPlane triPlane = OnShootRaycast<TriangluarPlane>(reflectData.hitPos, reflectData.direction);
        triPlane?.GetReflectedObjectDataModify(reflectData);

        OnShootRaycast<ReflectToReflect>(reflectData.hitPos, reflectData.direction);
        OnShootRaycast<ReflectToUp>(reflectData.hitPos, reflectData.direction);

        OnShootRaycast<DoorOpenTrigger>(reflectData.hitPos, reflectData.direction);
    }
}
