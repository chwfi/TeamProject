using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ReflectData //��������Ʈ�� ���� ����
{
    Vector3 inHitPos;
    Vector3 inDirection;
    Vector3 normal;
    Color inColor;
}

public class EventNode
{
    public ReflectData data { get; set; }
    EventNode node { get; set; }


}

public delegate void ReflectChanged();
public static class SignalHub
{

}
