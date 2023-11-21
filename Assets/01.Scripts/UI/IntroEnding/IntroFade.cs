using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroFade : MonoBehaviour
{
    [SerializeField] private Image _title;
    [SerializeField] private Image _bk1;
    [SerializeField] private Image _bk2;
    [SerializeField] private Image _background;
    [SerializeField] private TextMeshProUGUI _start;
    [SerializeField] private TextMeshProUGUI _titleTex;
    [SerializeField] private TextMeshProUGUI _exit;

    [SerializeField] private float _fadeTime;
    [SerializeField] private float _waitTime;
    [SerializeField] private Color c;

    Sequence seq;

    private void Awake()
    {
        _title.color = c;
        _bk1.color = c;
        _titleTex.color = c;
        _bk2.color = c;
        _background.color = c;
        _start.color = c;
        _exit.color = c;
    }

    private void Start()
    {
        seq = DOTween.Sequence();

        seq.Append(_title.DOFade(1, _fadeTime))
            .Join(_background.DOFade(1, _fadeTime))
            .Join(_titleTex.DOFade(1, _fadeTime))
            .Join(_start.DOFade(1, _fadeTime))
            .Join(_exit.DOFade(1, _fadeTime))
            .Join(_bk1.DOFade(1, _fadeTime))
            .Join(_bk2.DOFade(1, _fadeTime));
    }

    public void SceneMove()
    {
        seq = DOTween.Sequence();
        seq.Append(_start.DOFade(0, _fadeTime))
            .Join(_exit.DOFade(0, _fadeTime))
            .Join(_bk1.DOFade(0, _fadeTime))
            .Join(_bk2.DOFade(0, _fadeTime))

            .AppendInterval(_waitTime)
            .Append(_background.DOFade(0, _fadeTime))
            .Join(_title.DOFade(0, _fadeTime))
            .Join(_titleTex.DOFade(0, _fadeTime))

            .AppendCallback(() => SceneManager.LoadScene(SceneList.IntroStory));
    }

    public void ExitGame()
    {
        Debug.Log("게임 나감");
        Application.Quit();
    }
}
