using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define.Define;
public class SoundByDistance : MonoBehaviour
{
    [SerializeField, Tooltip("Liner: 점진적으로 작아짐, Logarithmic: 급격하게 작아짐")]
    private AudioRolloffMode rolloffMode;
    private AudioSource _audioSource;

    public SFX _sfx;

    private void Start() //효과음들은 3d음향을 켜준다.
    {
        _audioSource = SoundManager.Instance.GetAudioSource(_sfx.ToString());

        _audioSource.spatialBlend = 1.0f;
        _audioSource.rolloffMode = rolloffMode;
    }
}
