using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_DoorTest : MonoBehaviour
{
    [SerializeField]
    DoorOpenTrigger _doorOpenTrigger;

    [SerializeField]
    Crystal _crystal;

    [SerializeField]
    private Color curColor;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            _crystal.OnHandleReflected();
        }
    }
}
