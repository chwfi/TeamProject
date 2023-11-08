using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void LateUpdate()
    {
        if (_target != null)
        {
            transform.position = new Vector3(_target.position.x,
                10f, _target.position.z + 10f);
        }
    }
}
