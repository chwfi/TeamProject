using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

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
    Color newColor;

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
        }
        _mr = transform.Find("Visual").GetComponent<MeshRenderer>();

        _materialPropertyBlock = new MaterialPropertyBlock();
        _colorZero = _materialPropertyBlock.GetColor("_EmissionColor");
        _materialPropertyBlock.SetColor("_EmissionColor", Color.black); //색 초기화.
        _mr.SetPropertyBlock(_materialPropertyBlock);                   //색 초기화.
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            OnHandleReflected();
        }

        UpdateCrystalState(); // 상태 확인
        if (_preParticleType != _curParticleType) // 상태 바뀌면 파티클바꿔서 재생
        {
            ChangeParticleSystem();
        }

        if(_curParticleType == CrystalParticleType.ChargingFin)
        {
            StartCoroutine(FinishParticle());
        }

        _preParticleType = _curParticleType;
    }

    private IEnumerator FinishParticle()
    {
        yield return new WaitForSeconds(3f);
        particlesDic[_preParticleType].Stop();
    }

    private void ChangeParticleSystem() //파티클 재생
    {
        if (_preParticleType != CrystalParticleType.None)
        {
            particlesDic[_preParticleType].Stop();
        }
        if (_curParticleType != CrystalParticleType.None)
        {
            particlesDic[_curParticleType].Play();
        }
    }

    private void UpdateCrystalState() // 상태 업데이트
    {
        if (ChargingValue == maxChargingValue)
        {
            if (_curParticleType != CrystalParticleType.None)
            {
                ChangeParticleSystemColor();
            }

            isCharging = false;
            _curParticleType = CrystalParticleType.ChargingFin;
        }
        else
        {
            if(isCharging)
            {
                _curParticleType = CrystalParticleType.Charging;
            }
            else
            {
                _curParticleType = CrystalParticleType.None;
            }
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
            newColor = Color.Lerp(_colorZero, _targetColor, t);

            _materialPropertyBlock.SetColor("_EmissionColor", newColor);
            _mr.SetPropertyBlock(_materialPropertyBlock);

            if(_curParticleType != CrystalParticleType.None)
            {
                ChangeParticleSystemColor();
            }

            yield return null;
        }
    }

    private void ChangeParticleSystemColor()
    {
        foreach (var p in particlesDic[_curParticleType].
                    transform.GetComponentsInChildren<ParticleSystem>())
        {
            foreach (var cp in p.transform.GetComponentsInChildren<ParticleSystem>())
            {
                var c = cp.colorOverLifetime;
                c.color = newColor;
            }
        }
    }

    
}
