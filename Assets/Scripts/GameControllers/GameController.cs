using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayerController player;
    public DateTime startDate;
    public DateTime currentDate;
    public float daysSurvived;
    public float secondsPerDay;
    private float counter = 0;
    public UIUpdater UIDateText;
    public EnemyShipSpawner shipSpawner;

    void Awake()
    {
        startDate = new DateTime(2270, 1, 1);
        currentDate = startDate;
        daysSurvived = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (counter >= secondsPerDay)
        {
            currentDate = currentDate.AddDays(1);
            counter = 0;
            daysSurvived++;

            player.DayPassed();

            foreach(GameObject planet in shipSpawner.GetPlanetList())
            {
                planet.GetComponent<PlanetShop>().DayPassed();
            }
        }
        counter += Time.deltaTime;
    }
}
