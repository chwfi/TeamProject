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

    private void Awake()
    {
        _title.color = c;
        _start.color = c;
        _exit.color = c;
    }

    private void Start()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(_title.DOFade(1, _fadeTime))
            .Join(_start.DOFade(1, _fadeTime))
            .Join(_exit.DOFade(1, _fadeTime));
    }

    public void SceneMove(string name)
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(_title.DOFade(0, 1f))
            .Join(_start.DOFade(0, 1f))
            .Join(_exit.DOFade(0, 1f))
            .AppendInterval(_waitTime)
            .AppendCallback(() => SceneManager.LoadScene(name));
    }

    public void ExitGame()
    {
        Debug.Log("게임 나감");
        Sequence seq = DOTween.Sequence();

        seq.Append(_title.DOFade(0, 1f))
            .Join(_start.DOFade(0, 1f))
            .Join(_exit.DOFade(0, 1f))
            .AppendCallback(() => Application.Quit());
    }
}
