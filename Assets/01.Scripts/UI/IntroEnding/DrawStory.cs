using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

[Serializable]
public class OutputContent
{
    [TextArea] public string Text;
    public Sprite Sprite;
    public float WaitTime;
    public float FadeTime; //알파 조정 시간
    public float Duration;   //지속 시간
    public bool IsConnect;

    public UnityEvent OnEvent;
}

public class DrawStory : MonoBehaviour
{
    [SerializeField]
    private List<OutputContent> _drawList;

    private SpriteRenderer _spriteRenderer;
    private TextMeshProUGUI _textMeshPro;

    private void Awake()
    {
        _spriteRenderer = GetComponentFromName<SpriteRenderer>("DrawSprite");
        _textMeshPro = GetComponentInChildren<TextMeshProUGUI>("TextContent");
    }

    private void Start()
    {
        if (_spriteRenderer != null && _textMeshPro != null)
        {
            StartCoroutine(TextDraw());
        }
    }

    private IEnumerator TextDraw()
    {
        foreach (var content in _drawList)
        {
            SetContent(content);

            yield return new WaitForSeconds(content.WaitTime);

            yield return FadeInContent(content.FadeTime);

            content.OnEvent?.Invoke();

            yield return new WaitForSeconds(content.Duration);

            yield return FadeOutContent(content.FadeTime, content.IsConnect);
        }
    }

    private void SetContent(OutputContent content)
    {
        _spriteRenderer.sprite = content.Sprite;
        _textMeshPro.text = content.Text;
    }

    private IEnumerator FadeInContent(float fadeTime)
    {
        var textFadeIn = _textMeshPro.DOFade(1, fadeTime);
        var spriteFadeIn = _spriteRenderer.DOFade(1, fadeTime);

        yield return textFadeIn.WaitForCompletion();
        yield return spriteFadeIn.WaitForCompletion();
    }

    private IEnumerator FadeOutContent(float fadeTime, bool isConnect)
    {
        var textFadeOut = _textMeshPro.DOFade(0, fadeTime);
        var spriteFadeOut = isConnect ? null : _spriteRenderer.DOFade(0, fadeTime);

        yield return textFadeOut.WaitForCompletion();

        if (!isConnect && spriteFadeOut != null)
        {
            yield return spriteFadeOut.WaitForCompletion();
        }
    }

    private T GetComponentFromName<T>(string name) where T : Component
    {
        var gameObject = GameObject.Find(name);

        if (gameObject != null)
        {
            return gameObject.GetComponent<T>();
        }

        return null;
    }

    private T GetComponentInChildren<T>(string name) where T : Component
    {
        var child = transform.Find(name);

        if (child != null)
        {
            return child.GetComponent<T>();
        }

        return null;
    }
}
