using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public GameObject player;
    public GameObject childAsteroid;
    public GameObject explosion;
    public AsteroidSpawner spawner;
    public float health = 1;
    public float xp = 350;
    public float damage = 20;
    public Transform spawnCenter;
    public int xMax;
    public int xMin;
    public int yMax;
    public int yMin;

    void Awake()
    {
        //get player reference
        player = GameObject.Find("Player");

        //move asteroid
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = RandomVector(-5f, 5f);
    }

    void Update()
    {
        if (transform.position.x > spawnCenter.position.x + xMax || transform.position.x < spawnCenter.position.x - xMin
            || transform.position.y > spawnCenter.position.y + yMax || transform.position.y < spawnCenter.position.y - yMin)
        {
            var rb = GetComponent<Rigidbody2D>();
            rb.velocity *= -1;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        //destroy if health depleated
        if (health <= 0)
        {
            DestroyAsteroid();
        }
    }

    //generate random direction to move
    private Vector2 RandomVector(float min, float max)
    {
        var x = Random.Range(min, max);
        var y = Random.Range(min, max);
        return new Vector2(x, y);
    }

    public virtual void DestroyAsteroid()
    {
        //spawn break off asteroids
        childAsteroid.GetComponent<ChildAsteroid>().SetRange(spawnCenter, xMax, xMin, yMax, yMin);
        Instantiate(childAsteroid, transform.position, transform.rotation);
        Instantiate(childAsteroid, transform.position, transform.rotation);
        


        Instantiate(explosion, transform.position, transform.rotation);
        player.GetComponent<PlayerController>().AddXP(xp);
        Destroy(gameObject);
    }

    public void SetRange(Transform _spawnCenter, int _xMax, int _xMin, int _yMax, int _yMin)
    {
        spawnCenter = _spawnCenter;
        xMax = _xMax;
        xMin = _xMin;
        yMax = _yMax;
        yMin = _yMin;
    }

    //Damage enemy on collision
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<PlayerController>() != null)
        {
            if (!collider.gameObject.GetComponent<PlayerController>().isDead)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                collider.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
                Destroy(this.gameObject);
            }
        }
    }
}
