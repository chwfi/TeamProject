using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

[System.Serializable]
struct TutorialInfo
{
    [TextArea] public string Description;
    public Sprite Sprite;
}

public class TutorialManager : MonoSingleton<TutorialManager>
{
    [SerializeField] private List<TutorialInfo> tutorialInfo;

    private int _showOrder => GameManager.Instance.CurrentStage - 1;

    bool _canShowPanel = true;
    bool _isShown = false;

    private void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            UIManager.Instance.FadePanel(1f, 0.5f,
                tutorialInfo[_showOrder].Description,
                tutorialInfo[_showOrder].Sprite);
            _isShown = true;
        }
        
        if (Keyboard.current.eKey.wasPressedThisFrame && _isShown)
        {
            UIManager.Instance.FadePanel(0f, 0.5f,
                tutorialInfo[_showOrder].Description,
                tutorialInfo[_showOrder].Sprite);
        }
    }
}
