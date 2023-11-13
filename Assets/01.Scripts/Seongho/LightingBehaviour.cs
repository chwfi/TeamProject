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
    #region ���� ��ġ
    private readonly string AFTEREFFCT_PATH = "LightAfterEffect";
    #endregion

    [Header("���� ����")]
    [SerializeField] protected Color defaultColor; //�⺻ �� ��
    [SerializeField] private float lightFadeInoutTick = 10f; //�� ƽ�� �̵� �Ÿ�
    [SerializeField] private float lightWidth = .08f; //�� ũ��


    [Header("Ŭ���� �� ����")]
    protected LineRenderer lb; //���� ������

    #region ������ �� �� �ʿ��� ����

    protected Vector3 _startPos = Vector3.zero; //ó�� ����
    protected Vector3 _endPos = Vector3.zero; //�� ����

    protected Coroutine coroutine; //���� �ڷ�ƾ ����

    protected float startTime = 0f;

    protected float elapsedTime = 0f;

    protected float raycastDistance = 0f;
    #endregion

    #region �ݻ���� ����(��� !!����� ���� ������!! )

    protected Reflective reflectObject = null; //�� ���� �ݻ�� ������Ʈ

    protected ReflectData myReflectData; //���� �ݻ� ������

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

        // ���� �������� Property Block ����
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

        //�۾��� ������ ����Ǿ� �� �ڵ�
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
    protected void ChangedReflectObject(Reflective reflectable) //���� �ݻ��� ���� ���� ������Ʈ�� �ٲ���
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
