using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotatePlate : MonoBehaviour
{
    Camera cam;

    [SerializeField]
    LayerMask plateLayer;
    [SerializeField]
    private float rotateSensentive;

    Vector2 _screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if(Input.GetMouseButton(1))
        {
            if (Physics.Raycast(cam.ScreenPointToRay(_screenCenterPoint), out RaycastHit hit, 10f, plateLayer))
            {
                Debug.Log("dasdsa");
                float XRotation = Input.GetAxis("Mouse X") * rotateSensentive * Time.deltaTime;
                float YRotation = Input.GetAxis("Mouse Y") * rotateSensentive * Time.deltaTime;

                hit.transform.Rotate(Vector3.down, XRotation);
                hit.transform.Rotate(Vector3.right, YRotation);
            }
        }
    }
}
