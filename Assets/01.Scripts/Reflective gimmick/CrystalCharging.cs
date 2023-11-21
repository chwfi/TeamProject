using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CrystalParticleType
{
    None,
    Charging,
    ChargingFin
}
public class CrystalCharging : Reflective
{
    Dictionary<CrystalParticleType, ParticleSystem> particlesDic = new(); // 상태에 따른 파티클딕셔너리

    [SerializeField]
    private List<ParticleSystem> particles = new List<ParticleSystem>(); // 파티클들

    private CrystalParticleType _curParticleType;
    private CrystalParticleType _preParticleType;

    private MaterialPropertyBlock _materialPropertyBlock; // 크리스탈 색 적용할 머티리얼
    private MeshRenderer _mr; // 크리스탈의 메시렌더러
    [SerializeField]
    private Color _targetColor; //이 크리스탈이 바뀔 색
    private Color _colorZero;
    Color _newColor; // Color.Lerp를 통해 서서희 바꿀 목표 색

    private float timer = 0f;

    private float curChargingValue; // 현재 색차지 값
    public float maxChargingValue = 5f;
    public float ChargingValue // 색차지 값프로퍼티
    {
        get { return curChargingValue; }
        set { curChargingValue = Mathf.Clamp(value, 0, maxChargingValue); }
    }

    public bool CanUse = false; //사용가능한가
    private bool isCharging = false; //현재 차징중인가

    protected override void Start()
    {
        _curParticleType = CrystalParticleType.None; // 상태 초기화
        _preParticleType = _curParticleType;         // 상태 초기화

        int i = 0;
        foreach (CrystalParticleType e in Enum.GetValues(typeof(CrystalParticleType))) // 딕셔너리에 상태에 맞는 파티클 삽입
        {
            if (e == CrystalParticleType.None) continue;
            particlesDic.Add(e, particles[i]);
            i++;
        }
        _mr = GetComponent<MeshRenderer>();

        _materialPropertyBlock = new MaterialPropertyBlock();

        _colorZero = _materialPropertyBlock.GetColor("_EmissionColor");
        _materialPropertyBlock.SetColor("_EmissionColor", Color.black); //색 초기화.
        _mr.SetPropertyBlock(_materialPropertyBlock);                   //색 초기화.
    }

    public override void OnHandleReflected()
    {
        isCharging = true;

        SoundManager.Instance.PlaySFXSound(SFX.CrystalCharging);
    }
    public override void UnHandleReflected()
    {
        isCharging = false;
        SoundManager.Instance.PauseSFXSound(SFX.CrystalCharging);

        if (_curParticleType != CrystalParticleType.ChargingFin)
        {
            ChargingValue = 0;
            timer = 0;
            _newColor = _colorZero;
            _materialPropertyBlock.SetColor("_EmissionColor", _colorZero);
            _mr.SetPropertyBlock(_materialPropertyBlock);
        }


    }
    private void Update()
    {
        UpdateCrystalState(); // 상태 확인
        if (_preParticleType != _curParticleType) // 상태 바뀌면 파티클바꿔서 재생
        {
            ChangeParticleSystem();
        }

        if (_curParticleType == CrystalParticleType.ChargingFin) // 다 채워지면 재생 끝
        {
            StartCoroutine(FinishParticle());
        }

        _preParticleType = _curParticleType;

        CheckCharging();
    }
    public void CheckCharging()
    {
        if (CanUse == true) { return; }
        {
            if (isCharging == true)
            {
                if (ChargingValue <= maxChargingValue)
                {
                    timer += Time.deltaTime;
                    float t = Mathf.Clamp01(timer / maxChargingValue);

                    ChargingValue = Mathf.Lerp(0f, maxChargingValue, t);
                    _newColor = Color.Lerp(_colorZero, _targetColor, t);

                    _materialPropertyBlock.SetColor("_EmissionColor", _newColor);
                    _mr.SetPropertyBlock(_materialPropertyBlock);

                    if (_curParticleType != CrystalParticleType.None)
                    {
                        ChangeParticleSystemColor();
                    }
                }
            }
        }
    }

    #region 차징 코드

    private IEnumerator FinishParticle()
    {
        yield return new WaitForSeconds(1f);
        particlesDic[_preParticleType].Stop();

        CanUse = true;
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
        if (ChargingValue >= maxChargingValue)
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
            if (isCharging)
            {
                _curParticleType = CrystalParticleType.Charging;
            }
            else
            {
                _curParticleType = CrystalParticleType.None;
            }
        }
    }
    private void ChangeParticleSystemColor() // 색 바꾼다.
    {
        foreach (var p in particlesDic[_curParticleType].
                    transform.GetComponentsInChildren<ParticleSystem>())
        {
            foreach (var cp in p.transform.GetComponentsInChildren<ParticleSystem>())
            {
                var c = cp.colorOverLifetime;
                c.color = _newColor;
            }
        }
    }

    public override void GetReflectedObjectDataModify(ReflectData reflectedData)
    {
        
    }
    #endregion
}
