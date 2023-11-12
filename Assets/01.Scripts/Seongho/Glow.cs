using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static Define.Define;

[RequireComponent(typeof(LineRenderer))]
public abstract class Glow : LightingBehaviour, IGlowable
{

    public virtual void OnStartShootLight()
    {
        SetLightColor(defaultColor);
        ReflectObjectChangedTypeToUnReflect();

        StopDrawAndFadeLine();
    }
    public virtual void OnStopShootLight()
    {
        StartDrawAndFadeLine();
    }
    public virtual void OnShootingLight()
    {

    }
    public virtual void StartShootLight(Vector3 origin, Vector3 direction)
    {
        _startPos = origin;

        myReflectData.hitPos = origin;
        myReflectData.direction = direction;

        SetReflectDataModify(myReflectData);
    }
    public abstract void SetReflectDataModify(ReflectData reflectData);



}