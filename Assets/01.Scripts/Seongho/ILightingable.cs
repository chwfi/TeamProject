using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILightingable
{
    public abstract void DataModify(ReflectData reflectData);
    public abstract void ChangedReflectObject(Reflective reflectable);
}
