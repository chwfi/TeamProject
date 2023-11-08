using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define.Define;

[RequireComponent(typeof(LineRenderer))]
public abstract class LightingBehaviour : MonoBehaviour
{
    [Header("���� ����")]
    [SerializeField] protected Color defaultColor; //�⺻ �� ��
    [SerializeField] protected float lightFadeInoutDuration = .3f; //���� ������ų� ����� ������ �ð�

    private float lightWidth = .08f; //�� ũ��


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

    private ReflectState _currentState = ReflectState.NULL; //���� �� �ݻ� ����

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

        // ���� �������� Property Block ����
        lb.SetPropertyBlock(_materialPropertyBlock);
    }

    protected IEnumerator DrawAndFadeLineCoroutine() //������ ���� ������� �ڵ�
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

        if (elapsedTime < lightFadeInoutDuration) //������ �߻� ���϶�
        {
            float t = elapsedTime / lightFadeInoutDuration;
            raycastDistance = t * 10;

            Vector3 endPosition = inData.hitPos + dir * raycastDistance;
            lb.SetPosition(1, endPosition);

            if (Physics.Raycast(inData.hitPos, dir, out hit, raycastDistance, ReflectionLayer))
            //������ �߻� ���϶� ������Ʈ�� �¾�����
            {
                var reflectable = CheckObject<Reflective>(hit, inData);

                ChangedReflectObject(reflectable);
                
                reflectObject?.OnReflectTypeChanged(ReflectState.OnReflect);
                reflectObject?.SetDataModify(myReflectData);
            }
            else ////������ �߻� ���϶� ������Ʈ�� �ȸ¾�����
            {
                ReflectObjectChangedTypeToUnReflect();
            }
        }
        else //������ �߻簡 ��������
        {
            if (Physics.Raycast(inData.hitPos, dir, out hit, 1000, ReflectionLayer))
            {
                var reflectable = CheckObject<Reflective>(hit, inData); //��

                ChangedReflectObject(reflectable);

                reflectObject?.OnReflectTypeChanged(ReflectState.OnReflect);

                reflectObject?.SetDataModify(myReflectData);

            }
            else //������ �߻簡 ��������
            {
                ReflectObjectChangedTypeToUnReflect();

                lb.SetPosition(1, hit.point + dir * 1000); //�������̶� ��� ��ġ�� ���������
                _endPos = hit.point + dir * 1000;
            }
        }
    }

    private void ChangedReflectObject(Reflective reflectable) //���� �ݻ��� ���� ���� ������Ʈ�� �ٲ���
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
