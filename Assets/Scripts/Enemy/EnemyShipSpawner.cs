using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipSpawner : MonoBehaviour
{
    private List<GameObject> planetList;
    public GameObject player;

    private List<GameObject> cargoShipList;
    public GameObject cargoShipPrefab;
    private GameObject cargoShip;
    public int maxCargoShips;

    private List<GameObject> hunterShipList;
    public GameObject hunterShipPrefab;
    private GameObject hunterShip;
    private int maxHunterShips;

    public void AddPlanetToList(GameObject planet)
    {
        if (planetList == null)
        {
            planetList = new List<GameObject>();
        }
        planetList.Add(planet);
    }

    public List<GameObject> GetPlanetList()
    {
        return planetList;
    }

    public void AddCargoShipToList(GameObject ship)
    {
        if (cargoShipList == null)
        {
            cargoShipList = new List<GameObject>();
        }
        cargoShipList.Add(ship);
    }

    public void RemoveCargoShipFromList(GameObject ship)
    {
        GameObject shipToRemove = null;

        foreach (GameObject _ship in cargoShipList)
        {
            if(_ship == ship)
            {
                shipToRemove = _ship;
            }
        }
        if (shipToRemove != null)
        {
            cargoShipList.Remove(shipToRemove);
        }
    }

    public void AddHunterShipToList(GameObject ship)
    {
        if (hunterShipList == null)
        {
            hunterShipList = new List<GameObject>();
        }
        hunterShipList.Add(ship);
    }

    public void RemoveHunterShipFromList(GameObject ship)
    {
        GameObject shipToRemove = null;

        foreach (GameObject _ship in hunterShipList)
        {
            if (_ship == ship)
            {
                shipToRemove = _ship;
            }
        }
        if (shipToRemove != null)
        {
            hunterShipList.Remove(shipToRemove);
        }
    }

    public Vector2 FindClosestPlanet (Vector2 playerPosition)
    {
        Vector2 closestPlanet = planetList[0].transform.position;
        float furthestDistance = float.MaxValue;

        if (planetList != null)
        {
            foreach (GameObject _planet in planetList)
            {
                float distance = Vector2.Distance(playerPosition, _planet.transform.position);
                if (distance < furthestDistance)
                {
                    furthestDistance = distance;
                    closestPlanet = _planet.transform.position;
                }
            }
        }
        return closestPlanet;
    }

    private void Awake()
    {
        cargoShipPrefab.GetComponent<Enemy>().enemySpawner = this;
        hunterShipPrefab.GetComponent<Enemy>().enemySpawner = this;
        cargoShipList = new List<GameObject>();
        hunterShipList = new List<GameObject>();
    }

    private void Update()
    {
        maxHunterShips = 2 * (int)player.GetComponent<PlayerController>().wantedLevel;

        if (cargoShipList.Count < maxCargoShips && Random.Range(1, 5000) <= 25)
        {
            GameObject spawnPlanet = planetList[Random.Range(0, planetList.Count)];
            GameObject destinationPlanet = planetList[Random.Range(0, planetList.Count)];

            while (spawnPlanet == destinationPlanet)
            {
                destinationPlanet = planetList[Random.Range(0, planetList.Count)];
            }

            cargoShip = Instantiate(cargoShipPrefab, spawnPlanet.transform.position, Quaternion.identity);
            cargoShip.GetComponent<Inventory>().RandomizeInventory(0, 50);
            cargoShip.GetComponent<CargoShip>().SetDestination(destinationPlanet);
        }

        if (hunterShipList.Count < maxHunterShips && Random.Range(1, 5000) <= 25)
        {
            GameObject spawnPlanet = planetList[Random.Range(0, planetList.Count)];
            hunterShip = Instantiate(hunterShipPrefab, spawnPlanet.transform.position, Quaternion.identity);
            hunterShip.GetComponent<Inventory>().RandomizeInventory(0, 50);
        }
    }
}
