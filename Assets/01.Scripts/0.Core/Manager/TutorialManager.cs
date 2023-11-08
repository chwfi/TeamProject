using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
struct TutorialInfo
{
    [TextArea] public string Description;
    public Image Image;
}

public class TutorialManager : MonoSingleton<TutorialManager>
{
    [SerializeField] private List<TutorialInfo> _tutorialInfo;

    private int _currentIndex = 1;
    private int _previousIndex = 0;

    bool _isActive = false;

    public void ShowPanel()
    {
        SoundManager.Instance.PlaySFXSound("Page");
        UIManager.Instance.FadePanel(1f, 0.5f,
            _tutorialInfo[_currentIndex].Description,
            _tutorialInfo[_currentIndex].Image,
            _tutorialInfo[_previousIndex].Image);
        GameManager.Instance.StopGameImmediately(true);
        _isActive = true;
    }

    public void HidePanel()
    {
        SoundManager.Instance.PlaySFXSound("Page");
        UIManager.Instance.FadePanel(0f, 0.5f,
            _tutorialInfo[_currentIndex].Description,
            _tutorialInfo[_currentIndex].Image,
            _tutorialInfo[_previousIndex].Image);
        _isActive = false;
        GameManager.Instance.StopGameImmediately(false);
        _previousIndex++;
        _currentIndex++;
        if (_currentIndex == 3)
        {
            ShowPanel();
        }
    }

    private void Update() //디버그용
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
