using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    public GameObject turret;
    Player player;

    public float spawnTimer;
    public float spawnInterval = 20f;   //new enemies spawn every -spawnRate- seconds
    public float bigTimer;
    public float bigSpawnInterval = 60f;    //num of enemies spawned increases every -bigSpawnInterval- seconds

    public int numEnemies;          //-numEnemies- enemies spawn each time
    public int spawnIncrement;      //the amount of more enemies each round


    // Start is called before the first frame update
    void Start()
    {
        numEnemies = 1;
        spawnTimer = 0;
        bigTimer = 0;

        spawnIncrement = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //run the timers
        spawnTimer += Time.deltaTime;
        bigTimer += Time.deltaTime;

        if (spawnTimer > spawnInterval)     //activates every 20 seconds
        {
            Debug.Log("spawned " + (numEnemies + spawnIncrement) + " enemies!");
            spawnEnemy(numEnemies + spawnIncrement);
            spawnTimer = 0;
        }

        if (bigTimer > bigSpawnInterval)    //activates every 60 seconds
        {
            if (spawnIncrement < 1) //maximum spawnIncrement is 1
            {
                spawnIncrement++;
                Debug.Log("spawn increment: " + spawnIncrement);
            }
            else if (spawnIncrement < 12)
            {
                if (spawnInterval > 6)     //mininum spawnInterval is 3
                {
                    spawnInterval = spawnInterval - 0.5f;
                    Debug.Log("spawn interval: " + spawnInterval);

                }
            }
        }

    }
    public void spawnEnemy(int numEnemies)
    {
        for (int i = 0; i < numEnemies; i++)
        {
            Instantiate(turret, randomPosition(), Quaternion.identity);
        }
    }

    //generates a random position for flying enemy to spawn in
    public Vector3 randomPosition()
    {
        int area = Random.Range(1, 10);  //1-4 means one of the four platforms, anything else is ground level
        Vector3 randomPos;

        if (area == 1)
        {
            randomPos = new Vector3(Random.Range(117, 128), 6.97f, Random.Range(-18, -8));
        }
        else if (area == 2)
        {
            randomPos = new Vector3(Random.Range(117, 128), 6.97f, Random.Range(37, 25));
        }
        else if (area == 3)
        {
            randomPos = new Vector3(Random.Range(161, 173), 6.97f, Random.Range(25, 37));
        }
        else if (area == 4)
        {
            randomPos = new Vector3(Random.Range(161, 173), 6.97f, Random.Range(-18, -8));
        }
        else
        {
            randomPos = new Vector3(Random.Range(155, 170), 0, Random.Range(-16, 38));
        }

        /*
        if (Vector3.Distance(player.CurrentPosition(), randomPos) < 30)
        {
            return EnemySpawner.randomPosition();
        }
        */

        return randomPos;

    }
}
