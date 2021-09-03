using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waypoint : MonoBehaviour
{
    public Image image;
    public GameObject player;
    public EnemyShipSpawner spawner;

    private Vector2 target;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    void Awake()
    {
        AdjustWaypointForResolution();
    }

    // Update is called once per frame
    void Update()
    {
        target = spawner.FindClosestPlanet(player.transform.position);
        Vector2 position = Camera.main.WorldToScreenPoint(target);
        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);

        image.transform.position = position;
    }

    public void AdjustWaypointForResolution()
    {
        minX = image.GetPixelAdjustedRect().width / 2;
        maxX = Screen.width - minX;
        minY = image.GetPixelAdjustedRect().height / 2;
        maxY = Screen.height - minX;
    }
}
