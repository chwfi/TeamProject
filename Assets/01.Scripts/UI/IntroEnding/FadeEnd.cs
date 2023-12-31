using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeEnd : MonoBehaviour
{
    private SpriteRenderer _sprite;
    [SerializeField] private float _waitTime;
    [SerializeField] private float _fadeTime;
    [SerializeField] private float _duration;

    [SerializeField] private float _delay;

    [SerializeField] private Color c;

    Sequence seq;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>(); 
    }

    public void End()
    {
        seq = DOTween.Sequence();
        seq.Append(_sprite.DOFade(1, _fadeTime))
            .AppendInterval(_waitTime)
            .Append(_sprite.DOColor(c, _fadeTime))
            .AppendInterval(_duration)
            .Append(_sprite.DOFade(0, _fadeTime));
    }



    public void SceneMove()
    {
        SceneManager.LoadScene(SceneList.Intro);
    }
}
