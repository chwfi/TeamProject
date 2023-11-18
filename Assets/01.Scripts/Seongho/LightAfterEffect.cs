using Cinemachine.Utility;
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
    private TrailRenderer trail;
    private MaterialPropertyBlock _materialPropertyBlock;

    private Transform _trailTrm;
    private void OnEnable()
    {
        _materialPropertyBlock = new MaterialPropertyBlock();
    }
    void Awake()
    {
        lb = GetComponent<LineRenderer>();
        trail = GetComponentInChildren<TrailRenderer>();


        trail.enabled = false;
        _trailTrm = transform.GetChild(0);
    }
    private void Start()
    {
        lb.positionCount = 2;

        Destroy(gameObject, trail.time + 3f);
    }
    public void Setting(float lightWidth, Color color, bool isGrow)
    {
        lb.startWidth = lightWidth;
        lb.endWidth = lightWidth;

        lb.enabled = true;

        trail.enabled = !isGrow;

        _materialPropertyBlock.SetColor("_EmissionColor", color * 6f);
        lb.SetPropertyBlock(_materialPropertyBlock);
        trail.SetPropertyBlock(_materialPropertyBlock);
    }
    public void DrawAndFadeLine(Vector3 startPos, Vector3 endPos, float duration, Action action)
    {
        StartCoroutine(DrawAndFadeLineCoroutine(startPos, endPos, duration, action));
    }

    private IEnumerator DrawAndFadeLineCoroutine(Vector3 startPos, Vector3 endPos, float moveAmountPerTick, Action action)
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

            _trailTrm.position = lerpedPosition;
            yield return null;
        }
        _trailTrm.position = endPos;
        lb.enabled = false;

        action.Invoke();
    }
}