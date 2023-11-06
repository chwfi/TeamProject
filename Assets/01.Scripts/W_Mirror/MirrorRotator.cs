using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Define.Define;

public class MirrorRotator : MonoBehaviour
{
    [SerializeField] private float _ableDistance;
    [SerializeField] private float _rotateSpeed;

    private void Update()
    {
        RotateMirror();
    }

    private void RotateMirror()
    {
        if (Vector3.Distance(transform.position, PlayerTrm.position) < _ableDistance)
        {
            if (Keyboard.current.qKey.isPressed)
                transform.Rotate(new Vector3(0, 1, 0) * _rotateSpeed * Time.deltaTime);

            if (Keyboard.current.eKey.isPressed)
                transform.Rotate(new Vector3(0, -1, 0) * _rotateSpeed * Time.deltaTime);
        }
    }
}
