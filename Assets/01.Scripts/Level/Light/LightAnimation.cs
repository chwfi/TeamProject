using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAnimation : MonoBehaviour
{
    private Light _light;

    [SerializeField] private float _radiusRandomness;

    private float _baseRadius;
    private int _toggle;

    private void Awake()
    {
        _light = GetComponent<Light>();

        _baseRadius = _light.range;
    }

    private void Update()
    {
        float targetRadius = _baseRadius + _toggle * Random.Range(0, _radiusRandomness);
    }
}
