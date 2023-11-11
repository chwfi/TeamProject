
using UnityEngine;

public interface IReflectable
{
    public abstract void GetReflectedObjectDataModify(ReflectData data); //������ �޾Ƽ� �������ٰ�
    public abstract void OnHandleReflected();
    public abstract void UnHandleReflected();
}

