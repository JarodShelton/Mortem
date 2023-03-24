using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public Player player;
    public float spawnInterval = 3f;
    public float intervalFraction = 0.75f;
    public float increaseInterval = 15f;
    public int initialEnemies = 10;
    public int spawnCap = 250;
    public float spawnGap = 30f;
    public bool spawnZones = false;

    private int spawnAmount = 1;
    private float spawnTimer;
    private float noiseyspawnInterval;
    private float increaseTimer;
    private int totalEnemies;

    void Start()
    {
        spawnTimer = 0;
        increaseTimer = 0;

        noiseyspawnInterval = Random.Range(0f, spawnInterval);
        spawnEnemy(initialEnemies);
        totalEnemies = initialEnemies;
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        increaseTimer += Time.deltaTime;

        if (spawnTimer > noiseyspawnInterval)
        {
            if(totalEnemies<= spawnCap)
            {
                spawnEnemy(spawnAmount);
                totalEnemies += spawnAmount;
            }
            noiseyspawnInterval = Random.Range(0f, spawnInterval);
            spawnTimer = 0;
        }
        if (increaseTimer > increaseInterval)
        { 
            spawnInterval*= intervalFraction;
            increaseTimer = 0;
        }
    }

    public void spawnEnemy(int spawnAmount)
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Instantiate(enemy, randomPosition(), Quaternion.identity);
        }
    }

    public void death()
    {
        totalEnemies -= 1;
    }

        public Vector3 randomPosition()
    {
        Vector3 randomPos = new Vector3();
        if (!spawnZones)
        {
            randomPos = new Vector3(Random.Range(115, 170), Random.Range(14, 19), Random.Range(-45, 65));
        }
        else
        {
            int zone = Random.Range(1, 5);
            if (zone == 1)
            {
                randomPos = new Vector3(Random.Range(117, 128), 6.97f, Random.Range(-18, -8));
                Debug.Log("1");
            }
            else if (zone == 2)
            {
                randomPos = new Vector3(Random.Range(117, 128), 6.97f, Random.Range(25, 37));
                Debug.Log("2");
            }
            else if (zone == 3)
            {
                randomPos = new Vector3(Random.Range(161, 173), 6.97f, Random.Range(25, 37));
                Debug.Log("3");
            }
            else if (zone == 4)
            {
                randomPos = new Vector3(Random.Range(161, 173), 6.97f, Random.Range(-18, -8));
                Debug.Log("4");
            }
        }
        if (Vector3.Distance(player.CurrentPosition(), randomPos) < spawnGap)
        {
            randomPosition();
        }
        return randomPos;
    }
}
