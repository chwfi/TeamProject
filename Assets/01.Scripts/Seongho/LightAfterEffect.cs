using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class LightAfterEffect : MonoBehaviour
{
    private LineRenderer lb;
    private MaterialPropertyBlock _materialPropertyBlock;

    private void OnEnable()
    {
        _materialPropertyBlock = new MaterialPropertyBlock();
    }
    void Awake()
    {
        lb = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        lb.positionCount = 2;
    }
    public void Setting(float lightWidth, Color color)
    {
        lb.startWidth = lightWidth;
        lb.endWidth = lightWidth;

        lb.enabled = true;

        _materialPropertyBlock.SetColor("_EmissionColor", color * 6f);

        // 라인 렌더러에 Property Block 적용
        lb.SetPropertyBlock(_materialPropertyBlock);


    }
    public void DrawAndFadeLine(Vector3 startPos, Vector3 endPos, float duration, Action action)
    {
        Debug.Log("fewqe : " + startPos);
        Debug.Log(endPos);

        StartCoroutine(DrawAndFadeLineCoroutine(startPos, endPos, duration, action));
    }

    private IEnumerator DrawAndFadeLineCoroutine(Vector3 startPos, Vector3 endPos, float moveAmountPerTick, Action action) //서서히 빛이 사라지는 코드
    {
        float t = 0;

        lb.SetPosition(0, startPos);
        lb.SetPosition(1, endPos); // 끝점을 고정

        float totalDistance = Vector3.Distance(startPos, endPos); // 총 거리 계산

        // 이동이 완료되면 초기화
        while (t <= 1.0f)
        {
            Vector3 lerpedPosition = Vector3.Lerp(startPos, endPos, t);

            // t 값을 업데이트하여 다음 프레임에 대비
            t += moveAmountPerTick / totalDistance * Time.deltaTime;

            lb.SetPosition(0, lerpedPosition); // 시작점만 업데이트
            yield return null;
        }
        lb.enabled = false;

        action.Invoke();
    }
}