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

public class DrawIntro : MonoBehaviour
{
    public List<OutputContent> DrawList;
    
    private SpriteRenderer _sprite;
    private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _endTex;

    public int num;

    private void Awake()
    {
        _sprite = GameObject.Find("DrawSprite").GetComponent<SpriteRenderer>();
        _text = transform.Find("TextContent").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        StartCoroutine(TextDraw());
    }

    private IEnumerator TextDraw()
    {
        for (int i = 0; i < DrawList.Count; i++)
        {
            _sprite.sprite = DrawList[i].sprite;
            _text.text = DrawList[i].text;
            num = i;
            
            yield return new WaitForSeconds(DrawList[i].waitTime);
            _text.DOFade(1, DrawList[i].fadeTime);
            _sprite.DOFade(1, DrawList[i].fadeTime);
            if (DrawList[i].OnEvent != null)
            {
                DrawList[i].OnEvent?.Invoke();
            }
            yield return new WaitForSeconds(DrawList[i].duration);

            if (!DrawList[i].isConnect)
            {                
                _text.DOFade(0, DrawList[i].fadeTime);
                _sprite.DOFade(0, DrawList[i].fadeTime);
                yield return new WaitForSeconds(DrawList[i].fadeTime);
            }
            else
            {
                _text.DOFade(0, DrawList[i].fadeTime);
                yield return new WaitForSeconds(DrawList[i].fadeTime);
            }
        }
    }

    public void ToBeContinue(string s)
    {
        Sequence seq = DOTween.Sequence();
        _endTex.text = s;

        seq.Append(_endTex.DOFade(1, DrawList[num].fadeTime))
            .AppendInterval(DrawList[num].duration)
            .Append(_endTex.DOFade(0, DrawList[num].fadeTime));
        
    }
}
