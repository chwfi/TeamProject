using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public float raycastDistance = 4;
    public float rayLength = 10f; // ������ ����
    public int maxReflections = 5; // �ִ� �ݻ� Ƚ��
    public LayerMask reflectionLayer; // �ݻ��� ���̾� ����ũ

    private float _raycastDistance = 0;

    private LineRenderer lb;
    private void Awake()
    {
        lb = (LineRenderer)GetComponent("LineRenderer");
    }
    private void Start()
    {
        _raycastDistance = raycastDistance;
    }
    private void Update()
    {
        RaycastWithReflection(transform.position, transform.forward, rayLength, maxReflections);
    }

    private void RaycastWithReflection(Vector3 origin, Vector3 direction, float length, int reflectionsRemaining)
    {
        //ó�� ����
        Vector3 raycastDirection = transform.forward;
        Vector3 currentPoint = transform.position;

        lb.positionCount = 1;
        lb.SetPosition(0, currentPoint);

        for (int i = 0; i < 5; i++)
        {
            if (_raycastDistance <= 0)
            {
                break;
            }
            RaycastHit hit;
            if (Physics.Raycast(currentPoint, raycastDirection, out hit, _raycastDistance, reflectionLayer))
            {
                raycastDirection = Vector3.Reflect(raycastDirection, hit.normal);

                float dis = Vector3.Distance(currentPoint, hit.point);
                _raycastDistance -= dis;

                currentPoint = hit.point;

                lb.positionCount += 1;
                lb.SetPosition(lb.positionCount - 1, currentPoint);
            }
            else
            {
                // ���� �浹���� ���� ���, ����ĳ��Ʈ�� ������ �������� �׷��ݴϴ�.
                currentPoint += raycastDirection * _raycastDistance;
                lb.positionCount += 1;
                lb.SetPosition(lb.positionCount - 1, currentPoint);

                _raycastDistance -= currentPoint.magnitude;
                break;
            }
        }
        _raycastDistance = raycastDistance;
    }
}
