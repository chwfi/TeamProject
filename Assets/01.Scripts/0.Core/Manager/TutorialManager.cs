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
    [SerializeField] private CanvasGroup _gimmickPanel;
    [SerializeField] private List<GameObject> _gimmickList;
    [SerializeField] private CanvasGroup _tutoPanel;
    [SerializeField] private List<GameObject> _tutoList;

    private int _currentGimmick = 1;
    public int _previousGimmick = 0;

    private int _currentTuto = 1;
    public int _previousTuto = 0;

    public bool IsActive = false;
    public bool IsActiveTuto = false;

    private void Start()
    {
        Invoke("ShowTuto", 2f);
    }

    public void ShowGmmick()
    {
        SoundManager.Instance.PlaySFXSound(SFX.Page);
        _gimmickPanel.DOFade(1, 0.5f);
        _gimmickList[_previousGimmick].SetActive(false);
        _gimmickList[_currentGimmick].SetActive(true);
        UIManager.Instance.InputReader.CanShoot = false;
        GameManager.Instance.StopGameImmediately(true);
        IsActive = true;
    }

    public void HideGmmick()
    {
        SoundManager.Instance.PlaySFXSound(SFX.Page);
        _gimmickPanel.DOFade(0, 0.5f);
        IsActive = false;
        UIManager.Instance.InputReader.CanShoot = true;
        GameManager.Instance.StopGameImmediately(false);
        _currentGimmick++;
        _previousGimmick++;
    }

    public void ShowTuto()
    {
        SoundManager.Instance.PlaySFXSound(SFX.Page );
        _tutoPanel.DOFade(1, 0.5f);
        _tutoList[_previousTuto].SetActive(false);
        _tutoList[_currentTuto].SetActive(true);
        UIManager.Instance.InputReader.CanShoot = false;
        GameManager.Instance.StopGameImmediately(true);
        IsActiveTuto = true;
    }

    private void HideTuto()
    {
        SoundManager.Instance.PlaySFXSound(SFX.Page);
        _tutoPanel.DOFade(0, 0.5f);
        IsActiveTuto = false;
        UIManager.Instance.InputReader.CanShoot = true;
        GameManager.Instance.StopGameImmediately(false);
        _currentTuto++;
        _previousTuto++;
        if (_currentTuto < _tutoList.Count)
            ShowTuto();
    }

    //public void ShowPanel()
    //{
    //    SoundManager.Instance.PlaySFXSound("Page");
    //    UIManager.Instance.FadePanel(1f, 0.5f,
    //        _tutorialInfo[_currentIndex].Description,
    //        _tutorialInfo[_currentIndex].Image,
    //        _tutorialInfo[_previousIndex].Image);
    //    UIManager.Instance.InputReader.CanShoot = false;
    //    GameManager.Instance.StopGameImmediately(true);
    //    IsActive = true;
    //}

    //public void HidePanel()
    //{
    //    SoundManager.Instance.PlaySFXSound("Page");
    //    UIManager.Instance.FadePanel(0f, 0.5f,
    //        _tutorialInfo[_currentIndex].Description,
    //        _tutorialInfo[_currentIndex].Image,
    //        _tutorialInfo[_previousIndex].Image);
    //    IsActive = false;
    //    UIManager.Instance.InputReader.CanShoot = true;
    //    GameManager.Instance.StopGameImmediately(false);
    //    _previousIndex++;
    //    _currentIndex++;
    //    if (_currentIndex >= 3 && _currentIndex <= 5)
    //    {
    //        ShowPanel();
    //    }
    //}

    private void Update() 
    {
        if (IsActive && Input.GetKeyDown(KeyCode.E))
        {
            HideGmmick();
        }

        if (IsActiveTuto && Input.GetKeyDown(KeyCode.E))
        {
            HideTuto();
        }
    }
}
