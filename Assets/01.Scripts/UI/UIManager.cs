using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public TutorialPanelUI tutorialPanelUI;

    protected override void Awake()
    {
        base.Awake();
        tutorialPanelUI = FindObjectOfType<TutorialPanelUI>();
    }

    public void FadePanel(float value, float time, string contents, Sprite sprite)
    {
        tutorialPanelUI.Fade(value, time, contents, sprite);
    }
}
