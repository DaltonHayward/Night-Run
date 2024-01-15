using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    //public GameObject enemyPrefab;
    [SerializeField] private float spawnRate = 5.0f;
    public float groundOffset = 0;
    private float SpawnTimer;
    public float timeRemaining = 15.0f;
    public int selectedW = 0;


    //public GameObject[] spawnLocations;

    void Awake()
    {
        if (selectedW == 0)
        {
            spawnRate = 9.0f;
        }
        if (selectedW == 1)
        {
            spawnRate = 7.0f;
        }
        if (selectedW == 2)
        {
            spawnRate = 5.0f;
        }


        //spawnLocations = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)

        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            spawnRate = spawnRate - 1.0f;
            //timeRemaining = 15.0f;
            if (selectedW == 0) //if Pistol
            {
                timeRemaining = 25.0f;
                if (spawnRate <= 6.0f)
                {
                    spawnRate = 9.0f;
                }
            }
            if (selectedW == 1) //if Machine Gun
            {
                timeRemaining = 20.0f;
                if (spawnRate == 3.0f)
                {
                    spawnRate = 5.0f;
                }
            }
            if (selectedW == 2)
            {
                timeRemaining = 15.0f;
                if (spawnRate == 0.0f)
                {
                    spawnRate = 4.0f;
                }
            }
        }
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {

        //sint spawn = Random.Range(0, spawnLocations.Length);
        if (Time.time > SpawnTimer)
        {

            Vector3 spawnPosition = new Vector3(transform.position.x, groundOffset, transform.position.z);
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomEnemy], spawnPosition, transform.rotation);
            SpawnTimer = Time.time + spawnRate;
        }
    }

}
