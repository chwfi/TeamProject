using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public abstract class Glow : LightingBehaviour, IGlowable
{
    public abstract void OnPickUp();
    public abstract void OnPutDown();
    public virtual void OnStartShootLight()
    {
        SetLightColor(defaultColor);

        var afterEffects = GameObject.FindObjectsOfType<LightAfterEffect>();

        for (int i = 0; i < afterEffects.Length; ++i)
        {
            Destroy(afterEffects[i].gameObject);
        }

        StartDrawAndFadeLine();
    }
    public virtual void OnStopShootLight()
    {
        StopDrawAndFadeLine(true);
    }
    public virtual void OnShootingLight()
    {

    }

    public virtual void StartShootLight(Vector3 origin, Vector3 direction)
    {
        _startPos = origin;

        myReflectData.hitPos = origin;
        myReflectData.direction = direction;
    }

    public abstract void ShootLightSetting();



}