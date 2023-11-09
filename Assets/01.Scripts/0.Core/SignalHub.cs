using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ReflectData
{
    public Vector3 hitPos;
    public Vector3 direction;
    public Vector3 normal;
    public Color color;
}
public enum ReflectState
{
    NULL,
    OnReflect,
    UnReflect,
}
public enum GlowState
{
    NULL,
    OnGlow,
    UnGlow,
}
public delegate void ReflectDataChanged(ReflectData data, ReflectState prevType, ReflectState currentTpye);
public delegate void GlowStateChangedHander(GlowState currentTpye);
public delegate void ReflectRegisterHandler(IReflectable reflectObject);
public static class SignalHub
{

}
