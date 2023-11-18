using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class OutputText
{
    [TextArea] public string text;
    public float waitTime;
    public float fadeTime; //알파 조정 시간
    public float duration;   //지속 시간
}

[Serializable]
public class OutputSprite
{
    public Sprite sprite;
    public float waitTime;
    public float fadeTime;
    public float duration;
}

public class DrawIntro : MonoBehaviour
{
    public List<OutputText> TextList;
    public List<OutputSprite> SpriteList;
    
    private SpriteRenderer _sprite;
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _sprite = GameObject.Find("DrawSprite").GetComponent<SpriteRenderer>();
        _text = transform.Find("TextContent").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        for (int i = 0; i < TextList.Count; i++)
        {
            _sprite.sprite = SpriteList[i].sprite;
            _text.text = TextList[i].text;
            
            StartCoroutine(TextDraw(i, TextList[i].waitTime, TextList[i].duration, TextList[i].fadeTime));
            StartCoroutine(SpriteDraw(i, SpriteList[i].waitTime, SpriteList[i].duration, SpriteList[i].fadeTime));
        }
    }

    private IEnumerator TextDraw(int i, float waitTIme, float duration, float fadeTime)
    {
        yield return new WaitForSeconds(waitTIme);
        _text.DOFade(1, fadeTime);
        yield return new WaitForSeconds(duration);
        _text.DOFade(0, fadeTime);
    }
    private IEnumerator SpriteDraw(int i, float waitTIme, float duration, float fadeTime)
    {
        yield return new WaitForSeconds(waitTIme);
        _sprite.DOFade(1, fadeTime);
        yield return new WaitForSeconds(duration);
        _sprite.DOFade(0, fadeTime);
    }
}
