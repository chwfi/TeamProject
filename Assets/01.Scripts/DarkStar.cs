using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkStar : MonoBehaviour
{
    [SerializeField]
    private GameObject testTimeline;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            testTimeline.gameObject.SetActive(true);
        }
    }
}
