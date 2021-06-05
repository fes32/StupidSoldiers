using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestructibleObject : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private int _reward;

    public event UnityAction<DestructibleObject, int> Destroyed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>())
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        GetComponent<BoxCollider>().enabled=false;
        Destroyed?.Invoke(this,_reward);
        _explosion.gameObject.SetActive(true);
        _animator.Play("Destroy");
    }

}