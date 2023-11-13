using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
struct TutorialInfo
{
    [TextArea(4, 4)] public string Description;
    public Image Image;
}

public class TutorialManager : MonoSingleton<TutorialManager>
{
    [SerializeField] private List<TutorialInfo> _tutorialInfo;

    private int _currentIndex = 1;
    private int _previousIndex = 0;

    public bool IsActive = false;

    public void ShowPanel()
    {
        SoundManager.Instance.PlaySFXSound("Page");
        UIManager.Instance.FadePanel(1f, 0.5f,
            _tutorialInfo[_currentIndex].Description,
            _tutorialInfo[_currentIndex].Image,
            _tutorialInfo[_previousIndex].Image);
        GameManager.Instance.StopGameImmediately(true);
        IsActive = true;
    }

    public void HidePanel()
    {
        SoundManager.Instance.PlaySFXSound("Page");
        UIManager.Instance.FadePanel(0f, 0.5f,
            _tutorialInfo[_currentIndex].Description,
            _tutorialInfo[_currentIndex].Image,
            _tutorialInfo[_previousIndex].Image);
        IsActive = false;
        GameManager.Instance.StopGameImmediately(false);
        _previousIndex++;
        _currentIndex++;
        if (_currentIndex >= 3 && _currentIndex <= 6)
        {
            ShowPanel();
        }
    }

    private void Update() 
    {
        if (IsActive && Input.GetKeyDown(KeyCode.E))
        {
            HidePanel();
        }
    }
}
