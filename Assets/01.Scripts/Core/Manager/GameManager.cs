using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define.Define;
using StarterAssets;

public class GameManager : MonoSingleton<GameManager>
{
    public int CurrentStage = 1;

    private FirstPersonController _controller;

    protected override void Awake()
    {
        base.Awake();
        _controller = PlayerTrm.GetComponent<FirstPersonController>();
    }

    public void StopGameImmediately(bool value)
    {
        if (value)
        {
            _controller.CanMove = false;
            StartCoroutine(SetTimerZero(0.5f));
        }
        else
        {
            _controller.CanMove = true;
            Time.timeScale = 1;
        }
    }

    private IEnumerator SetTimerZero(float delay)
    {
        yield return new WaitForSeconds(delay);
        Time.timeScale = 0;
    }
}
