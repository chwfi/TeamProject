using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void LateUpdate()
    {
        if (_target != null)
        {
            transform.position = new Vector3(
                _target.position.x, _target.position.y + 9.5f, _target.position.z);

            //if (_target.rotation.y > -0.75f && _target.rotation.y < 0.75f) //���� �÷��̾ ������ �ٶ󺸰������� �̴ϸ��� ������ ����Ű�� �ݴ�� �ݴ�
            //    _minusValue = -10;
            //else
            //    _minusValue = 10;
        }
    }
}
