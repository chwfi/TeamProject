using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IGlowable
{
    public abstract void OnStartShootLight();
    public abstract void OnStopShootLight();
    public abstract void OnShootingLight();
    public abstract void StartShootLight(Vector3 origin, Vector3 direction);
    public virtual void SetReflectDataModify(ReflectData reflectData) { }
}
