using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern1 : Glow
{
    [SerializeField] private InputReader _inputReader;
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
        Reflective obj = OnShootRaycast<Reflective>(reflectData, reflectData.direction);

        ChangedReflectObject(obj);

        obj?.OnReflectTypeChanged(ReflectState.OnReflect);
        obj?.GetReflectedObjectDataModify(myReflectData);

        DoorOpenTrigger door = OnShootRaycast<DoorOpenTrigger>(reflectData, transform.forward);

        door?.ColorMatch(myReflectData.color);
    }
    private void OnDestroy()
    {
        _inputReader.OnStartFireEvent -= OnStartShootLight;
        _inputReader.OnStopFireEvent -= OnStopShootLight;
        _inputReader.OnShootingFireEvent -= OnShootingLight;
    }
}
