using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define.Define;

[RequireComponent(typeof(LineRenderer))]
public abstract class LightingBehaviour : MonoBehaviour
{
    [Header("참조 변수")]
    [SerializeField] protected Color defaultColor; //기본 빛 색
    [SerializeField] protected float lightFadeInoutDuration = .3f; //빛이 사라지거나 생기는 딜레이 시간

    private float lightWidth = .08f; //빛 크기


    [Header("클래스 내 변수")]
    protected LineRenderer lb; //라인 렌더러

    #region 딜레이 줄 때 필요한 변수

    protected Vector3 _startPos = Vector3.zero; //처음 지점
    protected Vector3 _endPos = Vector3.zero; //끝 지점

    protected Coroutine coroutine; //나의 코루틴 저장

    protected float startTime = 0f;

    protected float elapsedTime = 0f;

    protected float raycastDistance = 0f;
    #endregion

    #region 반사로직 변수(경고 !!절대로 열지 마세요!! )
    protected Reflective reflectObject = null; //내 빛에 반사된 오브젝트

    protected ReflectData myReflectData; //나의 반사 데이터

    private ReflectState _currentState = ReflectState.NULL; //현재 내 반사 상태

    public ReflectState CurrentState
    {
        get { return _currentState; }
        set
        {
            if (_currentState == value) return;

            _currentState = value;
        }
    }

    #endregion

    private MaterialPropertyBlock _materialPropertyBlock;
    protected virtual void Awake()
    {
        lb = GetComponent<LineRenderer>();
        //lb.material.color = defaultColor;

    }
    protected virtual void Start()
    {
        lb.positionCount = 2;

        myReflectData.color = defaultColor;

        lb.startWidth = lightWidth;
        lb.endWidth = lightWidth;

        _materialPropertyBlock = new MaterialPropertyBlock();
    }
    protected void SetLightColor(Color color)
    {
        _materialPropertyBlock.SetColor("_EmissionColor", color * 6f);

        // 라인 렌더러에 Property Block 적용
        lb.SetPropertyBlock(_materialPropertyBlock);
    }

    protected IEnumerator DrawAndFadeLineCoroutine() //서서히 빛이 사라지는 코드
    {
        lb.SetPosition(0, _startPos);
        lb.SetPosition(1, _endPos);

        while (startTime < lightFadeInoutDuration)
        {
            startTime += Time.deltaTime;
            Vector3 lerpedPosition = Vector3.Lerp(_startPos, _endPos, startTime / lightFadeInoutDuration);

            lb.SetPosition(0, lerpedPosition);

            yield return null;
        }

        lb.enabled = false;

        ReflectObjectChangedTypeToUnReflect();
    }

    public abstract void SetDataModify(ReflectData reflectData);
    protected void OnShootRaycast(ReflectData inData, Vector3 dir)
    {
        lb.SetPosition(0, inData.hitPos);

        RaycastHit hit;

        elapsedTime += Time.deltaTime;

        if (elapsedTime < lightFadeInoutDuration) //레이저 발사 중일때
        {
            float t = elapsedTime / lightFadeInoutDuration;
            raycastDistance = t * 10;

            Vector3 endPosition = inData.hitPos + dir * raycastDistance;
            lb.SetPosition(1, endPosition);

            if (Physics.Raycast(inData.hitPos, dir, out hit, raycastDistance, ReflectionLayer))
            //레이저 발사 중일때 오브젝트가 맞았을때
            {
                var reflectable = CheckObject<Reflective>(hit, inData);

                ChangedReflectObject(reflectable);
                
                reflectObject?.OnReflectTypeChanged(ReflectState.OnReflect);
                reflectObject?.SetDataModify(myReflectData);
            }
            else ////레이저 발사 중일때 오브젝트가 안맞았을때
            {
                ReflectObjectChangedTypeToUnReflect();
            }
        }
        else //레이저 발사가 끝났을때
        {
            if (Physics.Raycast(inData.hitPos, dir, out hit, 1000, ReflectionLayer))
            {
                var reflectable = CheckObject<Reflective>(hit, inData); //똥

                ChangedReflectObject(reflectable);

                reflectObject?.OnReflectTypeChanged(ReflectState.OnReflect);

                reflectObject?.SetDataModify(myReflectData);

            }
            else //레이저 발사가 끝났을때
            {
                ReflectObjectChangedTypeToUnReflect();

                lb.SetPosition(1, hit.point + dir * 1000); //포지션이라 쏘는 위치를 더해줘야함
                _endPos = hit.point + dir * 1000;
            }
        }
    }

    private void ChangedReflectObject(Reflective reflectable) //내가 반사한 빛에 닿은 오브젝트를 바꿔줌
    {
        if (reflectObject == reflectable) return;

        reflectObject?.ChangedReflectObject(reflectable);

        reflectObject = (reflectable);
    }

    private T CheckObject<T>(RaycastHit hit, ReflectData reflectData) where T : LightingBehaviour
    {
        if (hit.collider.TryGetComponent<T>(out var reflectable))
        {
            myReflectData.hitPos = hit.point;
            myReflectData.direction = reflectData.direction;
            myReflectData.normal = hit.normal;

            lb.SetPosition(1, hit.point);
            _endPos = hit.point;

            return reflectable;
        }
        return null;
    }

    private void ReflectObjectChangedTypeToUnReflect()
    {
        if (reflectObject != null)
        {
            reflectObject?.OnReflectTypeChanged(ReflectState.UnReflect);
            reflectObject = null;
        }
    }
}
