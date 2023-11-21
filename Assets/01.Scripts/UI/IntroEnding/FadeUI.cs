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
    private bool isChanged = false;

    Sequence seq;
    public void FadeInImg()
    {
        seq = DOTween.Sequence();
        seq.Append(_img.DOFade(1, _fadeTime))
            .AppendCallback(() => SceneMove());
    }

    public void SceneMove()
    {
        if(!isChanged)
        {
            isChanged = true;
            SceneManager.LoadScene(SceneList.LoadingScene);
        }
        else
        {
            Debug.Log("¿¿ æ»µ≈ §ª§ª");
        }
        
    }
}
