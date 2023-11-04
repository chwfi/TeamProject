
using UnityEngine;

public interface IReflectable
{
    public abstract void SetDataModify(ReflectData reflectData); //정보를 받아서 수정해줄거
    public abstract void OnReflectTypeChanged(ReflectState type);
}

