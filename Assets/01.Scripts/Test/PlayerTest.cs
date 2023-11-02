using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using static Define.Define;

public class PlayerTest : MonoBehaviour
{
    public Color defaultColor;
    private LineRenderer lb;

    private ReflectData _currentData;
    public ReflectData CurrentData
    {
        get
        {
            return _currentData;
        }
        set
        {
            if (_currentData.Equals(value)) return;
            _currentData = value;
        }
    }
    private void Awake()
    {
        lb = (LineRenderer)GetComponent("LineRenderer");
        lb.positionCount = 2;
        lb.material.color = defaultColor;
    }
    private void Update()
    {
        OnShootLight();
    }

    public void OnShootLight()
    {
        RaycastWithReflection(transform.position, transform.forward);
    }

    private void RaycastWithReflection(Vector3 origin, Vector3 direction)
    {
        lb.SetPosition(0, origin);

        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, 1000, ReflectionLayer))
        {
            Debug.Log("����");
            lb.SetPosition(1, hit.point);

            if (hit.collider.TryGetComponent<IReflectable>(out var reflectableObject))
            {
                reflectableObject?.OnHandleReflected(hit.point, direction, hit.normal, defaultColor);
            }
        }
        else
        {
            lb.SetPosition(1, direction * 1000);
        }
    }
}
