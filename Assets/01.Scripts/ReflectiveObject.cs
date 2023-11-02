using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define.Define;

//여기서는 받은 벡터로 반사시켜주고 라인렌더러를 그리는 곳
public class ReflectiveObject : MonoBehaviour, IReflectable
{
    private Collider _col;
    private LineRenderer _lr;
    public Color defaultColor;

    protected virtual void Awake()
    {
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

    public void OnHandleReflected(Vector3 inHitPos, Vector3 inDirection, Vector3 normal, Color inColor)
    {
        _lr.SetPosition(0, inHitPos);
        var cCol = ColorSystem.GetColorCombination(inColor, defaultColor);
        _lr.material.color = cCol;
        var raycastDirection = Vector3.Reflect(inDirection, normal);

        Debug.Log(gameObject.name + " : " + inDirection + ": " + normal + ": " + raycastDirection);
        RaycastHit hit;

        if (Physics.Raycast(inHitPos, raycastDirection, out hit, 1000, ReflectionLayer))
        {
            if (_col.name == hit.collider.name) return;

            _lr.SetPosition(1, hit.point);

            if (hit.collider.TryGetComponent<IReflectable>(out var reflectableObject))
            {
                Debug.Log(gameObject.name + " : " + _col.name + ": " + hit.collider.name);
                reflectableObject?.OnHandleReflected(hit.point, raycastDirection, hit.normal, cCol);
            }
        }
        else
        {
            _lr.SetPosition(1, inHitPos + raycastDirection * 1000);
        }
    }

    public void UnHandleReflected(Vector3 inHitPos, Vector3 inDirection, Vector3 inNormal)
    {
        Init();
    }
}

