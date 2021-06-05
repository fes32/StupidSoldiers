using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinishZone : MonoBehaviour
{
    public event UnityAction GameEnded;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Weapon>())
            GameEnded?.Invoke();
    }
}