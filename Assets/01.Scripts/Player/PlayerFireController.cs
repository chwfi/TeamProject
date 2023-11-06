using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireController : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private void Awake()
    {
        //_inputReader.FireEvent += OnShootHandler;
    }

    private void OnShootHandler(bool value)
    {
        //_lantern.OnShootLight(value);
    }

    private void OnDestroy()
    {
        //_inputReader.FireEvent -= OnShootHandler;
    }
}
