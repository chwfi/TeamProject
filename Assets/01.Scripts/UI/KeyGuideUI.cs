using TMPro;
using UnityEngine;
using DG.Tweening;

public class KeyGuideUI : LookCamUIModule //원판에 가까이 다가갔을 때 Q/E UI가 Fade되는 코드
{
    private TextMeshPro _text;

    protected override void Awake()
    {
        _text = GetComponent<TextMeshPro>();
    }

    public void Fade(float value, float time)
    {
        _text.DOFade(value, time);
    }
}
