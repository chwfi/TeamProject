
using UnityEngine;

public interface IReflectable
{
    public abstract void OnDeflected(ReflectData data); //������ �޾Ƽ� �������ٰ�
    public abstract void OnHandleReflected();
    public abstract void UnHandleReflected();
}

