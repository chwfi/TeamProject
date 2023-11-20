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

    private bool pressedKey;
    private bool fadeOut;
    private bool canSkip;

    public UnityEvent OnEvent;

    private void Update()
    {
        Sequence seq = DOTween.Sequence();

        if(pressedKey)
        {
            seq.Append(_skipTex.DOFade(1, _fadeTime))
                .Join(_skipImg.DOFade(1, _fadeTime))
                .AppendCallback(()=>fadeOut = true)
                .AppendCallback(()=>canSkip = true)
                .AppendCallback(()=>pressedKey = false);
        }

        if(fadeOut)
        {
            seq.PrependInterval(_duration)
                .AppendCallback(() => canSkip = false)
                .Append(_skipTex.DOFade(0, _fadeTime))
                .Join(_skipImg.DOFade(0, _fadeTime));
        }

        if(canSkip && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            OnEvent.Invoke();
        }

        if(Keyboard.current.anyKey.wasPressedThisFrame)
        {
            pressedKey = true;
        }
    }
}
