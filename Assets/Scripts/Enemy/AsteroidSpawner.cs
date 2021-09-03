using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject enemyAsteroid;
    private int xPos;
    private int yPos;
    private int enemyCount;

    public int enemyNum;
    public int xMax;
    public int xMin;
    public int yMax;
    public int yMin;

    // Start is called before the first frame update
    void Start()
    {
        AsteroidDrop();
    }

    private void AsteroidDrop()
    {
        while (enemyCount < enemyNum)
        {
            xPos = Random.Range((int)transform.position.x - xMin, (int)transform.position.x + xMax);
            yPos = Random.Range((int)transform.position.y - yMin, (int)transform.position.y + yMax);
            enemyAsteroid.GetComponent<Asteroid>().SetRange(transform, xMax, xMin, yMax, yMin);
            Instantiate(enemyAsteroid, new Vector2(xPos, yPos), Quaternion.identity);

            enemyCount += 1;
        }
    }

}
