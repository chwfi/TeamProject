using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : Glow
{
    [SerializeField] private InputReader _inputReader;
    protected override void Awake()
    {
        base.Awake();

        OnPickUp();
    }

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
    }
    public override void OnStopShootLight()
    {
        base.OnStopShootLight();
    }
    public override void OnShootingLight()
    {
        StartShootLight(transform.position, transform.forward);
    }
    public override void SetReflectDataModify(ReflectData reflectData)
    {
        ReflectiveObject refObj = OnShootRaycast<ReflectiveObject>(reflectData.hitPos, reflectData.direction);

        ChangedReflectObject(refObj);
        refObj?.OnReflectTypeChanged(ReflectState.OnReflect);
        refObj?.GetReflectedObjectDataModify(myReflectData);

        TriangluarPlane triPlane = OnShootRaycast<TriangluarPlane>(reflectData.hitPos, reflectData.direction);
        triPlane?.GetReflectedObjectDataModify(reflectData);

        CrystalCharging crystal = OnShootRaycast<CrystalCharging>(reflectData.hitPos, reflectData.direction);
        crystal?.OnCharging();

        OnShootRaycast<ReflectToReflect>(reflectData.hitPos, reflectData.direction);
        OnShootRaycast<ReflectToUp>(reflectData.hitPos, reflectData.direction);

        OnShootRaycast<DoorOpenTrigger>(reflectData.hitPos, reflectData.direction);
    }
    private void OnDestroy()
    {
        OnPutDown();
    }
}
