using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints; // assign 6 spawners

    public float timeBetweenRounds = 5f;

    private int currentRound = 0;

    private int[] waveCounts = { 1, 2, 2, 3, 3, 4, 4, 5, 6 };

    private bool roundActive = false;

    void Start()
    {
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenRounds);

            currentRound++;

            if (currentRound <= 9)
            {
                yield return StartCoroutine(StartWave(waveCounts[currentRound - 1]));
            }
            else
            {
                yield return StartCoroutine(EndlessWave());
            }
        }
    }

    IEnumerator StartWave(int enemyCount)
    {
        roundActive = true;

        Debug.Log("Round " + currentRound);

        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

        roundActive = false;
    }

    IEnumerator EndlessWave()
    {
        roundActive = true;

        Debug.Log("Round 10+ Endless");

        // Initial 6 enemies
        for (int i = 0; i < 6; i++)
        {
            SpawnEnemy();
        }

        while (true)
        {
            yield return new WaitForSeconds(5f);
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(
            enemyPrefab,
            spawnPoints[randomIndex].position,
            spawnPoints[randomIndex].rotation
        );
    }
}
