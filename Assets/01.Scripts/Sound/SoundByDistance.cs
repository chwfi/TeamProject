using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define.Define;
public class SoundByDistance : MonoBehaviour
{
    [SerializeField, Tooltip("Liner: ���������� �۾���, Logarithmic: �ް��ϰ� �۾���")]
    private AudioRolloffMode rolloffMode;
    private AudioSource _audioSource;

    public SFX _sfx;

    private void Start() //ȿ�������� 3d������ ���ش�.
    {
        _audioSource = SoundManager.Instance.GetAudioSource(_sfx.ToString());

        _audioSource.spatialBlend = 1.0f;
        _audioSource.rolloffMode = rolloffMode;
    }
}
