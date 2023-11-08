using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IGlowable
{
    public abstract void OnStartShootLight();
    public abstract void OnStopShootLight();
    public abstract void OnShootingLight();
}
