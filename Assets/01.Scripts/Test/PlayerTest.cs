using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public float rayLength = 10f; // ������ ����
    public int maxReflections = 5; // �ִ� �ݻ� Ƚ��
    public LayerMask reflectionLayer; // �ݻ��� ���̾� ����ũ

    private LineRenderer lb;
    private void Awake()
    {
        lb = (LineRenderer)GetComponent("LineRenderer");
    }
    private void Update()
    {
        RaycastWithReflection(transform.position, transform.forward, rayLength, maxReflections);
    }

    private void RaycastWithReflection(Vector3 origin, Vector3 direction, float length, int reflectionsRemaining)
    {
        //ó�� ����
        Vector3 currentPoint = origin;
        Vector3 raycastDirection = direction;

        float raycastLength = length;
        lb.positionCount = 1;
        lb.SetPosition(0, currentPoint);

        for (int i = 0; i < reflectionsRemaining; i++)
        {
            if (raycastLength <= 0)
            {
                break;
            }
            RaycastHit hit;
            if (Physics.Raycast(currentPoint, raycastDirection, out hit, raycastLength, reflectionLayer))
            {
                raycastDirection = Vector3.Reflect(raycastDirection, hit.normal);

                float dis = Vector3.Distance(currentPoint, hit.point);
                raycastLength -= dis;

                currentPoint = hit.point;

                lb.positionCount += 1;
                lb.SetPosition(lb.positionCount - 1, currentPoint);
            }
            else
            {
                // ���� �浹���� ���� ���, ����ĳ��Ʈ�� ������ �������� �׷��ݴϴ�.
                currentPoint += raycastDirection * raycastLength;
                lb.positionCount += 1;
                lb.SetPosition(lb.positionCount - 1, currentPoint);

                raycastLength -= currentPoint.magnitude;
                break;
            }
        }
        raycastLength = length;
    }
}
