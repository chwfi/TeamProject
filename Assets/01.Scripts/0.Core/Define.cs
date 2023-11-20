using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Define
{
    public static class Define
    {
        public static LayerMask ReflectionLayer =>
                 LayerMask.GetMask("ReflectiveObject", "DoorCrystal");

        public static Transform PlayerTrm
        {
            get { return GameObject.FindGameObjectWithTag("Player").transform; }
        }

        public static Camera MainCam => Camera.main;
    }
}
