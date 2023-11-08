using Define;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;
using UnityEngine;
using static Define.Define;

public abstract class Reflective : LightingBehaviour, IReflectable
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
    }
    public abstract override void SetDataModify(ReflectData inData); //맞고있는 중이면 실행됨 

    public virtual void OnHandleReflected() //처음 빛을 맞을때 한번만 실행됨
    {
        lb.enabled = true;

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        raycastDistance = 0f;
        elapsedTime = 0f;
    }

    public virtual void UnHandleReflected() //맞지 않을때 한번만 실행됨
    {
        startTime = 0;

        coroutine = StartCoroutine(DrawAndFadeLineCoroutine());
    }

    public void OnReflectTypeChanged(ReflectState state)
    {
        CurrentState = state;
    }

    protected Vector3 SetDirection(Vector3 value) //쏠 방향을 정해주고
    {
        myReflectData.direction = value;
        return value;
    }
}


