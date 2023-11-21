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
    [SerializeField] private TextMeshProUGUI _start;
    [SerializeField] private TextMeshProUGUI _exit;

    [SerializeField] private float _fadeTime;
    [SerializeField] private float _waitTime;
    [SerializeField] private Color c;

    Sequence seq;

    private void Awake()
    {
        _title.color = c;
        _start.color = c;
        _exit.color = c;
    }

    private void Start()
    {
        seq = DOTween.Sequence();

        seq.Append(_title.DOFade(1, _fadeTime))
            .Join(_start.DOFade(1, _fadeTime))
            .Join(_exit.DOFade(1, _fadeTime));
    }

    public void SceneMove()
    {
        seq = DOTween.Sequence();
        seq.Append(_title.DOFade(0, 1f))
            .Join(_start.DOFade(0, 1f))
            .Join(_exit.DOFade(0, 1f))
            .AppendInterval(_waitTime)
            .AppendCallback(() => SceneManager.LoadScene(SceneList.IntroStory));
    }

    public void ExitGame()
    {
        Debug.Log("게임 나감");
        seq = DOTween.Sequence();
        seq.Append(_title.DOFade(0, 1f))
            .Join(_start.DOFade(0, 1f))
            .Join(_exit.DOFade(0, 1f))
            .AppendCallback(() => Application.Quit());
    }
}
