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
    #region ������
    [SerializeField] protected Color defaultColor;
    public float lgihtFadeInoutDuration = 0.3f; // ����ĳ��Ʈ�� ������ �ð� (��)

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
            if (_currentType == value) return; //�����Ŵ� �ѹ���
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

    public abstract void SetDataModify(ReflectData inData); //�°��ִ� ���̸� ����� 

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
    private IEnumerator DrawAndFadeLineCoroutine() //������ ���� ������� �ڵ�
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
    protected void SetLightColor(Color type) //���⼭ ���� ������ �����������
    {

        // Property Block ������Ʈ
        _materialPropertyBlock.SetColor("_EmissionColor", type * 6f);

        // ���� �������� Property Block ����
        lb.SetPropertyBlock(_materialPropertyBlock);

    }

    protected Vector3 SetDirection(Vector3 value) //�� ������ �����ְ�
    {
        myReflectData.direction = value;
        return value;
    }
    private void ChangedReflectObject(Reflective reflectable) //���� �ݻ��� ���� ���� ������Ʈ�� �ٲ���
    {
        if (reflectObject == reflectable) return;
        reflectObject = (reflectable);
    }


    protected virtual void OnShootRaycast(ReflectData inData, Vector3 dir) //���� ���� ������Ʈ�� �����Ϳ� �� ����
    {
        lb.SetPosition(0, inData.hitPos);
        RaycastHit hit;

        if (elapsedTime < lgihtFadeInoutDuration) //������ �߻� ���϶� ������ ������
        {
            float t = elapsedTime / lgihtFadeInoutDuration;
            raycastDistance = t * 10;

            elapsedTime += Time.deltaTime;

            Vector3 endPosition = inData.hitPos + dir * raycastDistance;
            lb.SetPosition(1, endPosition);

            if (Physics.Raycast(inData.hitPos, dir, out hit, raycastDistance, ReflectionLayer)) //������ ������ �ִµ� ������Ʈ�� �¾�����
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
            else //���� �ʾ�����
            {
                _endPos = endPosition;
            }
        }
        else //������ �߻簡 ��������
        {
            if (Physics.Raycast(inData.hitPos, dir, out hit, 100, ReflectionLayer)) //������ �����°� �����µ� ������Ʈ�� �¾�����
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
            else ////���� �ʾ�����
            {
                if (reflectObject != null)
                {
                    reflectObject?.OnReflectTypeChanged(ReflectState.UnReflect);
                    reflectObject = null;
                }
                lb.SetPosition(1, dir * 100);

                Debug.Log($"{gameObject.name} : ���� �ʾѴ�");
                _endPos = inData.hitPos + dir * 100;
            }
        }

    }
    public void HandleGlowReflectStateChanged(GlowState glowState)
    {

    }
}


