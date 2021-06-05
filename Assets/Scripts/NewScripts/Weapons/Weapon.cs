using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private ParticleSystem _shootEffect;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _shootingMoveForce;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _minRotationSpeedAfterShoot;
    [SerializeField] private float _maxRotationSpeedAfterShoot;
    [SerializeField] private float _normalyRotationSpeed;
    [SerializeField] private float _speedReducedStep;
    [SerializeField] private float _gravityModifire;
    [SerializeField] private AudioClip _shootSound;

    private float _modifireRotation = 1;
    private float _currentRotationSpeed;
    private Rigidbody _rigidbody;

    private const float _laserRenderDistance = 50;
    private const float _forceMoveRight= 80;
    private const float _chanceRotation = 80;

    public event UnityAction _Collided;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _currentRotationSpeed = _rotationSpeed;
        _audioSource.clip = _shootSound;
        _shootEffect.gameObject.SetActive(true);
        _rigidbody.AddForce(Vector3.up * 100);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            Shoot();
        
        Rotate();
        LaserRendering();

        var rotation = Vector3.Cross(transform.right, Vector3.forward);

        print(rotation);
    }

    private void Rotate()
    {
        transform.Rotate(0,0,_currentRotationSpeed* -_modifireRotation*Time.deltaTime);
        _rigidbody.AddForce(new Vector3(0, _gravityModifire, 0));
    }

    private void LaserRendering()
    {
        _lineRenderer.SetPosition(0, _bulletSpawnPoint.position);
        
        if (Physics.Raycast(new Ray(_bulletSpawnPoint.position, transform.right), out RaycastHit hit))
        {
            _lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            _lineRenderer.SetPosition(1, _bulletSpawnPoint.position + transform.right *_laserRenderDistance);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Ground>())
        {
            _Collided?.Invoke();
        }        
    }

    private void Shoot()
    {
        _shootEffect.Play();
        _audioSource.Play();

        var bullet =Instantiate(_bulletPrefab,_bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
        var rotation = Vector3.Cross(transform.right, Vector3.up);
        var horizontRotation = Vector3.Cross(transform.right, Vector3.forward);


        if (rotation.z < 0)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x/2, 0, 0);
            _rigidbody.AddForce(-Vector3.forward * _forceMoveRight);            
        }

        if (horizontRotation.x < 0)
        {
            if (rotation.z > -0.9f & rotation.z <= 0)
            {              
                    _modifireRotation = 1;
            }
            else if (rotation.z < 0.8f & rotation.z > 0)
            {
                if (Random.Range(0, 100) < _chanceRotation)
                    _modifireRotation = -1;
            }
        }


        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, 0);
        _rigidbody.AddForce(-transform.right * _shootingMoveForce);

        _currentRotationSpeed = Random.Range(_minRotationSpeedAfterShoot, _maxRotationSpeedAfterShoot);

        StopCoroutine(ReducedTurnSpeed());
        StartCoroutine(ReducedTurnSpeed());
    }
       
    private IEnumerator ReducedTurnSpeed()
    {
        while (_currentRotationSpeed > _normalyRotationSpeed)
        {
            _currentRotationSpeed = Mathf.MoveTowards(_currentRotationSpeed, _normalyRotationSpeed, _speedReducedStep);
            yield return null;
        }
    }
}