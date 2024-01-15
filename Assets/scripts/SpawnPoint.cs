using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private int lastNumberSpawn;
    private int lastNumberPrefab;
    public GameObject[] spawnPoint;

    void start()
    {
        spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }

    IEnumerator enemySpawn()
    {
        yield return new WaitForSeconds(Random.Range(1, 5));
        int spawn = Random.Range(0, spawnPoint.Length);
        GameObject _sp = spawnPoint[GetRandomForSpawn(0, spawnPoint.Length)];
        GameObject enemy = (GameObject)Resources.Load("Enemy", typeof(GameObject));
        Instantiate(enemy, _sp.transform.position, _sp.transform.rotation);
    }

    private int GetRandomForSpawn(int min, int max)
    {
        int rand = Random.Range(min, max);
        while (rand == lastNumberSpawn)
            rand = Random.Range(min, max);
        lastNumberSpawn = rand;
        return rand;
    }
}
