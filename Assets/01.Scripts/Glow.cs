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
    public virtual void SetReflectDataModify(ReflectData reflectData) //기본 베이스는 반사 오브젝트 안하고 싶으면 베이스 지우면 댐
    {
        var obj = OnShootRaycast<Reflective>(reflectData, reflectData.direction);

        ChangedReflectObject(obj);

        obj?.OnReflectTypeChanged(ReflectState.OnReflect);
        obj?.GetReflectedObjectDataModify(myReflectData);
    }

  
}