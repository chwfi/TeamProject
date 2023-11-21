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
        //여기서 이미 SetReflectDataModify를 실행해줌
    }
    public override void SetReflectDataModify(ReflectData reflectData)
    {
        Reflective obj = OnShootRaycast<Reflective>(reflectData.hitPos, reflectData.direction);

        if (obj is not ReflectToReflect)
        {
            ChangedReflectObject(obj);
            obj?.OnReflectTypeChanged(ReflectState.OnReflect);
            obj?.GetReflectedObjectDataModify(myReflectData);
        }

        OnShootRaycast<ReflectToReflect>(reflectData.hitPos, reflectData.direction);
        OnShootRaycast<ReflectToUp>(reflectData.hitPos, reflectData.direction);

        OnShootRaycast<DoorOpenTrigger>(reflectData.hitPos, reflectData.direction);

        //CrystalCharging cC = OnShootRaycast<CrystalCharging>(reflectData.hitPos, reflectData.direction);
        //cC?.OnCharging();
    }
    private void OnDestroy()
    {
        _inputReader.OnStartFireEvent -= OnStartShootLight;
        _inputReader.OnStopFireEvent -= OnStopShootLight;
        _inputReader.OnShootingFireEvent -= OnShootingLight;
    }


}