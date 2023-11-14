using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotatePlate : MonoBehaviour
{
    Camera cam;

    [SerializeField, Tooltip("Outline 스크립트를 가지고 있는 오브젝트의 레이어")]
    LayerMask plateLayer;
    [SerializeField, Tooltip("회전 감도")]
    private float rotateSensentive;
    [SerializeField, Tooltip("잡는 길이")]
    private float grabRange;

    private Outline prevGrapPlateOutline; //outline을 켰다 껐다 하기 위한 변수

    Vector2 _screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f); // 레이캐스트를 쏘기 위한 화면중앙

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if(Input.GetMouseButton(1))
        {
            if (Physics.Raycast(cam.ScreenPointToRay(_screenCenterPoint), out RaycastHit hit, grabRange, plateLayer))
            {
                if(hit.transform.TryGetComponent(out prevGrapPlateOutline)) //만약 잡혔다면 그 오브젝트의 outline 키기
                {
                    prevGrapPlateOutline.enabled = true;
                }

                float XRotation = Input.GetAxis("Mouse X") * rotateSensentive * Time.deltaTime;
                float YRotation = Input.GetAxis("Mouse Y") * rotateSensentive * Time.deltaTime;

                hit.transform.Rotate(Vector3.down, XRotation, Space.World);  //마우스 입력에 따른 오브젝트 회전
                hit.transform.Rotate(Vector3.right, YRotation, Space.World); //마우스 입력에 따른 오브젝트 회전
            }
        }
        else if(Input.GetMouseButtonUp(1)) //잡는걸 놓으면 outline 끄기
        {
            if (prevGrapPlateOutline != null)
            {
                Debug.Log("끔");
                prevGrapPlateOutline.enabled = false;
                prevGrapPlateOutline = null;
            }
        }
    }
}
