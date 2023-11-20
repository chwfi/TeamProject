using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEnd : MonoBehaviour
{
    private SpriteRenderer _sprite;
    [SerializeField] private float _waitTime;
    [SerializeField] private float _fadeTime;
    [SerializeField] private float _duration;

    [SerializeField] private float _delay;

    [SerializeField] private Color c;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void End()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(_sprite.DOFade(1, _fadeTime))
            .AppendInterval(_waitTime)
            .Append(_sprite.DOColor(c, _fadeTime))
            .AppendInterval(_duration)
            .Append(_sprite.DOFade(0, _fadeTime));
    }
}
