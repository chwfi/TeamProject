
using UnityEngine;

public interface IReflectable
{
    public abstract void SetDataModify(ReflectData reflectData); //������ �޾Ƽ� �������ٰ�
    public abstract void OnReflectTypeChanged(ReflectState type);
}

