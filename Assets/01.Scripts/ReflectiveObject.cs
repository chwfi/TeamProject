using System;
using System.Xml.Linq;
using UnityEngine;
using static Define.Define;



public class ReflectiveObject : MonoBehaviour, IReflectable
{
    [SerializeField] private Color defaultColor;

    private Collider _col;
    private LineRenderer _lr;

    public ReflectDataChanged OnReflectTypeChanged = null;

    private ReflectData myData;

    private ReflectState _currentType = ReflectState.UnReflect;

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
                UnHandleReflected();

            else if (_currentType == ReflectState.OnReflect)
                OnHandleReflected();

        }
    }

    private IReflectable reflectObject = null;
    protected virtual void Awake()
    {
        myData.inColor = defaultColor;

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

    public void DataModify(ReflectData data)
    {
        _lr.SetPosition(0, data.inHitPos);

        var cCol = ColorSystem.GetColorCombination(data.inColor, defaultColor);
        _lr.material.color = cCol;

        var raycastDirection = Vector3.Reflect(data.inDirection, data.normal);
        myData.inDirection = raycastDirection;

        RaycastHit hit;
        if (Physics.Raycast(data.inHitPos, raycastDirection, out hit, 1000, ReflectionLayer))
        {
            if (_col.name == hit.collider.name) return;

            myData.inHitPos = hit.point;
            myData.normal = hit.normal;

            _lr.SetPosition(1, hit.point);

            if (hit.collider.TryGetComponent<IReflectable>(out var reflectable))
            {
                ChangedReflectObject(reflectable);

                reflectObject.OnReflectTypeChanged(ReflectState.OnReflect);
                reflectObject.DataModify(myData);
            }
        }
        else
        {
            if (reflectObject != null)
            {
                reflectObject.OnReflectTypeChanged(ReflectState.UnReflect);
            }

            _lr.SetPosition(1, data.inHitPos + raycastDirection * 1000);
        }
    }
    public void OnHandleReflected()
    {
        Init();
    }
    public void UnHandleReflected()
    {
        _lr.positionCount = 0;
        _lr.startWidth = 0;
        _lr.endWidth = 0;
    }
    void IReflectable.OnReflectTypeChanged(ReflectState type)
    {
        CurrentType = type;
    }
    private void ChangedReflectObject(IReflectable reflectable)
    {
        if (reflectObject == reflectable) return;
        reflectObject = reflectable;
    }


}

