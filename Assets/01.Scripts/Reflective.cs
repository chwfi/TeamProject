using Define;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using static Define.Define;

public abstract class Reflective : MonoBehaviour, IReflectable
{
    #region 변수들
    [SerializeField] protected Color defaultColor;

    protected Collider _col;
    protected LineRenderer _lr;

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

    private Reflective reflectObject = null;
    #endregion
    protected virtual void Awake()
    {
        myReflectData.color = defaultColor;

        _col = GetComponent<Collider>();

        _lr = gameObject.GetComponent<LineRenderer>();

        if (_lr == null)
            _lr = gameObject.AddComponent<LineRenderer>();

        Init();
    }

    private void Init()
    {
        _lr.positionCount = 2;
        _lr.startWidth = .2f;
        _lr.endWidth = .2f;
    }

    public abstract void SetDataModify(ReflectData inData); //맞고있는 중이면 실행됨 

    public virtual void OnHandleReflected() //처음 빛을 맞을때 한번만 실행됨
    {
        _lr.enabled = true;
    }
    public virtual void UnHandleReflected() //맞지 않을때 한번만 실행됨
    {
        _lr.enabled = false;
    }
    public void OnReflectTypeChanged(ReflectState type)
    {
        CurrentType = type;
    }
    protected void SetLightColor(Color type)
    {
        _lr.material.color = type;
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
        _lr.SetPosition(0, inData.hitPos);

        RaycastHit hit;

        if (Physics.Raycast(inData.hitPos, dir, out hit, 1000, ReflectionLayer))
        {
            if (gameObject == hit.collider.gameObject) return;
            //if (_col.name == hit.collider.name) return;

            myReflectData.hitPos = hit.point;
            myReflectData.normal = hit.normal;

            _lr.SetPosition(1, hit.point);

            if (hit.collider.TryGetComponent<Reflective>(out var reflectable))
            {
                ChangedReflectObject(reflectable);

                reflectObject.OnReflectTypeChanged(ReflectState.OnReflect);
                reflectObject.SetDataModify(myReflectData);

                Debug.DrawRay(inData.hitPos, dir * hit.distance, Color.red);
            }
        }
        else
        {
            if (reflectObject != null)
            {
                reflectObject.OnReflectTypeChanged(ReflectState.UnReflect);
                reflectObject = null;
            }

            Debug.DrawRay(inData.hitPos, inData.hitPos + dir * 1000, Color.green);
            _lr.SetPosition(1, inData.hitPos + dir * 1000);
        }
        Debug.Log(gameObject.name + " : " + reflectObject?.name);
    }
    public void HandleGlowReflectStateChanged(GlowState glowState)
    {

    }
}


