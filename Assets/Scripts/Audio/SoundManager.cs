using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    public static AudioClip shoot;
    public static AudioClip explosion;

    void Awake()
    {
        //load sound effects
        audioSource = gameObject.GetComponent<AudioSource>();

        shoot = Resources.Load<AudioClip>("Shoot");
        explosion = Resources.Load<AudioClip>("Explosion");
    }

    public void PlayShoot()
    {
        audioSource.PlayOneShot(shoot);
    }

    public void PlayExplosion()
    {
        audioSource.PlayOneShot(explosion);
    }
}
