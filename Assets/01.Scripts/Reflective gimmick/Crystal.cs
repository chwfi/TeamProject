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

    public float curChargingValue; // 테스트용 public
    [SerializeField]
    private float maxChargingValue = 10f;
    public float ChargingValue
    {
        get { return curChargingValue; }
        set { curChargingValue = Mathf.Clamp(value, 0, maxChargingValue); }
    }

    private bool isCharging = false; //지금은 o키 누르는거. 나중에 바꿔야함 //지금처바꾸기
    private bool isFirst = true;
    private float chargingRate = 1f; // 1초에 올라가는 양

    private void Start()
    {
        _curParticleType = CrystalParticleType.None;
        _preParticleType = _curParticleType;

        int i = 0;
        foreach (CrystalParticleType e in Enum.GetValues(typeof(CrystalParticleType))) //파티클 딕셔너리 세팅
        {
            if (e == CrystalParticleType.None) continue;
            particlesDic.Add(e, particles[i]);
            i++;
            particlesDic[e].Stop();
        }
    }

    private void Update()
    {
        UpdateCrystalState(); // 상태 체크
        if (_preParticleType != _curParticleType) //상태 바뀌면 파티클 바꿈
        {
            ChangeParticleSystem();
        }

        _preParticleType = _curParticleType;
    }
    private void ChangeParticleSystem() //파티클 바꾸는 함수
    {
        if (_preParticleType != CrystalParticleType.None) { particlesDic[_preParticleType].Stop(); }
        if (_curParticleType != CrystalParticleType.None) { particlesDic[_curParticleType].Play(); }
    }

    private void UpdateCrystalState() //상태 바꾸는 함수
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
