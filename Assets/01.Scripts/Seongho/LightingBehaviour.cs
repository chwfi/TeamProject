using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.VFX;
using static Define.Define;

[RequireComponent(typeof(LineRenderer))]
public abstract class LightingBehaviour : MonoBehaviour
{
    #region 파일 위치
    private readonly string AFTEREFFCT_PATH = "LightAfterEffect";
    #endregion

    [Header("참조 변수")]
    [SerializeField] protected Color defaultColor; //기본 빛 색
    [SerializeField] private float lightFadeInoutTick = 10f; //한 틱당 이동 거리
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

    private Color _effectColor;
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
    protected void SetStartPos(Vector3 pos)
    {
        _startPos = pos;
    }
    protected void SetLightColor(Color color)
    {
        _effectColor = color;

        _materialPropertyBlock.SetColor("_EmissionColor", color * 6f);

        // 라인 렌더러에 Property Block 적용
        lb.SetPropertyBlock(_materialPropertyBlock);
    }

    protected void StopDrawAndFadeLine()
    {
        lb.enabled = true;

        ReflectObjectChangedTypeToUnReflect();
    }

    protected void StartDrawAndFadeLine()
    {
        lb.enabled = false;

        elapsedTime = 0;
        raycastDistance = 0;

        var effect = RdfResources.Load<LightAfterEffect>(AFTEREFFCT_PATH);

        var obj = GameObject.Instantiate(effect);

        obj.Setting(lightWidth, _effectColor);

        obj.DrawAndFadeLine(_startPos, _endPos, lightFadeInoutTick,
          () =>
          {
              ReflectObjectChangedTypeToUnReflect();
          });

        //작업이 끝나고 수행되야 할 코드
    }
    protected T OnShootRaycast<T>(Vector3 pos, Vector3 dir) where T : class
    {
        lb.SetPosition(0, pos);

        RaycastHit hit;

        T reflectedObject = null;

        Vector3 endPosition = pos + dir * raycastDistance;

        raycastDistance += lightFadeInoutTick * Time.deltaTime;

        if (Physics.Raycast(pos, dir, out hit, raycastDistance, ReflectionLayer))
        {
            reflectedObject = CheckObject<T>(hit, dir);
        }
        else
        {
            lb.SetPosition(1, endPosition);
            SetDrawLineEndPos(endPosition);

            ReflectObjectChangedTypeToUnReflect();
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
            SetDrawLineEndPos(hit.point);

            lb.SetPosition(1, hit.point);

            myReflectData.hitPos = hit.point;
            myReflectData.direction = reflectDirection;
            myReflectData.normal = hit.normal;

            return reflectable;
        }
        return null;
    }
    protected void ChangedReflectObject(Reflective reflectable) //내가 반사한 빛에 닿은 오브젝트를 바꿔줌
    {
        if (reflectObject == reflectable) return;
        ReflectObjectChangedTypeToUnReflect();
        reflectObject = reflectable;
    }

    protected void ReflectObjectChangedTypeToUnReflect()
    {
        if (reflectObject == null) return;

        reflectObject?.OnReflectTypeChanged(ReflectState.UnReflect);
        reflectObject = null;
    }
}
