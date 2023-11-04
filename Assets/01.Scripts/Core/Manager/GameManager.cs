using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define.Define;
using StarterAssets;

public class GameManager : MonoSingleton<GameManager>
{
    public int CurrentStage = 1;

    private FirstPersonController _player;
    public FirstPersonController Player => _player;

    protected override void Awake()
    {
        base.Awake();
        _player = PlayerTrm.GetComponent<FirstPersonController>();
    }

    public void StopGameImmediately(bool value) //�÷��̾��� ������, ȭ�� ��ȯ ��� ����
    {
        if (value)
        {
            _player.CanMove = false;
        }
        else
        {
            _player.CanMove = true;
        }
    }
}
