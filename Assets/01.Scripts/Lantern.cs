using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : Glow
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private RemainedLight _remainedLight;
    protected override void Awake()
    {
        base.Awake();

        _inputReader.OnStartFireEvent += OnStartShootLight;
        _inputReader.OnStopFireEvent += OnStopShootLight;
        _inputReader.OnShootingFireEvent += OnShootingLight;

    }
    public override void OnStartShootLight()
    {  
        base.OnStartShootLight();
        _remainedLight.ClearTrail();
    }
    public override void OnStopShootLight()
    {
        base.OnStopShootLight();
    }
    public override void OnShootingLight()
    {
        base.OnShootingLight();

        StartShootLight(transform.position, transform.forward);
    }
    public override void SetReflectDataModify(ReflectData reflectData)
    {
        Reflective obj = OnShootRaycast<Reflective>(reflectData.hitPos, reflectData.direction);

        ChangedReflectObject(obj);

        obj?.OnReflectTypeChanged(ReflectState.OnReflect);
        obj?.GetReflectedObjectDataModify(myReflectData);
    }
    private void OnDestroy()
    {
        _inputReader.OnStartFireEvent -= OnStartShootLight;
        _inputReader.OnStopFireEvent -= OnStopShootLight;
        _inputReader.OnShootingFireEvent -= OnShootingLight;
    }
}
