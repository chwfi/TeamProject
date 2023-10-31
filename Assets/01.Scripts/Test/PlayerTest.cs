using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public float rayLength = 10f; // 레이의 길이
    public int maxReflections = 5; // 최대 반사 횟수
    public LayerMask reflectionLayer; // 반사할 레이어 마스크

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
        //처음 방향
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
                // 벽에 충돌하지 않은 경우, 레이캐스트가 끝나는 지점까지 그려줍니다.
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
