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
    public virtual void SetReflectDataModify(ReflectData reflectData)
    {
        Reflective obj = OnShootRaycast<Reflective>(reflectData.hitPos, reflectData.direction);

        ChangedReflectObject(obj);

        obj?.OnReflectTypeChanged(ReflectState.OnReflect);
        obj?.GetReflectedObjectDataModify(myReflectData);

        var triPlane = OnShootRaycast<TriangluarPlane>(reflectData.hitPos, reflectData.direction);
        triPlane?.GetReflectedObjectDataModify(reflectData);


    }



}