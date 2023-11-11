using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.VFX;
using static Define.Define;

[RequireComponent(typeof(LineRenderer))]
public abstract class LightingBehaviour : MonoBehaviour
{
    [Header("참조 변수")]
    [SerializeField] protected Color defaultColor; //기본 빛 색
    [SerializeField] protected float lightFadeInoutDuration = .3f; //빛이 사라지거나 생기는 딜레이 시간

    [SerializeField] private float lightWidth = .08f; //빛 크기


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

    #endregion

    private MaterialPropertyBlock _materialPropertyBlock;
    private int maxDistance = 1000;
    protected virtual void Awake()
    {
        lb = GetComponent<LineRenderer>();
    }
    protected virtual void Start()
    {
        lb.positionCount = 2;

        myReflectData.color = defaultColor;

        lb.startWidth = lightWidth;
        lb.endWidth = lightWidth;

        lb.enabled = false;

        _materialPropertyBlock = new MaterialPropertyBlock();
    }
    protected void SetLightColor(Color color)
    {
        _materialPropertyBlock.SetColor("_EmissionColor", color * 6f);

        // 라인 렌더러에 Property Block 적용
        lb.SetPropertyBlock(_materialPropertyBlock);
    }

    protected void StopDrawAndFadeLine()
    {
        lb.enabled = true;

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    protected void StartDrawAndFadeLine()
    {
        elapsedTime = 0;
        raycastDistance = 0;
        startTime = 0;

        coroutine = StartCoroutine(DrawAndFadeLineCoroutine());
    }

    private IEnumerator DrawAndFadeLineCoroutine() //서서히 빛이 사라지는 코드
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
    protected T OnShootRaycast<T>(ReflectData inData, Vector3 dir) where T : class //나를 맞춘 오브젝트의 데이터와 쏠 방향
    {
        lb.SetPosition(0, inData.hitPos);
        RaycastHit hit;

        T reflectedObject = null;
        if (elapsedTime < lightFadeInoutDuration) //레이저 발사 중일때 서서히 쏴지게
        {
            float t = elapsedTime / lightFadeInoutDuration;
            raycastDistance = t * 10;

            elapsedTime += Time.deltaTime;

            Vector3 endPosition = inData.hitPos + dir * raycastDistance;
            lb.SetPosition(1, endPosition);

            if (Physics.Raycast(inData.hitPos, dir, out hit, raycastDistance, ReflectionLayer)) //서서히 쏴지고 있는데 오브젝트가 맞았을때
            {
                var obj = CheckObject<T>(hit, dir);

                reflectedObject = obj;

            }
            else //맞지 않았을때
            {
                SetDrawLineEndPos(endPosition);
            }
        }
        else //레이저 발사가 끝났을때
        {
            if (Physics.Raycast(inData.hitPos, dir, out hit, maxDistance, ReflectionLayer)) //서서히 쏴지는게 끝났는데 오브젝트가 맞았을때
            {
                var obj = CheckObject<T>(hit, dir);

                reflectedObject = obj;
            }
            else ////맞지 않았을때
            {
                ReflectObjectChangedTypeToUnReflect();

                lb.SetPosition(1, inData.hitPos + dir * maxDistance);

                _endPos = inData.hitPos + dir.normalized * maxDistance;
            }
        }

        return reflectedObject;

    }
    protected void SetDrawLineEndPos(Vector3 endPos)
    {
        _endPos = endPos;
    }
    private T CheckObject<T>(RaycastHit hit, Vector3 reflectDirection) where T : class
    {
        if (hit.collider.TryGetComponent<T>(out var reflectable))
        {
            myReflectData.hitPos = hit.point;
            myReflectData.direction = reflectDirection;
            myReflectData.normal = hit.normal;

            lb.SetPosition(1, hit.point);

            SetDrawLineEndPos(hit.point);
            return reflectable;
        }
        return null;
    }
    protected void ChangedReflectObject(Reflective reflectable) //내가 반사한 빛에 닿은 오브젝트를 바꿔줌
    {
        if (reflectObject == reflectable) return;
        reflectObject = reflectable;
    }

    protected void ReflectObjectChangedTypeToUnReflect()
    {
        if (reflectObject == null) return;

        reflectObject?.OnReflectTypeChanged(ReflectState.UnReflect);
        reflectObject = null;
    }
}
