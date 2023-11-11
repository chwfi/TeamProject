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
        StartCoroutine(DrawAndFadeLineCoroutine(startPos, endPos, duration, action));
    }
    private IEnumerator DrawAndFadeLineCoroutine(Vector3 startPos, Vector3 endPos, float duration, Action action) //서서히 빛이 사라지는 코드
    {
        float startTime = 0;
        lb.SetPosition(0, startPos);
        lb.SetPosition(1, endPos);

        while (startTime < duration)
        {
            startTime += Time.deltaTime;
            Vector3 lerpedPosition = Vector3.Lerp(startPos, endPos, startTime / duration);

            lb.SetPosition(0, lerpedPosition);
            Debug.Log(lerpedPosition);
            yield return null;
        }

        action.Invoke();
    }
}