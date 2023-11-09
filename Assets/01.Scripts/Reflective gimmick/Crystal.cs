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

    private MaterialPropertyBlock _materialPropertyBlock;
    private MeshRenderer _mr;
    [SerializeField]
    private Color _targetColor;
    private Color _colorZero;

    private float curChargingValue;
    private float maxChargingValue = 5f;
    public float ChargingValue
    {
        get { return curChargingValue; }
        set { curChargingValue = Mathf.Clamp(value, 0, maxChargingValue); }
    }

    private bool isCharging = false;

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
        _mr = transform.Find("Visual").GetComponent<MeshRenderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
        _colorZero = _materialPropertyBlock.GetColor("_EmissionColor");

        _materialPropertyBlock.SetColor("_EmissionColor", Color.black); //색 초기화.
        _mr.SetPropertyBlock(_materialPropertyBlock);                   //색 초기화.
    }

    private void Update()
    {
        UpdateCrystalState(); // 상태 확인
        if (_preParticleType != _curParticleType) // 상태 바뀌면 파티클바꿔서 재생
        {
            ChangeParticleSystem();
        }

        _preParticleType = _curParticleType;
    }

    private void ChangeParticleSystem() //파티클 재생
    {
        if (_preParticleType != CrystalParticleType.None) { particlesDic[_preParticleType].Stop(); }
        if (_curParticleType != CrystalParticleType.None) { particlesDic[_curParticleType].Play(); }
    }

    private void UpdateCrystalState() // 상태 업데이트
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
    private IEnumerator IncreaseChargingValueCoroutine() // 크리스탈 색 조정 및 차징
    {
        float elapsedTime = 0f;

        while (isCharging && ChargingValue <= maxChargingValue)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / maxChargingValue);

            ChargingValue = Mathf.Lerp(0f, maxChargingValue, t);
            Color newColor = Color.Lerp(_colorZero, _targetColor, t);

            _materialPropertyBlock.SetColor("_EmissionColor", newColor);
            _mr.SetPropertyBlock(_materialPropertyBlock);

            yield return null;
        }
    }
}
