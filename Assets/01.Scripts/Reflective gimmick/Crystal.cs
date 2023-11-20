using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Glow
{
    [SerializeField] private InputReader _inputReader;
    public override void OnPickUp()
    {
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
        StartShootLight(transform.position, transform.forward);
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
