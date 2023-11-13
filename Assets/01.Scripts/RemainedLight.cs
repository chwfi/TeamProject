using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainedLight : MonoBehaviour
{
    private TrailRenderer _trail;

    private void Awake()
    {
        _trail = GetComponent<TrailRenderer>();
        _trail.enabled = false;
    }

    public void SetPosition(Vector3 pos)
    { 
        _trail.enabled = true;
        _trail.emitting = true;
        transform.position = pos;
    }

    public void ClearTrail()
    {
        _trail.Clear();
        _trail.emitting = false;
    }
}
