using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    /*public GameObject obstaclePrefab;*/
    public List<GameObject> obstacles;
    public GameObject coin;
    private Vector3 spawnPos = new Vector3(25, 0.1f, 0);
    private Vector3 coinSpawnPos = new Vector3(30, 4.35f, 0);

    private float startDelay = 2;
    private float repeatRate = 2;

    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        int index = Random.Range(0, obstacles.Count);
        if (!playerControllerScript.gameOver)
        Instantiate(obstacles[index], spawnPos, obstacles[index].transform.rotation);
        int generator = Random.Range(0, 6);
        if (generator == 5)
        {
            Instantiate(coin, coinSpawnPos, coin.transform.rotation);
        }
    }
}
