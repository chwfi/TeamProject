using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum CrystalParticleType
{
    None,
    Charging,
    ChargingFin
}

public class Crystal : Reflective
{
    Dictionary<CrystalParticleType, ParticleSystem> particlesDic = new();
    [SerializeField]
    private List<ParticleSystem> particles = new List<ParticleSystem>();

    private CrystalParticleType _curParticleType; 
    private CrystalParticleType _preParticleType;

    public float curChargingValue; // �׽�Ʈ�� public
    [SerializeField]
    private float maxChargingValue = 10f;
    public float ChargingValue
    {
        get { return curChargingValue; }
        set { curChargingValue = Mathf.Clamp(value, 0, maxChargingValue); }
    }

    private bool isCharging = false; //������ oŰ �����°�. ���߿� �ٲ���� //����ó�ٲٱ�
    private bool isFirst = true;
    private float chargingRate = 1f; // 1�ʿ� �ö󰡴� ��

    private void Start()
    {
        _curParticleType = CrystalParticleType.None;
        _preParticleType = _curParticleType;

        int i = 0;
        foreach (CrystalParticleType e in Enum.GetValues(typeof(CrystalParticleType))) //��ƼŬ ��ųʸ� ����
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

        _preParticleType = _curParticleType;
    }
    private void ChangeParticleSystem() //��ƼŬ �ٲٴ� �Լ�
    {
        if (_preParticleType != CrystalParticleType.None) { particlesDic[_preParticleType].Stop(); }
        if (_curParticleType != CrystalParticleType.None) { particlesDic[_curParticleType].Play(); }
    }

    private void UpdateCrystalState() //���� �ٲٴ� �Լ�
    {
        if (ChargingValue == maxChargingValue)
        {
            isCharging = false;
            _curParticleType = CrystalParticleType.ChargingFin;
        }
        else
        {
            _curParticleType = isCharging ? CrystalParticleType.Charging : CrystalParticleType.None;
        }
    }

    public override void SetDataModify(ReflectData inData)
    {
        OnShootRaycast(inData, transform.forward);
    }
    public override void OnHandleReflected()
    {
        base.OnHandleReflected();
        isCharging = true;
        StartCoroutine(IncreaseChargingValueCoroutine());
    }
    public override void UnHandleReflected()
    {
        base.UnHandleReflected();
        isCharging = false;
    }
    private IEnumerator IncreaseChargingValueCoroutine()
    {
        while (isCharging && ChargingValue < maxChargingValue)
        {
            ChargingValue += chargingRate;
            yield return new WaitForSeconds(1f);
        }
    }
}
