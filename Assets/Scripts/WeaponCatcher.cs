using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponCatcher : MonoBehaviour
{
	[SerializeField] private CatchHandler _handler;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Weapon weapon))
        {
            _handler.CatchWeapon(weapon);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Weapon weapon))
        {
            _handler.RemoveWeapon();
        }
    }


}

