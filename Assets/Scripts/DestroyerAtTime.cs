using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerAtTime : MonoBehaviour
{
	[SerializeField] private float _time = 2;

    private void Start()
    {
        Destroy(gameObject, _time);
    }

}

