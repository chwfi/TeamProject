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
    public abstract override void SetDataModify(ReflectData inData); //�°��ִ� ���̸� ����� 

    public virtual void OnHandleReflected() //ó�� ���� ������ �ѹ��� �����
    {
        lb.enabled = true;

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        raycastDistance = 0f;
        elapsedTime = 0f;
    }

    public virtual void UnHandleReflected() //���� ������ �ѹ��� �����
    {
        startTime = 0;

        coroutine = StartCoroutine(DrawAndFadeLineCoroutine());
    }

    public void OnReflectTypeChanged(ReflectState state)
    {
        CurrentState = state;
    }

    protected Vector3 SetDirection(Vector3 value) //�� ������ �����ְ�
    {
        myReflectData.direction = value;
        return value;
    }
}


