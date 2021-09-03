using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLazerController : MonoBehaviour
{
    public float maxSpeed = 5;
    public GameObject player;
    public float damage = 1;

    void Awake()
    {
        //Rotate to face player
        player = GameObject.Find("Player");
        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        //Play shoot sound effect
        player.GetComponentInChildren<SoundManager>().PlayShoot();
    }
    // Update is called once per frame
    void Update()
    {
        //Move Lazer
        Vector3 position = transform.position;
        Vector3 velocity = new Vector3(maxSpeed * Time.deltaTime, 0, 0);
        position += transform.rotation * velocity;
        transform.position = position;
    }

    //Damage enemy on collision
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<PlayerController>() != null)
        {
            collider.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

