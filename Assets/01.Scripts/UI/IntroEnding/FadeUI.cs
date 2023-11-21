using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{
    [SerializeField] private Image _img;

    [SerializeField] private float _fadeTime;

    Sequence seq;
    public void FadeInImg(string name)
    {
        seq = DOTween.Sequence();
        seq.Append(_img.DOFade(1, _fadeTime))
            .AppendCallback(() => SceneManager.LoadScene(name));
    }
}
