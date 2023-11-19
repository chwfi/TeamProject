using System;
using System.Collections;
using UnityEngine;

public class LightAfterEffect : MonoBehaviour
{
    private LineRenderer lb;
    private TrailRenderer trail;

    private Transform _trailTrm;

    private void Awake()
    {
        lb = GetComponent<LineRenderer>();
        trail = GetComponentInChildren<TrailRenderer>();

        trail.enabled = false;
        _trailTrm = transform.GetChild(0);
    }

    private void Start()
    {
        lb.positionCount = 2;
        Destroy(gameObject, trail.time + 6f);
    }

    public void Setting(float lightWidth, Color color, bool isHit)
    {
        lb.startWidth = lightWidth;
        lb.endWidth = lightWidth;

        lb.enabled = true;
        trail.enabled = isHit;

        MaterialPropertyBlock _materialPropertyBlock = new MaterialPropertyBlock();
        MaterialPropertyBlock _materialAfterEffect = new MaterialPropertyBlock();

        _materialPropertyBlock.SetColor("_EmissionColor", color * 6f);
        _materialAfterEffect.SetColor("_EmissionColor", color * -2f);
        _materialAfterEffect.SetColor("_BaseColor", new Color(color.r, color.g, color.b, color.a - 0.9f));

        lb.SetPropertyBlock(_materialPropertyBlock);
        trail.SetPropertyBlock(_materialAfterEffect);
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

        lb.enabled = false;
        action.Invoke();
    }
}
