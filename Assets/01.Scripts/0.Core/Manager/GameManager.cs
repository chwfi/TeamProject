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

    [SerializeField] private InputReader _inputReader;

    protected override void Awake()
    {
        base.Awake();
        _player = PlayerTrm.GetComponent<FirstPersonController>();
    }
    private void Update()
    {
        _inputReader.Update();
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
