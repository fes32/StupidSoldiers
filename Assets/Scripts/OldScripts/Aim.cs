using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private GameObject _visualObject;
    [SerializeField] private ParticleSystem _effects;
    [SerializeField] private Collider _collider;
    [SerializeField] private AudioSource _audio;

    private float _t;
    private float _speed = 0.5f;
    private Vector3 upVector;
    private Vector3 downVector;

    private void Start()
    {
        _speed += Random.Range(-0.1f, 0.1f);
        downVector = transform.position;
        upVector = transform.position + Vector3.up * 0.3f;
    }

    private void Update()
    {
        _t = Mathf.PingPong(Time.time * _speed, 1.0f);
        transform.position = Vector3.Lerp(downVector, upVector, _t);
    }

    public void Hit()
    {
        _collider.enabled = false;
        _visualObject.SetActive(false);
        _effects.Play();
        Destroy(gameObject, 2);
        _audio.Play();
    }
}

