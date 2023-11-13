using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [Header("TutorialUI")]
    public TutorialPanelUI tutorialPanelUI;
    [Header("SettingUI")]
    public SettingUI settingUI;
    public SettingMouseSenseValue settingMouseUI;
    public SettingRotationValue settingRotUI;

    bool _isSettingShown;

    protected override void Awake()
    {
        base.Awake();
        tutorialPanelUI = FindObjectOfType<TutorialPanelUI>();
        settingUI = FindObjectOfType<SettingUI>();
        settingMouseUI = FindObjectOfType<SettingMouseSenseValue>();
        settingRotUI = FindObjectOfType<SettingRotationValue>();
    }

    public void FadePanel(float value, float time, string contents, Image image, Image previousImage)
    {
        tutorialPanelUI.Fade(value, time, contents, image, previousImage);
    }

    //세팅창 로직
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (_isSettingShown)
            {
                HideSettingPanel();
            }
            else
            {
                settingUI.Fade(1, 0.5f);
                SoundManager.Instance.PlaySFXSound("Page");
                GameManager.Instance.StopGameImmediately(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                _isSettingShown = true;
            }
        }
    }

    public void HideSettingPanel()
    {
        settingUI.Fade(0, 0.5f);
        SoundManager.Instance.PlaySFXSound("Page");
        GameManager.Instance.StopGameImmediately(false);
        Cursor.visible = false;
        _isSettingShown = false;
    }
}
