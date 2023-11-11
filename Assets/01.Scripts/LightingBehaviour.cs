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
    [Header("���� ����")]
    [SerializeField] protected Color defaultColor; //�⺻ �� ��
    [SerializeField] protected float lightFadeInoutDuration = .3f; //���� ������ų� ����� ������ �ð�

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

        // ���� �������� Property Block ����
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

    private IEnumerator DrawAndFadeLineCoroutine() //������ ���� ������� �ڵ�
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
    protected T OnShootRaycast<T>(ReflectData inData, Vector3 dir) where T : class //���� ���� ������Ʈ�� �����Ϳ� �� ����
    {
        lb.SetPosition(0, inData.hitPos);
        RaycastHit hit;

        T reflectedObject = null;
        if (elapsedTime < lightFadeInoutDuration) //������ �߻� ���϶� ������ ������
        {
            float t = elapsedTime / lightFadeInoutDuration;
            raycastDistance = t * 10;

            elapsedTime += Time.deltaTime;

            Vector3 endPosition = inData.hitPos + dir * raycastDistance;
            lb.SetPosition(1, endPosition);

            if (Physics.Raycast(inData.hitPos, dir, out hit, raycastDistance, ReflectionLayer)) //������ ������ �ִµ� ������Ʈ�� �¾�����
            {
                var obj = CheckObject<T>(hit, dir);

                reflectedObject = obj;

            }
            else //���� �ʾ�����
            {
                SetDrawLineEndPos(endPosition);
            }
        }
        else //������ �߻簡 ��������
        {
            if (Physics.Raycast(inData.hitPos, dir, out hit, maxDistance, ReflectionLayer)) //������ �����°� �����µ� ������Ʈ�� �¾�����
            {
                var obj = CheckObject<T>(hit, dir);

                reflectedObject = obj;
            }
            else ////���� �ʾ�����
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
    protected void ChangedReflectObject(Reflective reflectable) //���� �ݻ��� ���� ���� ������Ʈ�� �ٲ���
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
