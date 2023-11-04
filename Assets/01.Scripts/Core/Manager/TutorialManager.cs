using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
struct TutorialInfo
{
    [TextArea] public string Description;
    public Sprite Sprite;
}

public class TutorialManager : MonoSingleton<TutorialManager>
{
    [SerializeField] private List<TutorialInfo> tutorialInfo;

    private int _currentIndex = 0;
    bool _isActive = false;

    public void ShowPanel()
    {
        SoundManager.Instance.PlaySFXSound("Page");
        UIManager.Instance.FadePanel(1f, 0.5f,
                tutorialInfo[_currentIndex].Description,
                tutorialInfo[_currentIndex].Sprite);
        GameManager.Instance.StopGameImmediately(true);
        _isActive = true;
    }

    public void HidePanel()
    {
        SoundManager.Instance.PlaySFXSound("Page");
        UIManager.Instance.FadePanel(0f, 0.5f,
                tutorialInfo[_currentIndex].Description,
                tutorialInfo[_currentIndex].Sprite);
        _isActive = false;
        GameManager.Instance.StopGameImmediately(false);
        _currentIndex++;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ShowPanel();
        }

        if (_isActive && Input.GetKeyDown(KeyCode.E))
        {
            HidePanel();
        }
    }
}
