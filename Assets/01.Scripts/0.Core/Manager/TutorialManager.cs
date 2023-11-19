using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
struct TutorialInfo
{
    [TextArea(4, 4)] public string Description;
    public Image Image;
}

public class TutorialManager : MonoSingleton<TutorialManager>
{
    [SerializeField] private List<TutorialInfo> _tutorialInfo;
    [SerializeField] private CanvasGroup _gmmickPanel;
    [SerializeField] private List<GameObject> _gmmickList;

    private int _currentIndex = 1;
    private int _previousIndex = 0;

    private int _currentPanel = 1;
    public int _previousPanel = 0;

    public bool IsActive = false;

    public void ShowGmmick()
    {
        SoundManager.Instance.PlaySFXSound("Page");
        _gmmickPanel.DOFade(1, 0.5f);
        _gmmickList[_previousPanel].SetActive(false);
        _gmmickList[_currentPanel].SetActive(true);
        UIManager.Instance.InputReader.CanShoot = false;
        GameManager.Instance.StopGameImmediately(true);
        IsActive = true;
    }

    public void HideGmmick()
    {
        SoundManager.Instance.PlaySFXSound("Page");
        _gmmickPanel.DOFade(0, 0.5f);
        IsActive = false;
        UIManager.Instance.InputReader.CanShoot = true;
        GameManager.Instance.StopGameImmediately(false);
        _currentPanel++;
        _previousPanel++;
    }

    public void ShowPanel()
    {
        SoundManager.Instance.PlaySFXSound("Page");
        UIManager.Instance.FadePanel(1f, 0.5f,
            _tutorialInfo[_currentIndex].Description,
            _tutorialInfo[_currentIndex].Image,
            _tutorialInfo[_previousIndex].Image);
        UIManager.Instance.InputReader.CanShoot = false;
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
        UIManager.Instance.InputReader.CanShoot = true;
        GameManager.Instance.StopGameImmediately(false);
        _previousIndex++;
        _currentIndex++;
        if (_currentIndex >= 3 && _currentIndex <= 5)
        {
            ShowPanel();
        }
    }

    private void Update() 
    {
        if (IsActive && Input.GetKeyDown(KeyCode.E))
        {
            HideGmmick();
        }

        if (!IsActive && Input.GetKeyDown(KeyCode.T))
        {
            ShowGmmick();
        }
    }
}
