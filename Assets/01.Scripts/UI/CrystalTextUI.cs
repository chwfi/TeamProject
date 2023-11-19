using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using static Define.Define;

public enum CatchState
{
    None,
    Possible,
    Impossible,
}
public class CrystalTextUI : LookCamUIModule
{
    [SerializeField] private float _ShowUIdistance = 5f;

    private Crystal crystal;

    private TextMeshPro _text;
    private bool _canCatch =>
        Vector3.Distance(transform.position, PlayerTrm.position) < _ShowUIdistance 
        /*&& crystal.CanUse*/;

    private CatchState _currentState;
    public CatchState State
    {
        get => _currentState;
        set
        {
            if (_currentState == value) return;

            _currentState = value;

            if (_currentState == CatchState.Possible)
            {
                Fade(1, 0.4f);
            }
            else if (_currentState == CatchState.Impossible)
            {
                Fade(0, 0.4f);
            }
        }
    }
    protected override void Awake()
    {
        _text = GetComponent<TextMeshPro>();

        crystal = transform.root.GetComponent<Crystal>();
    }

    protected override void Update()
    {
        CheckState();
    }

    private void CheckState()
    {
        if (_canCatch)
            _currentState = CatchState.Possible;
        else
            _currentState = CatchState.Impossible;
    }

    public void Fade(float value, float time)
    {
        _text.DOFade(value, time);
    }
}
