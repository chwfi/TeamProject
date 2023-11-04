using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class UIManager : MonoSingleton<UIManager>
{
    public TutorialPanelUI tutorialPanelUI;
    public SettingUI settingUI;
    public SettingMouseSenseValue settingMouseUI;

    bool _isSettingShown;

    protected override void Awake()
    {
        base.Awake();
        tutorialPanelUI = FindObjectOfType<TutorialPanelUI>();
        settingUI = FindObjectOfType<SettingUI>();
        settingMouseUI = FindObjectOfType<SettingMouseSenseValue>();
    }

    public void FadePanel(float value, float time, string contents, Sprite sprite)
    {
        tutorialPanelUI.Fade(value, time, contents, sprite);
    }

    //����â ����
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
