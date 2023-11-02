using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Define
{
    public static class Define
    {
        public static LayerMask ReflectionLayer =>
            1 << LayerMask.NameToLayer("ReflectiveObject");

        public static Transform PlayerTrm
        {
            get { return GameObject.FindGameObjectWithTag("Player").transform; }
        }
    }
}

