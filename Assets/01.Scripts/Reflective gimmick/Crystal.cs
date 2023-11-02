using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum CrystalParticleType
{
    None,
    Charging,
    OverCharging,
    ChargingFin
}

public class Crystal : MonoBehaviour, IReflectable
{
    Dictionary<CrystalParticleType, ParticleSystem> particlesDic = new();
    [SerializeField]
    private List<ParticleSystem> particles = new List<ParticleSystem>();

    public CrystalParticleType _curParticleType;
    public CrystalParticleType _preParticleType;

    public float curChargingValue; // �׽�Ʈ�� public
    private float maxChargingValue = 500f;
    public float ChargingValue
    {
        get { return curChargingValue; }
        set { curChargingValue = Mathf.Clamp(value, 0, maxChargingValue); }
    }

    private bool isOKeyHeld = false; //������ oŰ �����°�. ���߿� �ٲ����
    private bool isFirst = true;
    private float chargingRate = 1f; // 1�ʿ� �ö󰡴� ��

    private void Start()
    {
        _curParticleType = CrystalParticleType.None;
        _preParticleType = _curParticleType;

        int i = 0;
        foreach(CrystalParticleType e in Enum.GetValues(typeof(CrystalParticleType))) //��ƼŬ ��ųʸ� ����
        {
            if (e == CrystalParticleType.None) continue;
            particlesDic.Add(e, particles[i]);
            i++;
            particlesDic[e].Stop();
        }
    }

    private void Update()
    {
        UpdateCrystalState(); // ���� üũ
        if (_preParticleType != _curParticleType) //���� �ٲ�� ��ƼŬ �ٲ�
        {
            ChangeParticleSystem();
        }

        // 'o' Ű�� ������ ���� �� ChargingValue�� ������Ŵ ����׿��� ����
        if (Keyboard.current.oKey.isPressed)
        {
            isOKeyHeld = true;
        }
        else
        {
            isOKeyHeld = false;
        }

        if (isOKeyHeld) //����׿�
        {
            OnReflected(transform.position, transform.position, transform.position, Color.red);
        }
        _preParticleType = _curParticleType;
    }

    private void ChangeParticleSystem() //��ƼŬ �ٲٴ� �Լ�
    {
        if (_preParticleType != CrystalParticleType.None && _curParticleType != CrystalParticleType.OverCharging) { particlesDic[_preParticleType].Stop(); }
        if (_curParticleType != CrystalParticleType.None) { particlesDic[_curParticleType].Play(); }
    }

    private void UpdateCrystalState() //���� �ٲٴ� �Լ�
    {
        if (ChargingValue == maxChargingValue)
        {
            if(isFirst) //���� ó�� �� �����Ǿ��ٸ� �׳� �� ���� ��ƼŬ �����ϰ� ����������
            {
                particlesDic[CrystalParticleType.ChargingFin].Play();
                isFirst = false;
            }
            _curParticleType = isOKeyHeld ? CrystalParticleType.OverCharging : CrystalParticleType.ChargingFin;
        }
        else
        {
            _curParticleType = isOKeyHeld ? CrystalParticleType.Charging : CrystalParticleType.None;
        }
    }

    public void OnReflected(Vector3 inHitPos, Vector3 inDirection, Vector3 inNormal, Color inColor)
    {
        StartCoroutine(IncreaseChargingValueCoroutine());
    }

    private IEnumerator IncreaseChargingValueCoroutine()
    {
        while (isOKeyHeld && ChargingValue < maxChargingValue)
        {
            ChargingValue += chargingRate;
            yield return new WaitForSeconds(1f);
        }
    }

    public void UnReflected(Vector3 inHitPos, Vector3 inDirection, Vector3 inNormal)
    {
        // �� �ƹ��͵� ����
    }
}
