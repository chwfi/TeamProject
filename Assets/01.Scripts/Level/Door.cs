using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
    
public class ColorDoor : MonoBehaviour
{
    private Animator _animator;
    private CameraShake _cameraShake;
    private List<ParticleSystem> _dustParticles = new List<ParticleSystem>();

    private readonly int openDoorHash = Animator.StringToHash("OpenDoorTrigger");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _cameraShake = FindObjectOfType<CameraShake>();
        _dustParticles.AddRange(GetComponentsInChildren<ParticleSystem>());
    }

    public void OpenDoor()
    {
        _animator.SetTrigger(openDoorHash);
        _dustParticles.ForEach(p => p.Play()); //먼지 파티클 실행해주고
        _cameraShake.Shake(5.75f);
    }
}
