using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private SpriteRenderer _spriteRender;
    [SerializeField] private Transform _circle;
    [SerializeField] private float _rotateValue;

    private bool isComplete = false;
    private Sequence seq;

    private void Awake()
    {
        AssetLoader.Instance.OnLoadComplete += HandleLoadComplete;
        seq = DOTween.Sequence();
    }

    private void OnDisable()
    {
        AssetLoader.Instance.OnLoadComplete -= HandleLoadComplete;
    }

    private void HandleLoadComplete()
    {
        isComplete = true;
    }

    private void Update()
    {

        seq.Append(_text.DOFade(5f, 2f))
            .Join(_spriteRender.DOFade(5f, 2f))
            .AppendInterval(3f)
            .OnComplete(() =>
            {
                if (isComplete)
                {
                    SceneManager.LoadScene(SceneList.TestMap);
                    Destroy(gameObject);
                }
            });

        _circle.Rotate(0, 0, _rotateValue * Time.deltaTime);
    }
}
