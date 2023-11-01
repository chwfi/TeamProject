using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define.Define;

public class PlayerTest : MonoBehaviour
{
    public Color defaultColor;
    private LineRenderer lb;
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
    public void OnShootLight() //연결할 함수
    {
        RaycastWithReflection(transform.position, transform.forward);
    }
    private void RaycastWithReflection(Vector3 origin, Vector3 direction)
    {
        lb.SetPosition(0, origin);

        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, 1000, ReflectionLayer))
        {
            Debug.Log("으악");
            lb.SetPosition(1, hit.point);

            if (hit.collider.TryGetComponent<IReflectable>(out var reflectableObject))
            {
                reflectableObject?.OnReflected(hit.point, direction, hit.normal, defaultColor);
            }
        }
        else
        {
            lb.SetPosition(1, direction * 1000);
        }
    }
}
