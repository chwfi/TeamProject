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

            //if (_target.rotation.y > -0.75f && _target.rotation.y < 0.75f) //만약 플레이어가 앞쪽을 바라보고있으면 미니맵은 앞쪽을 가리키고 반대면 반대
            //    _minusValue = -10;
            //else
            //    _minusValue = 10;
        }
    }
}
