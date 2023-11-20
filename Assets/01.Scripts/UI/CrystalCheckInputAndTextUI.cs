using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using static Define.Define;

public enum CatchState
{
    None,
    Possible,
    Impossible,
}
public class CrystalCheckInputAndTextUI : LookCamUIModule
{
    [SerializeField] private float _ShowUIdistance = 5f;
    [SerializeField] private UnityEvent OnPickUpEvent = null;
    private CrystalCharging crystal;

    private TextMeshPro _text;
    public bool CanCatch =>
        Vector3.Distance(transform.position, PlayerTrm.position) < _ShowUIdistance
        && crystal.CanUse;

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

    private Vector3[] SetTransform = //포지션
    {
        new Vector3(0,-0.5f,0.3f),
        new Vector3(-90f,20 ,0),
        new Vector3(25,25,25),
    };
    protected override void Awake()
    {
        _text = GetComponent<TextMeshPro>();

        crystal = transform.root.Find("Glass").GetComponent<CrystalCharging>();
    }

    protected override void Update()
    {
        base.Update();

        CheckState();
        CheckInput();
    }

    private void CheckInput()
    {
        if (CanCatch == false) { return; }

        if (Input.GetKeyDown(KeyCode.F))
        {
            OnPickUpEvent?.Invoke();
            gameObject.SetActive(false); //숨기기
        }
    }

    private void CheckState()
    {
        if (CanCatch)
            State = CatchState.Possible;
        else
            State = CatchState.Impossible;
    }

    public void Fade(float value, float time)
    {
        _text.DOFade(value, time);
    }
}
