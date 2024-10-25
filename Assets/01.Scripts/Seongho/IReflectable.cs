
using UnityEngine;

public interface IReflectable
{
    public abstract void OnDeflected(ReflectData data); //정보를 받아서 수정해줄거
    public abstract void OnHandleReflected();
    public abstract void UnHandleReflected();
}

