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
    private MaterialPropertyBlock _materialPropertyBlock;
    #endregion
    private Coroutine coroutine;
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
        _materialPropertyBlock = new MaterialPropertyBlock();
    }

    public abstract void SetDataModify(ReflectData inData); //맞고있는 중이면 실행됨 

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
    private IEnumerator DrawAndFadeLineCoroutine() //서서히 빛이 사라지는 코드
    {
        lb.SetPosition(0, _startPos);
        lb.SetPosition(1, _endPos);

        while (startTime < lgihtFadeInoutDuration)
        {
            startTime += Time.deltaTime;
            Vector3 lerpedPosition = Vector3.Lerp(_startPos, _endPos, startTime / lgihtFadeInoutDuration);

            lb.SetPosition(0, lerpedPosition);

            yield return null;
        }

        lb.enabled = false;
        startTime = 0;

        if (reflectObject != null)
        {
            reflectObject?.OnReflectTypeChanged(ReflectState.UnReflect);
            reflectObject = null;
        }
    }

    public void OnReflectTypeChanged(ReflectState type)
    {
        CurrentType = type;
    }
    protected void SetLightColor(Color type) //여기서 빛의 색깔을 세팅해줘야함
    {

        // Property Block 업데이트
        _materialPropertyBlock.SetColor("_EmissionColor", type * 6f);

        // 라인 렌더러에 Property Block 적용
        lb.SetPropertyBlock(_materialPropertyBlock);

    }

    protected Vector3 SetDirection(Vector3 value) //쏠 방향을 정해주고
    {
        myReflectData.direction = value;
        return value;
    }
    private void ChangedReflectObject(Reflective reflectable) //내가 반사한 빛에 닿은 오브젝트를 바꿔줌
    {
        if (reflectObject == reflectable) return;
        reflectObject = (reflectable);
    }


    protected virtual void OnShootRaycast(ReflectData inData, Vector3 dir) //나를 맞춘 오브젝트의 데이터와 쏠 방향
    {
        lb.SetPosition(0, inData.hitPos);
        RaycastHit hit;

        if (elapsedTime < lgihtFadeInoutDuration) //레이저 발사 중일때 서서히 쏴지게
        {
            float t = elapsedTime / lgihtFadeInoutDuration;
            raycastDistance = t * 10;

            elapsedTime += Time.deltaTime;

            Vector3 endPosition = inData.hitPos + dir * raycastDistance;
            lb.SetPosition(1, endPosition);

            if (Physics.Raycast(inData.hitPos, dir, out hit, raycastDistance, ReflectionLayer)) //서서히 쏴지고 있는데 오브젝트가 맞았을때
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
            else //맞지 않았을때
            {
                _endPos = endPosition;
            }
        }
        else //레이저 발사가 끝났을때
        {
            if (Physics.Raycast(inData.hitPos, dir, out hit, 100, ReflectionLayer)) //서서히 쏴지는게 끝났는데 오브젝트가 맞았을때
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
            else ////맞지 않았을때
            {
                if (reflectObject != null)
                {
                    reflectObject?.OnReflectTypeChanged(ReflectState.UnReflect);
                    reflectObject = null;
                }
                lb.SetPosition(1, dir * 100);

                Debug.Log($"{gameObject.name} : 맞지 않앗다");
                _endPos = inData.hitPos + dir * 100;
            }
        }

    }
    public void HandleGlowReflectStateChanged(GlowState glowState)
    {

    }
}


