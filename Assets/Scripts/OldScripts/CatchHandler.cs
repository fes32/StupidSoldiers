using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchHandler : MonoBehaviour
{
	[SerializeField] private WeaponThrower _thrower;
    [SerializeField] private PlayerRotater _playerRotater;
    [SerializeField] private Transform _weaponPoint;
    [SerializeField] private Emojies _emojies;

    public void CatchWeapon(OldWeapon weapon)
    {
        weapon.SetKinematic();
        weapon.SetTargetPoint(_weaponPoint);
        _thrower.BindWeapon(weapon);
        _playerRotater.WeaponCatched();
        _emojies.PlaySmile();
    }

    public void RemoveWeapon()
    {
        _thrower.UntieWeapon();
    }

}

