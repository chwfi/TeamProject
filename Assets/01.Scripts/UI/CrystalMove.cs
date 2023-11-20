using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CrystalMove : MonoBehaviour
{
    [SerializeField] private Vector3 _targetPos;
    [SerializeField] private float _speed;
    [SerializeField] private float _fadeTime;
    [SerializeField] private float _waitTime;

    private SpriteRenderer _sprite;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Sequence seq = DOTween.Sequence();
        seq.PrependInterval(_waitTime);
        seq.Append(_sprite.DOFade(1, _fadeTime))
        .Append(transform.DOMove(_targetPos, _speed))
        .Append(_sprite.DOFade(0, _fadeTime));
    }
}