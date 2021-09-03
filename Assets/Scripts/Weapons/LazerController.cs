using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerController : MonoBehaviour
{
    public float maxSpeed = 5;
    public GameObject crosshair;
    private GameObject player;
    public float destroyTimer = 0;
    public float damage = 1;
 
    void Awake()
    {
        //Rotate to face crosshair
        crosshair = GameObject.Find("Crosshair");
        Vector2 direction = crosshair.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        //Play shoot sound effect
        player = GameObject.Find("Player");
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

        //Update timer
        destroyTimer += Time.deltaTime;

        //destroy lazer if active for over 5 seconds
        if (destroyTimer >= 5)
        {
            Destroy(this.gameObject);
        }
    }

    //Damage enemy on collision
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Enemy>() != null)
        {
            collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        else if (collider.gameObject.GetComponent<Asteroid>() != null)
        {
            collider.gameObject.GetComponent<Asteroid>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        else if (collider.gameObject.GetComponent<ChildAsteroid>() != null)
        {
            collider.gameObject.GetComponent<ChildAsteroid>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
