using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundByDistance : MonoBehaviour
{
    [SerializeField, Tooltip("Liner: ���������� �۾���, Logarithmic: �ް��ϰ� �۾���")]
    private AudioRolloffMode rolloffMode;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _audioSource.spatialBlend = 1.0f;
        _audioSource.rolloffMode = rolloffMode;
    }
}
