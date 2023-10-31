using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define.Define;

//���⼭�� ���� ���ͷ� �ݻ�����ְ� ���η������� �׸��� ��
public class ReflectiveObject : MonoBehaviour, IReflectable
{

    private LineRenderer _lr;
    public Color defaultColor;
    protected virtual void Awake()
    {
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

    public void OnReflected(Vector3 inHitPos, Vector3 inDirection, Vector3 normal, Color inColor) //��ȭ �ɶ���
    {
        _lr.SetPosition(0, inHitPos);
        var cCol = ColorSystem.GetColorCombination(inColor, defaultColor);
        _lr.material.color = cCol;

        var raycastDirection = Vector3.Reflect(inDirection, normal);
        RaycastHit hit;
        if (Physics.Raycast(inHitPos, raycastDirection, out hit, 1000, ReflectionLayer))
        {
            raycastDirection = Vector3.Reflect(raycastDirection, hit.normal);
            _lr.SetPosition(1, hit.point);

            if (hit.collider.TryGetComponent<IReflectable>(out var reflectableObject))
                if (reflectableObject.GetHashCode() != this.GetHashCode())
                    reflectableObject?.OnReflected(hit.point, raycastDirection, hit.normal,
                       cCol);
        }
        else
        {
            _lr.SetPosition(1, raycastDirection * 1000);
        }
    }

    public void UnReflected(Vector3 inHitPos, Vector3 inDirection, Vector3 inNormal)
    {
        Init();
    }
}
