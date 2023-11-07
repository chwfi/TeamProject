using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class TutorialPanelUI : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private TextMeshProUGUI _descriptionText;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _descriptionText = transform.Find("Info").GetComponent<TextMeshProUGUI>();
        _canvasGroup.alpha = 0.0f;
    }

    public void Fade(float value, float time, string contents, Image image, Image previousImage)
    {
        _descriptionText.text = contents;
        image.gameObject.SetActive(true);
        previousImage.gameObject.SetActive(false);
        _canvasGroup.DOFade(value, time);
    }
}
