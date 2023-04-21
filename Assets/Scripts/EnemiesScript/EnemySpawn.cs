using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float spawnRate = 3f;
    public Transform[] spawnPoints;
    public int maxEnemies = 15;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    { 
        StartCoroutine(SpawnEnemy());
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (gameManager.score == 15)
        {
            spawnRate = 2f;
        }
        if(gameManager.score == 50)
        {
            spawnRate = 1f;
            maxEnemies = 30;
        }
    }

    private IEnumerator SpawnEnemy()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        while(EnemyCount() < maxEnemies){
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            if(maxEnemies == 15)
            {
            Instantiate(enemyPrefabs[0], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            }
            if(maxEnemies == 20)
            {
            Instantiate(enemyPrefabs[enemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            }
            yield return wait;
        }
    }
    public int EnemyCount()
    {
        int count = 0;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            count++;
        }
        return count;
    }
}
