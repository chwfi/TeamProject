using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFX : uint
{
    Page,
    StoneFall,
    MetalDrag,
    Hum,
    Footstep,
}

public enum DistanceState
{
    Inside,
    Outside
}

public enum ReflectState
{
    NULL,
    OnReflect,
    UnReflect,
}

public enum GlowState
{
    NULL,
    OnGlow,
    UnGlow,
}
