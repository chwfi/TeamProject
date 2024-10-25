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

    public override void ShootLightSetting()
    {
        Vector3 startPos = transform.position;
        Vector3 dir = transform.forward;

        OnShootRaycast<ReflectToReflect>(startPos, dir);
        OnShootRaycast<ReflectToUp>(startPos, dir);
        OnShootRaycast<DoorOpenTrigger>(startPos, dir);

    }
    private void OnDestroy()
    {
        _inputReader.OnStartFireEvent -= OnStartShootLight;
        _inputReader.OnStopFireEvent -= OnStopShootLight;
        _inputReader.OnShootingFireEvent -= OnShootingLight;
    }


}