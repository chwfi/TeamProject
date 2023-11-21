using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class StroyManager : MonoBehaviour
{
    public InputReader InputReader;

    [SerializeField] private TextMeshProUGUI _skipTex;
    [SerializeField] private Image _skipImg;

    [SerializeField] private float _fadeTime;
    [SerializeField] private float _duration;

    public bool pressedKey;
    public bool fadeOut;
    public bool canSkip;

    public UnityEvent OnEvent;
    Sequence seq;

    private void Start()
    {
        seq = DOTween.Sequence();
    }

    private void Update()
    {
        if (pressedKey)
        {
            seq.Append(_skipTex.DOFade(1, _fadeTime).OnComplete(() =>
            {
                fadeOut = true;
                canSkip = true;
                pressedKey = false;
            }))
            .Join(_skipImg.DOFade(1, _fadeTime));
                //.AppendCallback(() => fadeOut = true)
                //.AppendCallback(() => canSkip = true)
                //.AppendCallback(() => pressedKey = false);
        }

        if (fadeOut)
        {
            seq.PrependInterval(_duration)
                .AppendCallback(() => canSkip = false)
                .Append(_skipTex.DOFade(0, _fadeTime))
                .Join(_skipImg.DOFade(0, _fadeTime));
        }

        if (canSkip && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            OnEvent.Invoke();
        }

        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            pressedKey = true;
        }
    }
}
