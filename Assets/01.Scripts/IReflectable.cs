
using UnityEngine;

public interface IReflectable
{
    public abstract void SetDataModify(ReflectData data); //������ �޾Ƽ� �������ٰ�
    public abstract void OnHandleReflected();
    public abstract void UnHandleReflected();
}

