using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
    
public class ColorDoor : MonoBehaviour
{
    Animator _animator;

    private readonly int openDoorHash = Animator.StringToHash("OpenDoorTrigger");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        _animator.SetTrigger(openDoorHash);
    }
}
