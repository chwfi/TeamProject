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
        base.SetReflectDataModify(reflectData);
    }
    private void OnDestroy()
    {
        _inputReader.OnStartFireEvent -= OnStartShootLight;
        _inputReader.OnStopFireEvent -= OnStopShootLight;
        _inputReader.OnShootingFireEvent -= OnShootingLight;
    }
}
