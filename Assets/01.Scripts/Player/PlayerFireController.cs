using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireController : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private void Awake()
    {
        _inputReader.OnStartFireEvent += OnShootHandler;
        _inputReader.OnStopFireEvent += OnStopHandler;
    }

    private void OnShootHandler()
    {
        SoundManager.Instance.PlaySFXSound("Hum");
    }

    private void OnStopHandler()
    {
        SoundManager.Instance.PauseSFXSound("Hum");
    }

    private void OnDestroy()
    {
        _inputReader.OnStartFireEvent -= OnShootHandler;
        _inputReader.OnStopFireEvent -= OnStopHandler;
    }
}
