using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ReflectData //��������Ʈ�� ���� ����
{
   public  Vector3 inHitPos;
    public Vector3 inDirection;
    public Vector3 normal;
    public Color inColor;
}
public enum ReflectState
{
    OnReflect,
    UnReflect,
}
public delegate void ReflectDataChanged(ReflectData data, ReflectState prevType, ReflectState currentTpye);
public delegate void ReflectStateChangedHander(ReflectState prevType, ReflectState currentTpye);
public delegate void ReflectRegisterHandler(IReflectable reflectObject);
public static class SignalHub
{

}
