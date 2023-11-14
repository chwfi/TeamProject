using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotatePlate : MonoBehaviour
{
    Camera cam;

    [SerializeField, Tooltip("Outline ��ũ��Ʈ�� ������ �ִ� ������Ʈ�� ���̾�")]
    LayerMask plateLayer;
    [SerializeField, Tooltip("ȸ�� ����")]
    private float rotateSensentive;
    [SerializeField, Tooltip("��� ����")]
    private float grabRange;

    private Outline prevGrapPlateOutline; //outline�� �״� ���� �ϱ� ���� ����

    Vector2 _screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f); // ����ĳ��Ʈ�� ��� ���� ȭ���߾�

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
                if(hit.transform.TryGetComponent(out prevGrapPlateOutline)) //���� �����ٸ� �� ������Ʈ�� outline Ű��
                {
                    prevGrapPlateOutline.enabled = true;
                }

                float XRotation = Input.GetAxis("Mouse X") * rotateSensentive * Time.deltaTime;
                float YRotation = Input.GetAxis("Mouse Y") * rotateSensentive * Time.deltaTime;

                hit.transform.Rotate(Vector3.down, XRotation, Space.World);  //���콺 �Է¿� ���� ������Ʈ ȸ��
                hit.transform.Rotate(Vector3.right, YRotation, Space.World); //���콺 �Է¿� ���� ������Ʈ ȸ��
            }
        }
        else if(Input.GetMouseButtonUp(1)) //��°� ������ outline ����
        {
            if (prevGrapPlateOutline != null)
            {
                Debug.Log("��");
                prevGrapPlateOutline.enabled = false;
                prevGrapPlateOutline = null;
            }
        }
    }
}
