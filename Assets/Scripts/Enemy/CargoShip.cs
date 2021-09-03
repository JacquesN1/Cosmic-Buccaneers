using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoShip : Enemy
{
    public GameObject destinationPlanet;
    public GameObject fieldOfViewObj;
    private EnemyFieldOfView fieldOfView;

    public override void Awake()
    {
        enemySpawner.AddCargoShipToList(gameObject);
        spriteRenderer = GetComponent<SpriteRenderer>();
        matDefault = spriteRenderer.material;
        matDamage = Resources.Load("DamageFlash", typeof(Material)) as Material;
        itemDrop = GetComponent<ItemDrop>();

        isActive = false;
        player = GameObject.Find("Player");
        fieldOfView = Instantiate(fieldOfViewObj, Vector3.zero, Quaternion.identity).GetComponent<EnemyFieldOfView>();
        fieldOfView.enemy = gameObject;
    }

    public override void Update()
    {
        FireCooldownTimer -= Time.deltaTime;

        if (isActive)
        {
            MoveTowards(player.transform);
            FireWeapons();
        }
        else
        {
            MoveTowards(destinationPlanet.transform);
        }

        if (Vector2.Distance(transform.position, destinationPlanet.transform.position) <= stoppingDistance)
        {
            enemySpawner.RemoveCargoShipFromList(gameObject);
            Destroy(gameObject);
        }
    }

    public override void RemoveFromList()
    {
        enemySpawner.RemoveCargoShipFromList(gameObject);
    }

    public void SetDestination(GameObject _destinationPlanet)
    {
        destinationPlanet = _destinationPlanet;
    }
}
