using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class EndingCradit : MonoBehaviour
{
    private TextMeshProUGUI _endTex;

    private void Awake()
    {
        _endTex = transform.Find("End").GetComponent<TextMeshProUGUI>();
    }

    public void ToBeContinue(string s)
    {
        Sequence seq = DOTween.Sequence();
        _endTex.text = s;

        seq.Append(_endTex.DOFade(1, 2f))
            .AppendInterval(4f)
            .Append(_endTex.DOFade(0, 2f));

    }
}
