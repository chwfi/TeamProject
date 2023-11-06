using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static Define.Define;

public abstract class Glow : MonoBehaviour
{
    [SerializeField] private Color defaultColor;

    private LineRenderer lb;

    private ReflectData myReflectData;

    private Reflective reflectObject = null;

    public GlowStateChangedHander OnGlowStateChanged = null;

    private GlowState _currentState = GlowState.NULL;

    public GlowState CurrentState
    {
        get { return _currentState; }
        set
        {
            if (_currentState == value) return;

            _currentState = value;

            OnGlowStateChanged?.Invoke(_currentState);
        }
    }
    protected virtual void Awake()
    {
        myReflectData.color = defaultColor;

        lb = GetComponent<LineRenderer>();

        if (lb == null)
            lb = gameObject.AddComponent<LineRenderer>();

        lb.positionCount = 2;
        lb.material.color = defaultColor;


    }
    protected virtual void OnStartShootLight()
    {
        lb.enabled = true;

        _currentState = GlowState.OnGlow;
    }
    protected virtual void OnEndShootLight()
    {
        lb.enabled = false;

        _currentState = GlowState.UnGlow;
    }
    protected virtual void OnShootLight()
    {
        Debug.Log("½´¿ô");
        StartShootLight(transform.position, transform.forward);
    }

    protected virtual void StartShootLight(Vector3 origin, Vector3 direction)
    {
        myReflectData.hitPos = origin;
        myReflectData.direction = direction;

        DataModify(myReflectData);
    }

    protected virtual void DataModify(ReflectData reflectData)
    {
        lb.SetPosition(0, reflectData.hitPos);
        RaycastHit hit;

        if (Physics.Raycast(reflectData.hitPos, reflectData.direction, out hit, 1000, ReflectionLayer))
        {
            myReflectData.hitPos = hit.point;
            myReflectData.direction = reflectData.direction;
            myReflectData.normal = hit.normal;

            lb.SetPosition(1, hit.point);

            if (hit.collider.TryGetComponent<Reflective>(out var reflectable)) //¹Ý»ç ¿ÀºêÁ§Æ®¶ó¸é
            {
                ChangedReflectObject(reflectable);

                reflectable?.OnReflectTypeChanged(ReflectState.OnReflect);
                reflectable?.SetDataModify(myReflectData);
            }
        }
        else
        {
            if (reflectObject != null)
            {
                reflectObject?.OnReflectTypeChanged(ReflectState.UnReflect);
                reflectObject = null;
            }

            lb.SetPosition(1, reflectData.direction * 1000);
        }
    }
    private void ChangedReflectObject(Reflective reflectable)
    {
        if (reflectObject == reflectable) return;

        OnGlowStateChanged -= reflectObject.HandleGlowReflectStateChanged;

        reflectObject = reflectable;

        OnGlowStateChanged += reflectObject.HandleGlowReflectStateChanged;
    }
}