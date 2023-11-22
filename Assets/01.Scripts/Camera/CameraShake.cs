using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    private CinemachineBasicMultiChannelPerlin _noiseModule;

    [SerializeField] private float _amplitude = 0.5f;
    [SerializeField] private float _frequency = 1.0f;

    private void Start()
    {
        //Virtual Camera에서 CinemachineBasicMultiChannelPerlin 모듈을 가져옴
        if (_virtualCamera != null)
        {
            _noiseModule = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        Shake(.1f);
    }

    public void Shake(float shakeTime)
    {
        if (_noiseModule != null)
        {
            StartCoroutine(ApplyShake(shakeTime)); //쉐이크 코루틴
        }
    }

    private IEnumerator ApplyShake(float shakeTime)
    {
        _noiseModule.m_AmplitudeGain = _amplitude;
        _noiseModule.m_FrequencyGain = _frequency;

        float elapsedTime = 0f;
        float startAmplitude = _amplitude;
        float startFrequency = _frequency;

        while (elapsedTime < shakeTime)
        {
            //서서히 노이즈 해제
            _noiseModule.m_AmplitudeGain = Mathf.Lerp(startAmplitude, 0f, elapsedTime / shakeTime);
            _noiseModule.m_FrequencyGain = Mathf.Lerp(startFrequency, 0f, elapsedTime / shakeTime);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        //최종
        _noiseModule.m_AmplitudeGain = 0;
        _noiseModule.m_FrequencyGain = 0;
    }
}
