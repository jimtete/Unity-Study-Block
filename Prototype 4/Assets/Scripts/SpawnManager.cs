using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerup;
    private float spawnRange=8f;
    public int enemyCount;
    public int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        
        SpawnEnemyWave(waveNumber);
        
        
    }

    private Vector3 GenerateSpawnPoint()
    {
        float x = Random.Range(-spawnRange, spawnRange);
        float z = Random.Range(-spawnRange, spawnRange);
        return new Vector3(x, 0.15f, z);
    }

    void SpawnEnemyWave(int length)
    {
        for (int i = 0; i < length; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPoint(), enemyPrefab.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            SpawnEnemyWave(++waveNumber);
            Instantiate(powerup, GenerateSpawnPoint(), powerup.transform.rotation);
        }
    }
}
