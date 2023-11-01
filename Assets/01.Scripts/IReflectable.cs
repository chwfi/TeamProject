
using UnityEngine;

public interface IReflectable
{
    public abstract void OnHandleReflected(Vector3 inHitPos,Vector3 inDirection, Vector3 inNormal,Color inColor);//�Ի纤�Ϳ� ��������, ��
    public abstract void UnHandleReflected(Vector3 inHitPos, Vector3 inDirection, Vector3 inNormal);
    //public abstract void OnReflect(Vector3 direction, Color color); //�߻��� ����� ���� ��
}
