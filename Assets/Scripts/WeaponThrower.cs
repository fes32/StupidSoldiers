using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrower : MonoBehaviour
{
	[SerializeField] private Transform _arrow;
    [SerializeField] private PlayerRotater _rotater;
    [SerializeField] private Weapon _weapon;

    [SerializeField] private float _angleMin = 60;
    [SerializeField] private float _angleMax = 300;

    private float _throwForce = 100;
    private float _speedArrow = 2;

    private int _angleModificator = 1;
    private float _t = 0;

    private Quaternion minRotation;
    private Quaternion maxRotation;

    private void Start()
    {
        if (_weapon == null)
        {
            Disactivate();
        }
        minRotation = Quaternion.Euler(_angleMin, 0, 0);
        maxRotation = Quaternion.Euler(_angleMax, 0, 0);
        _arrow.localRotation = Quaternion.Euler(_angleMin, 0, 0);
    }

    private void Update()
    {
       _t = Mathf.PingPong(Time.time * _speedArrow, 1.0f);
        _arrow.localRotation = Quaternion.Lerp(minRotation, maxRotation, _t);

        if (Input.GetMouseButtonDown(0))
        {
            _weapon.SetNonKinematic();
            _weapon.Throw(_arrow.forward * _throwForce, 50);

            _rotater.WeaponThrowed();
            Disactivate();
        }
    }

    private void Disactivate()
    {
        _arrow.gameObject.SetActive(false);
        this.enabled = false;
    }

    public void Activate()
    {
        _arrow.gameObject.SetActive(true);
        _arrow.localRotation = minRotation;
        enabled = true;
    }

    public void UntieWeapon()
    {
        _weapon = null;
        Time.timeScale = 0.6f;
    }

    public void BindWeapon(Weapon weapon)
    {
        _weapon = weapon;
        Time.timeScale = 1f;
    }
}

