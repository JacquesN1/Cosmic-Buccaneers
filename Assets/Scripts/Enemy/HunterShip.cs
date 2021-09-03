using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterShip : Enemy
{
    public float firingDistance;
    public float stopingDistance;

    public override void Awake()
    {
        enemySpawner.AddHunterShipToList(gameObject);
        spriteRenderer = GetComponent<SpriteRenderer>();
        matDefault = spriteRenderer.material;
        matDamage = Resources.Load("DamageFlash", typeof(Material)) as Material;
        itemDrop = GetComponent<ItemDrop>();

        isActive = false;
        player = GameObject.Find("Player");
    }

    public override void Update()
    {
        FireCooldownTimer -= Time.deltaTime;

        float distance = Vector2.Distance(gameObject.transform.position, player.transform.position);

        if (distance >= stopingDistance)
        {
            MoveTowards(player.transform);
        }

        if (distance <= firingDistance)
        {
            FireWeapons();
        }
    }

    public override void RemoveFromList()
    {
        enemySpawner.RemoveHunterShipFromList(gameObject);
    }

}
