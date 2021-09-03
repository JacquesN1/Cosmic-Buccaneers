using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSound : MonoBehaviour
{
    public GameObject player;

    void Awake()
    {
        //Play explosion sound effect
        player = GameObject.Find("Player");
        player.GetComponentInChildren<SoundManager>().PlayExplosion();
    }
}
