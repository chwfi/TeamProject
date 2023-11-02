using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class LightAnimation : MonoBehaviour
{
    private Light _light;

    private float _baseRadius;
    private float _baseIntensity;
    private int _toggle = 1;

    [SerializeField] private float _radiusRandomness = 1f;

    private void Awake()
    {
        _light = GetComponent<Light>();
        _baseRadius = _light.range;
        _baseIntensity = _light.intensity;
    }

    private void Start()
    {
        StartShake();
    }

    private void StartShake()
    {
        float targetRadius = _baseRadius + _toggle * Random.Range(0, _radiusRandomness);
        float targetIntenSity = _baseIntensity + _toggle * Random.Range(0, _radiusRandomness);
        _toggle *= -1;

        float targetTime = Random.Range(0.5f, 0.9f);

        Sequence seq = DOTween.Sequence();

        var t1 = DOTween.To(() => _light.intensity,
            value => _light.intensity = value,
            targetIntenSity,
            targetTime);

        var t2 = DOTween.To(() => _light.range,
            value => _light.range = value,
            targetRadius,
            Random.Range(0, 0.5f));

        seq.Append(t1);
        seq.Join(t2);
        seq.AppendCallback(() => StartShake());
    }
}
