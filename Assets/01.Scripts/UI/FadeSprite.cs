using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class FadeSprite : MonoBehaviour
{
    private SpriteRenderer _sprite;
    [SerializeField] private float _waitTime;
    [SerializeField] private float _fadeTime;
    [SerializeField] private float _duration;

    [SerializeField] private float _delay;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(_sprite.DOFade(1, _fadeTime))
            .AppendInterval(_duration)
            .Append(_sprite.DOFade(0, _fadeTime));
    }
    public void CrystalChangeColor()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(_sprite.DOFade(1, _fadeTime))
            .AppendInterval(_waitTime)
            .Join(_sprite.DOColor(Color.gray, _fadeTime))
            .AppendInterval(_delay)
            .Append(_sprite.DOFade(0, _fadeTime));
    }

    public void SceneMove(string name)
    {
        SceneManager.LoadScene(name);
    }
}
