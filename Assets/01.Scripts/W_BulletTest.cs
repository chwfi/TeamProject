using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_BulletTest : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Fire(Vector3 dir)
    {
        _rigidbody.AddForce(dir, ForceMode.Impulse);
        StartCoroutine(DestroyCoroutine());
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != Define.Define.ReflectionLayer) return;

            Vector3.Reflect(transform.position, other.contacts[0].normal);
    }
}
