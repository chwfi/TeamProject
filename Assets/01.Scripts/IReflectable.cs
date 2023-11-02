
using UnityEngine;

public interface IReflectable
{
    public abstract void OnHandleReflected(Vector3 inHitPos,Vector3 inDirection, Vector3 inNormal,Color inColor);//입사벡터와 법선벡터, 색
    public abstract void UnHandleReflected(Vector3 inHitPos, Vector3 inDirection, Vector3 inNormal);
    //public abstract void OnReflect(Vector3 direction, Color color); //발사할 방향과 나의 색
}
