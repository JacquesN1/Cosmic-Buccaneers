using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildAsteroid : Asteroid
{
    public override void DestroyAsteroid()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        player.GetComponent<PlayerController>().AddXP(xp);
        Destroy(gameObject);
    }
}

