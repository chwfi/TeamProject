using Define;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;
using UnityEngine;

public abstract class Reflective : LightingBehaviour, IReflectable
{

    private ReflectState _currentState = ReflectState.NULL; //현재 내 반사 상태

    public ReflectState CurrentState
    {
        get { return _currentState; }
        set
        {
            if (_currentState == value) return;

            _currentState = value;

            if (_currentState == ReflectState.UnReflect)
            {
                UnHandleReflected();
            }

            else if (_currentState == ReflectState.OnReflect)
            {
                OnHandleReflected();
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
    }
    public abstract void GetReflectedObjectDataModify(ReflectData reflectedData); //맞고있는 중이면 실행됨

    public virtual void OnHandleReflected() //처음 빛을 맞을때 한번만 실행됨
    {
        StopDrawAndFadeLine();
    }

    public virtual void UnHandleReflected() //맞지 않을때 한번만 실행됨
    {
        StartDrawAndFadeLine();
    }
    public void OnReflectTypeChanged(ReflectState state)
    {
        CurrentState = state;
    }
}


