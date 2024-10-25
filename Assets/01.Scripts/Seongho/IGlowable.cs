using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IGlowable
{
    public void OnStartShootLight();
    public void OnStopShootLight();
    public void OnShootingLight();
    public void ShootLightSetting();
}
