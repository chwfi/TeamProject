using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
    
public class ColorDoor : MonoBehaviour
{
    private Animator _animator;
    private CameraShake _cameraShake;

    private readonly int openDoorHash = Animator.StringToHash("OpenDoorTrigger");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _cameraShake = FindObjectOfType<CameraShake>();
    }

    public void OpenDoor()
    {
        Debug.Log("으아아아아아!!!");
        _animator.SetTrigger(openDoorHash);
        _cameraShake.Shake(4.75f);
    }
}
