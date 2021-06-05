using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotater : MonoBehaviour
{
    [SerializeField] private WeaponThrower _thrower;
    [SerializeField] private Transform _weapon;

    private Quaternion _originRotation;
    private Quaternion _lastRotation;
    private bool _isHaveWeapon;
    private float _lerpPercent;
    private float _lerpSpeed = 2f;

    private void Start()
    {
        _originRotation = transform.rotation;
    }

    public void WeaponCatched()
    {
        _isHaveWeapon = true;
        _lastRotation = transform.rotation;
        _lerpPercent = 0;
    }

    public void WeaponThrowed()
    {
        _isHaveWeapon = false;
    }

    private void Update()
    {
        if (_isHaveWeapon)
        {
            if (_lerpPercent < 1)
            {
                _lerpPercent += Time.deltaTime * _lerpSpeed;
                transform.rotation = Quaternion.Slerp(_lastRotation, _originRotation, _lerpSpeed);
                if (_lerpPercent >= 1)
                {
                    _thrower.Activate();
                }
            }
            
        }
        else
        {
            var pos = _weapon.position - transform.position;
            pos.y = 0;
            transform.forward = Vector3.Slerp(transform.forward, pos.normalized, 5 * Time.deltaTime);
            //transform.forward = pos;
        }
    }


}

