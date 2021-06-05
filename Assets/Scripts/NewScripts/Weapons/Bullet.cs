using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private ParticleSystem _destroyEffect;

    private Rigidbody _rigidbody;
    private Vector3 _direction;

    private const float _lifeTime = 1.5f;
    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = transform.right * _speed;

        Destroy(this.gameObject, _lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
            Destroy(this.gameObject);
    }
}