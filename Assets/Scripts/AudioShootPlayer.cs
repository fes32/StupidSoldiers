using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioShootPlayer : MonoBehaviour
{
	[SerializeField] private AudioSource _audio;

    public void PlaySound()
    {
        _audio.pitch = Random.Range(0.7f, 1.3f);
        _audio.Play();
    }

}

