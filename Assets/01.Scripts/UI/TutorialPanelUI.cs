using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class TutorialPanelUI : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private TextMeshProUGUI _descriptionText;
    private Image _image;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _descriptionText = transform.Find("Info").GetComponent<TextMeshProUGUI>();
        _image = transform.Find("Image").GetComponent<Image>();

        _canvasGroup.alpha = 0.0f;
    }

    public void Fade(float value, float time, string contents, Sprite sprite)
    {
        _descriptionText.text = contents;
        _image.sprite = sprite;
        _canvasGroup.DOFade(value, time);
    }
}
