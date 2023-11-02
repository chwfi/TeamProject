
using UnityEngine;

public interface IReflectable
{
    public abstract void DataModify(ReflectData reflectData); //정보를 받아서 수정해줄거
    public abstract void OnReflectTypeChanged(ReflectState type);
    public abstract void OnHandleReflected(); //한번만 실행
    public abstract void UnHandleReflected(); //반사가 안되면
}

