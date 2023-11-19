using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static Define.Define;

[RequireComponent(typeof(LineRenderer))]
public abstract class Glow : LightingBehaviour, IGlowable
{
    public abstract void OnPickUp();
    public abstract void OnPutDown();
    public virtual void OnStartShootLight()
    {
        SetLightColor(defaultColor);

        var afterEffects = GameObject.FindObjectsOfType<LightAfterEffect>();

        for (int i = 0; i < afterEffects.Length; ++i)
        {
            Destroy(afterEffects[i].gameObject);
        }

        StopDrawAndFadeLine();
    }
    public virtual void OnStopShootLight()
    {
        StartDrawAndFadeLine(true);
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