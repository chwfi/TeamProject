using Define;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;
using UnityEngine;
using static Define.Define;

public abstract class Reflective : MonoBehaviour, IReflectable
{
    #region 변수들
    [SerializeField] protected Color defaultColor;
    public float lgihtFadeInoutDuration = 0.3f; // 레이캐스트를 진행할 시간 (초)

    private float elapsedTime = 0f;

    private float raycastDistance = 0;

    private float startTime = 0;


    protected Collider _col;
    protected LineRenderer lb;

    protected ReflectData myReflectData;

    public ReflectState _currentType = ReflectState.NULL;
    public ReflectState CurrentType
    {
        get
        {
            return _currentType;
        }
        set
        {
            if (_currentType == value) return; //전에거는 한번만
            _currentType = value;

            if (_currentType == ReflectState.UnReflect)
            {
                reflectObject?.OnReflectTypeChanged(ReflectState.UnReflect);

                UnHandleReflected();
            }

            else if (_currentType == ReflectState.OnReflect)
            {
                OnHandleReflected();
            }

        }
    }

    public Vector3 _startPos = Vector3.zero;
    public Vector3 _endPos = Vector3.zero;

    private Reflective reflectObject = null;
    #endregion
    protected virtual void Awake()
    {
        myReflectData.color = defaultColor;

        _col = GetComponent<Collider>();

        lb = gameObject.GetComponent<LineRenderer>();

        if (lb == null)
            lb = gameObject.AddComponent<LineRenderer>();

        Init();
    }

    private void Init()
    {
        lb.positionCount = 2;
        lb.startWidth = .02f;
        lb.endWidth = .02f;
    }

    public abstract void SetDataModify(ReflectData inData); //맞고있는 중이면 실행됨 

    private Coroutine coroutine;
    public virtual void OnHandleReflected() //처음 빛을 맞을때 한번만 실행됨
    {
        lb.enabled = true;

        StopCoroutine(coroutine);

        raycastDistance = 0f;
        elapsedTime = 0f;
    }

    public virtual void UnHandleReflected() //맞지 않을때 한번만 실행됨
    {
        startTime = 0;

        coroutine = StartCoroutine(DrawAndFadeLineCoroutine());
    }
    private IEnumerator DrawAndFadeLineCoroutine()
    {
        while (startTime < lgihtFadeInoutDuration)
        {
            startTime += Time.deltaTime;
            Vector3 lerpedPosition = Vector3.Lerp(_startPos, _endPos, startTime / lgihtFadeInoutDuration);

            lb.SetPosition(0, lerpedPosition);
            lb.SetPosition(1, _endPos);

            yield return null;
        }

        lb.enabled = false;
        startTime = 0;
    }

    public void OnReflectTypeChanged(ReflectState type)
    {
        CurrentType = type;
    }
    protected void SetLightColor(Color type)
    {
        lb.material.color = type;
    }
    protected Vector3 SetDirection(Vector3 value)
    {
        myReflectData.direction = value;
        return value;
    }
    private void ChangedReflectObject(Reflective reflectable)
    {
        if (reflectObject == reflectable) return;
        reflectObject = (reflectable);
    }


    protected virtual void OnShootRaycast(ReflectData inData, Vector3 dir) //나를 맞춘 오브젝트의 데이터와 쏠 방향
    {
        lb.SetPosition(0, inData.hitPos);
        RaycastHit hit;



        if (elapsedTime < lgihtFadeInoutDuration) //레이저 발사 중일때
        {
            float t = elapsedTime / lgihtFadeInoutDuration;
            raycastDistance = t * 10;

            elapsedTime += Time.deltaTime;

            Vector3 endPosition = inData.hitPos + dir * raycastDistance;
            lb.SetPosition(1, endPosition);

            if (Physics.Raycast(inData.hitPos, dir, out hit, raycastDistance, ReflectionLayer))
            {
                if (hit.collider.TryGetComponent<Reflective>(out var reflectable))
                {
                    myReflectData.hitPos = hit.point;
                    myReflectData.direction = dir;
                    myReflectData.normal = hit.normal;

                    lb.SetPosition(1, hit.point);


                    ChangedReflectObject(reflectable);

                    reflectable?.OnReflectTypeChanged(ReflectState.OnReflect);
                    reflectable?.SetDataModify(myReflectData);
                }
            }
            else //레이저 발사가 끝났을때
            {
                if (reflectObject != null)
                {
                    reflectObject?.OnReflectTypeChanged(ReflectState.UnReflect);
                    reflectObject = null;
                }
            }
        }
        else //레이저 발사가 끝났을때
        {
            if (Physics.Raycast(inData.hitPos, dir, out hit, 100, ReflectionLayer))
            {
                if (hit.collider.TryGetComponent<Reflective>(out var reflectable))
                {
                    myReflectData.hitPos = hit.point;
                    myReflectData.direction = dir;
                    myReflectData.normal = hit.normal;

                    _endPos = myReflectData.hitPos;

                    lb.SetPosition(1, hit.point);

                    ChangedReflectObject(reflectable);

                    reflectable?.OnReflectTypeChanged(ReflectState.OnReflect);
                    reflectable?.SetDataModify(myReflectData);
                }
            }
            else //레이저 발사가 끝났을때
            {
                if (reflectObject != null)
                {
                    reflectObject?.OnReflectTypeChanged(ReflectState.UnReflect);
                    reflectObject = null;
                }
                lb.SetPosition(1, dir * 100);

                _endPos = inData.hitPos + dir * 100;
            }
        }

    }
    public void HandleGlowReflectStateChanged(GlowState glowState)
    {

    }
}


