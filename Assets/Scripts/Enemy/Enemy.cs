using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 2;
    public float speed = 5;
    public float stoppingDistance;
    public GameObject player;
    public GameObject lazer;
    public GameObject explosion;
    public float fireDelay = 0.5f;
    public float FireCooldownTimer = 0;
    public Vector3 lazerOffset = new Vector3(0.44f, 0, 0);
    public float xp = 700;
    public float wantedLevelAdd = 1;
    public float damageFlashTime;
    public ItemDrop itemDrop;
    public EnemyShipSpawner enemySpawner;
    public bool isActive;

    public Material matDefault;
    public Material matDamage;
    public SpriteRenderer spriteRenderer;


    public virtual void Awake() { }
    public virtual void Update() { }
    public virtual void RemoveFromList() { }


    public void TakeDamage(float damage)
    {
        health -= damage;
        spriteRenderer.material = matDamage;
        Invoke("ResetMaterial", damageFlashTime);

        //check if enemy is dead
        if (health <= 0)
        {
            EnemyDies();
        }
    }

    public void FireWeapons()
    {
        //If weapons arer not on cooldown
        if (FireCooldownTimer <= 0)
        {
            Vector3 offset = transform.rotation * lazerOffset;
            Instantiate(lazer, transform.position + offset, transform.rotation);
            FireCooldownTimer = fireDelay;

        }
    }

    void EnemyDies()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        player.GetComponent<PlayerController>().AddXP(xp);
        player.GetComponent<PlayerController>().AddWantedLevel(wantedLevelAdd);
        itemDrop.DropItems();
        RemoveFromList();
        Destroy(gameObject);
    }


    void ResetMaterial()
    {
        spriteRenderer.material = matDefault;
    }

    public void MoveTowards(Transform destination)
    {
        //Rotate to face destination
        Vector2 direction = destination.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);

        //Move towards destination
        if (Vector2.Distance(transform.position, destination.position) >= stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);
        }
    }

}
