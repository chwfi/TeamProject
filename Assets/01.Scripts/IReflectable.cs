
using UnityEngine;

public interface IReflectable
{
    public abstract void SetDataModify(ReflectData reflectData); //������ �޾Ƽ� �������ٰ�
    public abstract void OnReflectTypeChanged(ReflectState type);
    public abstract void OnHandleReflected(); //�ѹ��� ����
    public abstract void UnHandleReflected(); //�ݻ簡 �ȵǸ�
}

