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
    private RemainedLight trail;
    private MaterialPropertyBlock _materialPropertyBlock;

    private void OnEnable()
    {
        _materialPropertyBlock = new MaterialPropertyBlock();
    }
    void Awake()
    {
        lb = GetComponent<LineRenderer>();
        trail = FindAnyObjectByType<RemainedLight>();
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

        // ���� �������� Property Block ����
        lb.SetPropertyBlock(_materialPropertyBlock);


    }
    public void DrawAndFadeLine(Vector3 startPos, Vector3 endPos, float duration, Action action)
    {
        Debug.Log("fewqe : " + startPos);
        Debug.Log(endPos);

        StartCoroutine(DrawAndFadeLineCoroutine(startPos, endPos, duration, action));
    }

    private IEnumerator DrawAndFadeLineCoroutine(Vector3 startPos, Vector3 endPos, float moveAmountPerTick, Action action) //������ ���� ������� �ڵ�
    {
        float t = 0;

        lb.SetPosition(0, startPos);
        lb.SetPosition(1, endPos);

        float totalDistance = Vector3.Distance(startPos, endPos);

        while (t <= 1.0f)
        {
            Vector3 lerpedPosition = Vector3.Lerp(startPos, endPos, t);


            t += moveAmountPerTick / totalDistance * Time.deltaTime;

            lb.SetPosition(0, lerpedPosition); 
            yield return null;
        }
        lb.enabled = false;

        action.Invoke();
    }
}