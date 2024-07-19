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
    [TextArea] public string text;
    public Sprite sprite;
    public float waitTime;
    public float fadeTime; //알파 조정 시간
    public float duration;   //지속 시간
    public bool isConnect;

    public UnityEvent OnEvent;
}

public class DrawStory : MonoBehaviour
{
    [SerializeField]
    private List<OutputContent> drawList;

    private SpriteRenderer spriteRenderer;
    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        spriteRenderer = GetComponentFromName<SpriteRenderer>("DrawSprite");
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>("TextContent");
    }

    private void Start()
    {
        if (spriteRenderer != null && textMeshPro != null)
        {
            StartCoroutine(TextDraw());
        }
    }

    private IEnumerator TextDraw()
    {
        foreach (var content in drawList)
        {
            SetContent(content);

            yield return new WaitForSeconds(content.waitTime);

            yield return FadeInContent(content.fadeTime);

            content.OnEvent?.Invoke();

            yield return new WaitForSeconds(content.duration);

            yield return FadeOutContent(content.fadeTime, content.isConnect);
        }
    }

    private void SetContent(OutputContent content)
    {
        spriteRenderer.sprite = content.sprite;
        textMeshPro.text = content.text;
    }

    private IEnumerator FadeInContent(float fadeTime)
    {
        var textFadeIn = textMeshPro.DOFade(1, fadeTime);
        var spriteFadeIn = spriteRenderer.DOFade(1, fadeTime);

        yield return textFadeIn.WaitForCompletion();
        yield return spriteFadeIn.WaitForCompletion();
    }

    private IEnumerator FadeOutContent(float fadeTime, bool isConnect)
    {
        var textFadeOut = textMeshPro.DOFade(0, fadeTime);
        var spriteFadeOut = isConnect ? null : spriteRenderer.DOFade(0, fadeTime);

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
