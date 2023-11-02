using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Define
{
    public static class Define
    {
        public static LayerMask ReflectionLayer =>
            1 << LayerMask.NameToLayer("ReflectiveObject");
    }
}

