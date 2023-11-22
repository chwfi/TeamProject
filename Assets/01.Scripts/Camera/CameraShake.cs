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
        //Virtual Camera���� CinemachineBasicMultiChannelPerlin ����� ������
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
            StartCoroutine(ApplyShake(shakeTime)); //����ũ �ڷ�ƾ
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
            //������ ������ ����
            _noiseModule.m_AmplitudeGain = Mathf.Lerp(startAmplitude, 0f, elapsedTime / shakeTime);
            _noiseModule.m_FrequencyGain = Mathf.Lerp(startFrequency, 0f, elapsedTime / shakeTime);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        //����
        _noiseModule.m_AmplitudeGain = 0;
        _noiseModule.m_FrequencyGain = 0;
    }
}
