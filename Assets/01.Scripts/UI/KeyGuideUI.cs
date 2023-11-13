using TMPro;
using UnityEngine;
using DG.Tweening;

public class KeyGuideUI : LookCamUIModule //���ǿ� ������ �ٰ����� �� Q/E UI�� Fade�Ǵ� �ڵ�
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
