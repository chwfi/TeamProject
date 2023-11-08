using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static Define.Define;

[RequireComponent(typeof(LineRenderer))]
public abstract class Glow : LightingBehaviour, IGlowable
{
    public virtual void OnStartShootLight()
    {
        lb.enabled = true;

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }
    public virtual void OnStopShootLight()
    {
        elapsedTime = 0;
        startTime = 0;
        raycastDistance = 0;

        coroutine = StartCoroutine(DrawAndFadeLineCoroutine());
    }

    public void OnShootingLight()
    {
        
    }
}