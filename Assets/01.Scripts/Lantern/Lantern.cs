using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Xml.Linq;
using UnityEngine;
using static Define.Define;

[RequireComponent(typeof(LineRenderer))]
public class Lantern : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    public Color defaultColor;

    private LineRenderer lb;

    private ReflectData myReflectData;

    private Reflective reflectObject = null;

    private ReflectState _currentState = ReflectState.NULL;

    public ReflectState CurrentState
    {
        get { return _currentState; }
        set
        {
            if (_currentState == value) return;

            _currentState = value;
        }
    }

    private void Awake()
    {
        myReflectData.color = defaultColor;

        lb = (LineRenderer)GetComponent("LineRenderer");
        lb.positionCount = 2;
        lb.material.color = defaultColor;

        _inputReader.OnStartFireEvent += OnStartShootLight;
        _inputReader.OnUpdateFireEvent += OnShootLight;
        _inputReader.OnEndFireEvent += OnEndShootLight;
    }
    private void OnStartShootLight()
    {
        lb.enabled = true;
    }
    private void OnEndShootLight()
    {
        lb.enabled = false;
    }

    private void OnShootLight()
    {
        Debug.Log("½´¿ô");
        StartShootLight(transform.position, transform.forward);
    }

    public void StartShootLight(Vector3 origin, Vector3 direction)
    {
        myReflectData.hitPos = origin;
        myReflectData.direction = direction;

        DataModify(myReflectData);
    }

    public void DataModify(ReflectData reflectData)
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
        reflectObject = reflectable;
    }

    private void OnDestroy()
    {
        _inputReader.OnStartFireEvent -= OnShootLight;
        _inputReader.OnUpdateFireEvent -= OnShootLight;
        _inputReader.OnEndFireEvent -= OnShootLight;
    }
}
