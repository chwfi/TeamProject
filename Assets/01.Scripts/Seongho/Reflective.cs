using Define;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;
using UnityEngine;

public abstract class Reflective : LightingBehaviour, IReflectable
{

    private ReflectState _currentState = ReflectState.NULL;

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

    public abstract void OnDeflected(ReflectData reflectedData);

    public virtual void OnHandleReflected()
    {
        StartDrawAndFadeLine();

        var afterEffects = GameObject.FindObjectsOfType<LightAfterEffect>();

        for (int i = 0; i < afterEffects.Length; ++i)
        {
            Destroy(afterEffects[i].gameObject);
        }
    }

    public virtual void UnHandleReflected() //���� ������ �ѹ��� �����
    {
        StopDrawAndFadeLine();
    }
    public void OnReflectTypeChanged(ReflectState state)
    {
        CurrentState = state;
    }
}


