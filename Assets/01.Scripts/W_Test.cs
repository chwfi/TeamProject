using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Test : MonoBehaviour
{
    [SerializeField] private W_BulletTest _bulletPrefab;
    [SerializeField] private Transform _fireTrm;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            W_BulletTest bullet = Instantiate(_bulletPrefab, _fireTrm.position, Quaternion.identity);
            bullet.Fire(_fireTrm.forward * 100f);
        }
    }
}
