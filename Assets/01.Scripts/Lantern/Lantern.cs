using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define.Define;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(LineRenderer))]
public class Lantern : MonoBehaviour
{
    [SerializeField] private Transform _firePosition;

    public Color defaultColor;
    private LineRenderer lb;

    private void Awake()
    {
        lb = (LineRenderer)GetComponent("LineRenderer");
        lb.enabled = false;
        lb.positionCount = 2;
        lb.material.color = defaultColor;
    }

    public void OnShootLight(bool value) //연결할 함수
    {
        lb.enabled = value;
        RaycastWithReflection(_firePosition.position, transform.forward);
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
                reflectableObject?.OnHandleReflected(hit.point, direction, hit.normal, defaultColor);
            }
        }
        else
        {
            lb.SetPosition(1, direction * 100);
        }
    }
}
