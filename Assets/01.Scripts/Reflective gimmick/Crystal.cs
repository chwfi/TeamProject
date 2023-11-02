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

    public float curChargingValue; // 테스트용 public
    private float maxChargingValue = 500f;
    public float ChargingValue
    {
        get { return curChargingValue; }
        set { curChargingValue = Mathf.Clamp(value, 0, maxChargingValue); }
    }

    private bool isOKeyHeld = false; //지금은 o키 누르는거. 나중에 바꿔야함
    private bool isFirst = true;
    private float chargingRate = 1f; // 1초에 올라가는 양

    private void Start()
    {
        _curParticleType = CrystalParticleType.None;
        _preParticleType = _curParticleType;

        int i = 0;
        foreach(CrystalParticleType e in Enum.GetValues(typeof(CrystalParticleType))) //파티클 딕셔너리 세팅
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

        // 'o' 키를 누르고 있을 때 ChargingValue를 증가시킴 디버그용임 ㅋㅋ
        if (Keyboard.current.oKey.isPressed)
        {
            isOKeyHeld = true;
        }
        else
        {
            isOKeyHeld = false;
        }

        if (isOKeyHeld) //디버그용
        {
            OnReflected(transform.position, transform.position, transform.position, Color.red);
        }
        _preParticleType = _curParticleType;
    }

    private void ChangeParticleSystem() //파티클 바꾸는 함수
    {
        if (_preParticleType != CrystalParticleType.None && _curParticleType != CrystalParticleType.OverCharging) { particlesDic[_preParticleType].Stop(); }
        if (_curParticleType != CrystalParticleType.None) { particlesDic[_curParticleType].Play(); }
    }

    private void UpdateCrystalState() //상태 바꾸는 함수
    {
        if (ChargingValue == maxChargingValue)
        {
            if(isFirst) //만약 처음 다 충전되었다면 그냥 다 찬거 파티클 실행하고 오버차지도
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
        // 앙 아무것도 업음
    }
}
