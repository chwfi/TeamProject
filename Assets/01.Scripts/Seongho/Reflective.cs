using Define;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;
using UnityEngine;

public abstract class Reflective : LightingBehaviour, IReflectable
{

    private ReflectState _currentState = ReflectState.NULL; //���� �� �ݻ� ����

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
    public abstract void GetReflectedObjectDataModify(ReflectData reflectedData); //�°��ִ� ���̸� �����

    public virtual void OnHandleReflected() //ó�� ���� ������ �ѹ��� �����
    {
        StopDrawAndFadeLine();

    }

    public virtual void UnHandleReflected() //���� ������ �ѹ��� �����
    {
        StartDrawAndFadeLine();
    }
    public void OnReflectTypeChanged(ReflectState state)
    {
        CurrentState = state;
    }
}


